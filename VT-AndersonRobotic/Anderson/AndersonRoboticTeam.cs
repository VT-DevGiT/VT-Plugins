using Synapse;
using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT_AndersonRobotic
{
    [Synapse.Api.Teams.SynapseTeamInformation(
        ID = (int)TeamID.AND,
        Name = "AndersneRobtic"
        )]
    public class AndersonRoboticTeam : AbstractTeam
    {        
        public override List<RespawnRoleInfo> Roles { get; set; }

        public override void Initialise()
        {
            Roles = new List<RespawnRoleInfo>()
            {
                new RespawnRoleInfo() { Priority = 3, Max = 1, Min = -1, RoleID = (int)RoleID.AndersonLeader},
                new RespawnRoleInfo() { Priority = 0, Max = -1, RoleID = (int)RoleID.AndersonEngineer}
            };
        }
    }
}
