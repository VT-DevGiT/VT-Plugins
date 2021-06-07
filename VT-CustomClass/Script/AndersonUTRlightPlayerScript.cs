using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class AndersonUTRlightScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamGroupe.ANDennemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.ANDally;

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonUTRlight;

        protected override string RoleName => PluginClass.ConfigAndersonUTRlight.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigAndersonUTRlight;

        protected override bool heavyUTR => false;
    }
}
