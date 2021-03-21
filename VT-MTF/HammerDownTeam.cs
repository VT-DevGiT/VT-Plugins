using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    [SynapseTeamInformation(
        ID = (int)TeamID.CDM,
        Name = "HammerDown"
        )]
    public class HammerDownTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {            
            if (players.Count > Plugin.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Config.SpawnSize);
            if (players.Any())
            { 
                int chance = players.Count() > 1? UnityEngine.Random.Range(0, players.Count() - 1) : 0;
                var commander = players[chance];
                commander.RoleID = (int)RoleID.CDMCommandant;
                players.Remove(commander);

                int nbLieu = 0;
                while (players.Any() && nbLieu < Plugin.Config.MaxLieutenant)
                {
                    int chanceLieu = UnityEngine.Random.Range(0, players.Count() - 1);
                    var lieutenant = players[chanceLieu];
                    lieutenant.RoleID = (int)RoleID.CDMLieutenant;
                    players.Remove(lieutenant);
                    nbLieu++;
                }
                foreach(var player in players)
                {
                    player.RoleID = (int)RoleID.CMDCadet;

                }
            }
        }
    }
}
