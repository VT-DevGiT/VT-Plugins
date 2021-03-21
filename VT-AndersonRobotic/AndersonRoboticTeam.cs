using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_AndersonRobotic
{
    [SynapseTeamInformation(
        ID = (int)TeamID.AndersneRobotic,
        Name = "AndersneRobtic"
        )]
    public class AndersonRoboticTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            if (players.Count > Plugin.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Config.SpawnSize);
            
            foreach (var ply in players)
                ply.RoleID = (int)RoleID.AndersonUnite;
        }
    }
}
