using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class ZoneManagerScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.ZoneManager;

        protected override string RoleName => PluginClass.ConfigZoneManager.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigZoneManager;
    }
}
