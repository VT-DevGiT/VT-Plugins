using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class GardeSuperviseurScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)TeamID.CDM, (int)TeamID.NTF, (int)TeamID.SEC, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.SEC;

        protected override int RoleId => (int)RoleID.GardeSuperviseur;

        protected override string RoleName => PluginClass.ConfigGardeSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigGardeSuperviseur;
    }
}
