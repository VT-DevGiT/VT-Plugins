using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT_U2I
{
    public class U2IAgent : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSpecialist;

        protected override int RoleTeam => (int)TeamID.U2I;

        protected override int RoleId => (int)RoleID.U2IAgent;

        protected override string RoleName => Plugin.Instance.Config.U2IAgentName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.U2IRoleConfig;
    } 
}
