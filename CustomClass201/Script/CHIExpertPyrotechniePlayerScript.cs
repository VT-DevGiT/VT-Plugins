using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class CHIExpertPyrotechnieScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)TeamID.CDM, (int)TeamID.NTF, (int)TeamID.SEC };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)RoleID.CHIExpertPyrotechnie;

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
            Server.Get.Logger.Info($"{ev.HitInfo.GetDamageType()} {ev.Victim.NickName}");
            if (ev.Victim == Player && ev.HitInfo.GetDamageType() == DamageTypes.Grenade)
                ev.Allow = false;
        }
    }
}
