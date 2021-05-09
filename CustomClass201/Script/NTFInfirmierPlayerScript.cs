using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.PlayerScript;
using System;

namespace CustomClass.PlayerScript
{
    public class NTFInfirmierScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP, (int)TeamID.SHA, (int)TeamID.AND };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.NtfInfirmier;

        protected override string RoleName => PluginClass.ConfigNTFInfirmier.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFInfirmier;

        private DateTime lastPower = DateTime.Now.AddSeconds(-PluginClass.ConfigNTFInfirmier.CoolDownDoor);
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.HollowBullet(Player);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower(PowerType.Defibrillation);
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Defibrillation && (DateTime.Now - lastPower).TotalSeconds > PluginClass.ConfigNTFInfirmier.Cooldown)
            {
                Player corpseowner = Methods.GetPlayercoprs(Player, 2.5f);
                if (Methods.IsScpRole(corpseowner) == false)
                { 
                    corpseowner.RoleID = Dictionary.PlayerRole[corpseowner];
                    corpseowner.Position = corpseowner.DeathPosition;
                    corpseowner.Inventory.Clear();
                    lastPower = DateTime.Now;
                }
            }
            else if (power == PowerType.Respawn)
                Reponse.Cooldown(Player, lastPower, PluginClass.ConfigNTFInfirmier.Cooldown);
            else return false;
            return false;
        }
    }
}
