using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VT_Alpha
{
    public class AlphaOneAgent : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfCaptain;

        protected override int RoleTeam => (int)TeamID.AL1;

        protected override int RoleId => (int)RoleID.AlphaOneAgent;

        protected override string RoleName => Plugin.ConfigAlphaOneAgent.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigAlphaOneAgent;
    } 
}
