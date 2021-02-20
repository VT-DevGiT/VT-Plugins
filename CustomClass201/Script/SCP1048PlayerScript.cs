using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class SCP1048cript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp049;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)MoreClasseID.SCP1048;

        protected override string RoleName => Plugin.ConfigSCP1048.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP1048;
    }
}
