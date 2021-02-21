using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class NTFInfirmierScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.NTFInfirmier;

        protected override string RoleName => PluginClass.ConfigNTFInfirmier.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFInfirmier;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
        }
        private void OnDammage(PlayerDamageEventArgs ev)
        {
            ev.PointeCreuses(Player);
        }
    }
}
