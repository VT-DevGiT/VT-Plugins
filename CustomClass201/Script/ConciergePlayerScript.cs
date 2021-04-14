using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class ConciergeScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.SCP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.Concierge;

        protected override string RoleName => PluginClass.ConfigConcierge.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigConcierge;
    }
}
