using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class GardeSuperviseurScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.GardeSuperviseur;

        protected override string RoleName => PluginClass.ConfigGardeSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigGardeSuperviseur;
    }
}
