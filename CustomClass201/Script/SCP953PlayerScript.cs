using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP953Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RIP;

        protected override int RoleId => (int)RoleID.SCP953;

        protected override string RoleName => PluginClass.ConfigSCP953.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP953;
    }
}
