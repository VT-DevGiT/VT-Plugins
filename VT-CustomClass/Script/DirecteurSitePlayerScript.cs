using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class DirecteurSiteScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.VIPennemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.VIPally;

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.VIP;

        protected override int RoleId => (int)RoleID.DirecteurSite;

        protected override string RoleName => Plugin.ConfigDirecteurSite.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigDirecteurSite;
    }
}
