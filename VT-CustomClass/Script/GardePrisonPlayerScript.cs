using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class GardePrisonScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.GardePrison;

        protected override string RoleName => PluginClass.ConfigGardePrison.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigGardePrison;
    }
}
