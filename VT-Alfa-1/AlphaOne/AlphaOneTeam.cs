using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;

namespace VT_Alpha
{
    [SynapseTeamInformation(
        ID = (int)TeamID.AL1,
        Name = "AlfaOneAgent"
        )]
    public class AlphaOneTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            foreach (var player in players)
            {
                player.RoleID = (int)RoleID.AlphaOneAgent;
                players.Remove(player);
            }
        }
    }
}
