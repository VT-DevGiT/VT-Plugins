using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class CHIExpertPyrotechnieScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.SHA };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiExpertPyrotechnie;

        protected override string RoleName => PluginClass.ConfigCHIExpertPyrotechnie.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIExpertPyrotechnie;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
        }
        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player && ev.HitInfo.GetDamageType() == DamageTypes.Grenade)
                ev.Allow = false;
        }
    }
}
