using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_AndersonRobotic
{
    public class AndersonRoboticLeaderScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.ANDennemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.ANDally;

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonLeader;

        protected override string RoleName => Plugin.ConfigAndersonLeader.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigAndersonLeader;
    } 
}
