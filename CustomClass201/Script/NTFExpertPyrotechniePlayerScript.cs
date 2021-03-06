using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class NTFExpertPyrotechnieScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)RoleID.NTFExpertPyrotechnie;

        protected override string RoleName => PluginClass.ConfigNTFExpertPyrotechnie.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFExpertPyrotechnie;

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