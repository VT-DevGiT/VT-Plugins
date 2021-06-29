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
        ID = (int)TeamID.ASI,
        Name = "AsimovTeam"
        )]
    public class AsimovTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            if (players.Count > Plugin.Config.SpawnSizeAsimov)
                players = players.GetRange(0, Plugin.Config.SpawnSizeAsimov);

            if (players.Count > 0)
            {
                players[0].RoleID = (int)RoleID.GeneralAsimov;
                players.Remove(players[0]);
            }
            foreach (var ply in players)
            {
                ply.RoleID = (int)RoleID.GardienAsimov;
                players.Remove(ply);
            }
        }
    }
}
