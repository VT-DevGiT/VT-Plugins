using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class Scripte201 : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CDP, (int)Team.MTF, (int)Team.RSC };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => 201;

        protected override string RoleName => "Test 201";

        protected override AbstractConfigSection Config => PluginClass.Config201;
    }
}
