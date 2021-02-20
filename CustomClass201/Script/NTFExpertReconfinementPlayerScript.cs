using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class NTFExpertReconfinementScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.NTFExpertReconfinement;

        protected override string RoleName => Plugin.ConfigNTFExpertReconfinement.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigNTFExpertReconfinement;
    }
}
