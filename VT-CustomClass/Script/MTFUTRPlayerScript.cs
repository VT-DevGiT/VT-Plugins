using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class MTFUTRScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.MTFUTR;

        protected override string RoleName => Plugin.ConfigMTFUTR.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigMTFUTR;
    }
}
