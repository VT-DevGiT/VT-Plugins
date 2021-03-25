using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class GardeSuperviseurScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC } : new List<int> { };

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.GardeSuperviseur;

        protected override string RoleName => PluginClass.ConfigGardeSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigGardeSuperviseur;
    }
}
