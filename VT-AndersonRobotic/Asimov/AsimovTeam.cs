using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VT_AndersonRobotic
{
    [SynapseTeamInformation(
        ID = (int)TeamID.ASI,
        Name = "AsimovTeam"
        )]
    public class AsimovTeam : AbstractTeam
    {
        public override List<RespawnRoleInfo> Roles { get ; set; }

        public override void Initialise()
        {
            Roles = new List<RespawnRoleInfo>()
            {
                new RespawnRoleInfo() { Max = 1, Min = -1, Priority = 1, RoleID = (int)RoleID.GeneralAsimov},
                new RespawnRoleInfo() { Max = -1, RoleID = (int)RoleID.GardienAsimov }
            };
        }
    }
}
