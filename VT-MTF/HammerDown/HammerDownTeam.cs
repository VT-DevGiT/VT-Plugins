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
                //random Commander
                int chance = players.Count() > 1? UnityEngine.Random.Range(0, players.Count() - 1) : 0;
                var commander = players[chance];
                commander.RoleID = (int)RoleID.CdmCommander;
                players.Remove(commander);

                // Spawn lieutenant
                int nbLieu = 0;
                while (players.Any() && nbLieu < Plugin.ConfigHammerDownLieutenant.MaxRespawn)
                {
                    int chanceLieu = UnityEngine.Random.Range(0, players.Count() - 1);
                    var lieutenant = players[chanceLieu];
                    lieutenant.RoleID = (int)RoleID.CdmLieutenant;
                    players.Remove(lieutenant);
                    nbLieu++;
                }

                //For all last player
                foreach(var player in players)
                {
                    player.RoleID = (int)RoleID.CdmCadet;
                    players.Remove(player);
                }
            }
        }
    }
}
