using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;

namespace VTDevHelp
{
    [AutoRegisterManager.Ignore]
    [SynapseTeamInformation(ID = 21, Name = "Test")]
    internal class TestTeam : Synapse.Api.Teams.SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {

        }
    }
}
