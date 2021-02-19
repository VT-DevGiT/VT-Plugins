using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class SCP682Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp93953;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)MoreClasseID.SCP682;

        protected override string RoleName => PluginClass.ConfigSCP682.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP682;
    }
}
