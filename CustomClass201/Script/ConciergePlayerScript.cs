using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ConciergeScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RSC;

        protected override int RoleId => (int)MoreClasseID.Concierge;

        protected override string RoleName => Plugin.ConfigConcierge.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigConcierge;
    }
}
