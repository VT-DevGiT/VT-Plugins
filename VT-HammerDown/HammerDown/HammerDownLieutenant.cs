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

namespace VT_HammerDown
{
    public class HammerDownLieutenant : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.CDM;

        protected override int RoleId => (int)RoleID.CdmLieutenant;

        protected override string RoleName => Plugin.ConfigHammerDownLieutenant.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigHammerDownLieutenant;
    } 
}
