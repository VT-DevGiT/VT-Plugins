using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class FoundationUTRScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.FoundationUTR;

        protected override string RoleName => Plugin.ConfigFoundationUTR.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigFoundationUTR;
    }
}
