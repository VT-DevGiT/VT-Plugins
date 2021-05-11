using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;
using VT_Referance.PlayerScript;
using Synapse.Config;
using Synapse;

namespace VT_AndersonRobotic
{
    public class AndersonRoboticEngineerScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => new List<int> { (int)TeamID.RSC, (int)TeamID.NTF, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.AL1, (int)TeamID.ASI };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.AND };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonEngineer;

        protected override string RoleName => Plugin.ConfigAndersonEngineer.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigAndersonEngineer;
    } 
}
