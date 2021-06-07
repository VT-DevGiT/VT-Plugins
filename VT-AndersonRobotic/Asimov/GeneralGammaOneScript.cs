using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;
using VT_Referance.PlayerScript;
using Synapse.Config;
using Synapse;
using static VT_Referance.Variable.Data;

namespace VT_AndersonRobotic
{
    public class GeneralGammaOneScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfCommander;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonEngineer;

        protected override string RoleName => Plugin.ConfigGeneralGammaOne.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigGeneralGammaOne;
    } 
}
