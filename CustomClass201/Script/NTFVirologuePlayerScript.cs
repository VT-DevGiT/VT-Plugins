using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class NTFVirologueScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.NTFVirologue;

        protected override string RoleName => Plugin.ConfigNTFVirologue.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigNTFVirologue;
    }
}
