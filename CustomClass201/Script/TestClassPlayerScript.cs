using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class TestClassScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.SCP, (int)Team.CDP, (int)Team.CHI, (int)Team.MTF, (int)Team.RIP, (int)Team.RSC, (int)Team.TUT };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP, (int)Team.CDP, (int)Team.CHI, (int)Team.MTF, (int)Team.RIP, (int)Team.RSC, (int)Team.TUT };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)Team.TUT;

        protected override int RoleId => (int)MoreClasseID.TestClass;

        protected override string RoleName => PluginClass.ConfigTestClass.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigTestClass;
    }
}
