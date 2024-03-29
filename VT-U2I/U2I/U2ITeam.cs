﻿using Respawning;
using Respawning.NamingRules;
using Synapse.Api;
using Synapse.Api.Teams;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VT_U2I
{
    [SynapseTeamInformation(
        ID = (int)TeamID.U2I,
        Name = "U2I"
        )]
    public class U2ITeam : SynapseTeam
    {
        public string CurentUnitName { get; set; }

        public string GetNewUniteName()
        {
            string uniteName = Plugin.Instance.Config.UnitName;
            CurentUnitName = Regex.Replace(uniteName, "%RandomName%", VtController.Get.Team.GenerateNtfUnitName(), RegexOptions.IgnoreCase);
            return CurentUnitName;
        }

        public string GetSpawnAnnonce(string uniteName)
        {
            string message = Plugin.Instance.Config.CassieSpawn;
            message = Regex.Replace(message, "%UnitName%", uniteName, RegexOptions.IgnoreCase);
            return message;
        }

        public override void Spawn(List<Player> players)
        {
            if (players == null || !players.Any())
                return;
            Logger.Get.Info("1");
            Player playerChef = players.FirstOrDefault(p => Plugin.Instance.Config.SpawnNeedRank.Contains(p.RankName));
            Logger.Get.Info("2");

            if (Plugin.Instance.Config.SpawnSize > 0 && players.Count > Plugin.Instance.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Instance.Config.SpawnSize);
            Logger.Get.Info("3");

            if (!players.Any())
                return;
            Logger.Get.Info("4");

            RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit()
            {
                SpawnableTeam = 2,
                UnitName = GetNewUniteName()
            });
            Logger.Get.Info("5");
            if (!string.IsNullOrWhiteSpace(Plugin.Instance.Config.CassieSpawn))
            {
                string SpawnCassie = GetSpawnAnnonce(CurentUnitName);
                Map.Get.GlitchedCassie(SpawnCassie);
            }
            Logger.Get.Info("6");
            if (playerChef != null)
            {
                players.Remove(playerChef);
                playerChef.RoleID = (int)RoleID.U2IAgentLiaison;
                playerChef.UnitName = CurentUnitName;
            }
            Logger.Get.Info("7");
            int i = 0;
            foreach (var player in players)
            {
                player.RoleID = (int)RoleID.U2IAgent;
                Logger.Get.Info($"8.{i}.1");
                player.UnitName = CurentUnitName;
                Logger.Get.Info($"8.{i++}.2");
            }//Bug here
        }
    }
}
