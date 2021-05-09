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
    public class HammerDownLieutenant : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.AND, (int)TeamID.SHA };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.CDM;

        protected override int RoleId => (int)RoleID.CdmLieutenant;

        protected override string RoleName => Plugin.ConfigHammerDownLieutenant.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigHammerDownLieutenant;
    } 
}
