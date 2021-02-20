using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class SCP999Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RIP;

        protected override int RoleId => (int)MoreClasseID.SCP999;

        protected override string RoleName => Plugin.ConfigSCP999.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP999;
    }
}
