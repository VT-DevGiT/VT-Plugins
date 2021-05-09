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
        ID = (int)TeamID.AND,
        Name = "AndersneRobtic"
        )]
    public class AndersonRoboticTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            int UTRSpawn = 0;
            if (players.Count > Plugin.Config.SpawnSizeAnderson)
                players = players.GetRange(0, Plugin.Config.SpawnSizeAnderson);

            if (players.Count > 0)
            { 
                players[0].RoleID = (int)RoleID.AndersonLeader;
                players.Remove(players[0]);
            }

            if (Server.Get.RoleManager.IsIDRegistered((int)RoleID.AndersonUTRheavy) && Server.Get.RoleManager.IsIDRegistered((int)RoleID.AndersonUTRlight))
                foreach (var ply in players)
                {
                    if (UTRSpawn > 3)
                    {
                        ply.RoleID = (int)RoleID.AndersonUTRlight;
                        UTRSpawn++;
                    }
                    else if (UTRSpawn == 3)
                    {
                        ply.RoleID = (int)RoleID.AndersonUTRheavy;
                        UTRSpawn++;
                    }
                    else ply.RoleID = (int)RoleID.AndersonEngineer;
                    players.Remove(ply);
                }
            else foreach (var ply in players)
            {
                ply.RoleID = (int)RoleID.AndersonEngineer;
                players.Remove(ply);
            }
        }
    }
}
