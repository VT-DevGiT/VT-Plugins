using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ICIntrusScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHIIntrus;

        protected override string RoleName => PluginClass.ConfigCHIntrus.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIntrus;
    }
}
