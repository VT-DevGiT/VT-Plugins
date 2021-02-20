using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class StaffClassScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)Team.TUT;

        protected override int RoleId => (int)MoreClasseID.Staff;

        protected override string RoleName => Plugin.ConfigStaff.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigStaff;
    }
}
