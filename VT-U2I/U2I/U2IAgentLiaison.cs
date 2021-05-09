using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_U2I
{
    public class U2IAgentLiaison : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.AND, (int)TeamID.BerserkSCP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.AL1, (int)TeamID. };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.U2I;

        protected override int RoleId => (int)RoleID.U2IAgentLiaison;

        protected override string RoleName => Plugin.ConfigU2IAgentLiaison.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigU2IAgentLiaison;
    } 
}
