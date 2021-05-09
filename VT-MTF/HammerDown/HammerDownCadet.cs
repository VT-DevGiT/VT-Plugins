using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    public class HammerDownCadet : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.AND, (int)TeamID.SHA, (int)TeamID.SCP, (int)TeamID.BerserkSCP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.ASI, (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.NtfCadet;

        protected override int RoleTeam => (int)TeamID.CDM;

        protected override int RoleId => (int)RoleID.CdmCadet;

        protected override string RoleName => Plugin.ConfigHammerDownCadet.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigHammerDownCadet;
    } 
}
