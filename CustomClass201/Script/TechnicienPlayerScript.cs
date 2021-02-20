using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class TechnicienScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.Technicien;

        protected override string RoleName => Plugin.ConfigTechnicien.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigTechnicien;
    }
}
