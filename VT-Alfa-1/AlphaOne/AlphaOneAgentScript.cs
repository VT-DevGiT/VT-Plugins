using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_Alpha
{
    public class AlphaOneAgent : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFEnemys;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFFriends;

        protected override RoleType RoleType => RoleType.NtfCommander;

        protected override int RoleTeam => (int)TeamID.AL1;

        protected override int RoleId => (int)RoleID.AlphaOneAgent;

        protected override string RoleName => Plugin.ConfigAlphaOneAgent.RoleName;

        protected override AbstractConfigSection Config => Plugin.Config;
    } 
}
