using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VTCustomClass.CustomTeam
{
    [SynapseTeamInformation(
        ID = (int)TeamID.NetralSCP,
        Name = name
        )]
    public class NetralSCPTeam : SynapseTeam
    {
        const string name = "NetralSCP";
        public override void Spawn(List<Player> players)
        {
            Server.Get.Logger.Warn($"you can't make the team spawn {name}");
        }
    }
}
