using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class CHIKamikazeScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.SHA };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiKamikaze;

        protected override string RoleName => PluginClass.ConfigCHIKamikaze.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIKamikaze;

        private DateTime coolDown = DateTime.Now;

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Suicide && (DateTime.Now - coolDown).TotalSeconds > 30)
            {
                Server.Get.Map.SpawnGrenade(Player.Position, Vector3.zero, 0.1f, GrenadeType.Grenade, Player);
                Player.Kill(DamageTypes.Grenade);
                return true;
            }
            return false;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDeathEvent -= OnDeath;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower(PowerType.Suicide);

        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == this.Player)
            {
                Server.Get.Map.SpawnGrenade(Player.DeathPosition, Vector3.zero, PluginClass.ConfigCHIKamikaze.GrenadeTime, GrenadeType.Grenade, Player);
            }
        }
    }
}
