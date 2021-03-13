using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_MTF
{
    [SynapseTeamInformation(
        ID = (int)TeamID.AndersneRobotic,
        Name = "HammerDown"
        )]
    public class HammerDownTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            Server.Get.Logger.Info(Plugin.Config.SpawnSize);
            Server.Get.Logger.Info(players == null);
            Server.Get.Logger.Info(players.Any(p => p == null));
            
            if (players.Count > Plugin.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Config.SpawnSize);

            int chance = UnityEngine.Random.Range(0, players.Count() - 1);
            var commander = players[chance];
            commander.RoleID = (int)RoleID.CDMCommandant;
            players.Remove(commander);

            int nbLieu = 0;
            do
            {
                int chanceLieu = UnityEngine.Random.Range(0, players.Count() - 1);
                var lieutenant = players[chanceLieu];
                lieutenant.RoleID = (int)RoleID.CDMLieutenant;
                players.Remove(lieutenant);
                nbLieu++;
            }
            while (players.Any() && nbLieu < Plugin.Config.MaxLieutenant);
            foreach(var player in players)
            {
                player.RoleID = (int)RoleID.CMDCadet;

            }
        }
    }
}
