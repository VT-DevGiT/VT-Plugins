using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VT_AndersonRobotic
{
    public class GardienGammaOneScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonLeader;

        protected override string RoleName => Plugin.ConfigGardienGammaOne.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigGardienGammaOne;
    } 
}
