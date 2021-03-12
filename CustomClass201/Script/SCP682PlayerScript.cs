using Synapse;
using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP682Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP, (int)Team.SCP, (int)TeamID.BerserkSCP };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Scp93953;

        protected override int RoleTeam => (int)TeamID.BerserkSCP;

        protected override int RoleId => (int)RoleID.SCP682;

        protected override string RoleName => PluginClass.ConfigSCP682.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP682;
    }
}
