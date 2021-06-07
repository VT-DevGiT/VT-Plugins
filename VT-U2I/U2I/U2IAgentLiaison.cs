using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VT_U2I
{
    public class U2IAgentLiaison : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.U2I;

        protected override int RoleId => (int)RoleID.U2IAgentLiaison;

        protected override string RoleName => Plugin.ConfigU2IAgentLiaison.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigU2IAgentLiaison;
    } 
}
