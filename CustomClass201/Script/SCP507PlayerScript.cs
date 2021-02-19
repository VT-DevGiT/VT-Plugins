using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class SCP507Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.SCP, (int)Team.CHI };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RSC;

        protected override int RoleId => (int)MoreClasseID.SCP507;

        protected override string RoleName => PluginClass.ConfigSCP507.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP507;
    }
}
