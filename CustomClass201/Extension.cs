using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomClass
{
    public static class Extension
    {
        public static  T GetConfigValue<T>(this AbstractConfigSection Config,string Name, T defaultValue)
        {
            T value = defaultValue;
            if (Config != null && Config.GetType().GetField(Name) != null)
            {
                value = (T)Config.GetType().GetField(Name).GetValue(Config);
            }
            return value;
        }

        public static Dictionary<MoreClasseID, int> RespawnedPlayer = new Dictionary<MoreClasseID, int>();
        public static void SpawnUnRole(this Dictionary<Synapse.Api.Player, int> dictionaire, RoleType ancienRole, MoreClasseID nouveauRole, AbstractConfigSection config) 
        {
            var playerClass = dictionaire.Where(p => p.Value == (int)ancienRole);
            int spawnChance = config.GetConfigValue<int>("SpawnChance", 0);
            int maxTotal = config.GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = config.GetConfigValue<int>("RequiredPlayers", 0);
            if (playerClass.Count() > minActuClass)
            {
                if (Server.Get.Players.Where(p => p.RoleID == (int)nouveauRole).Count() <  maxTotal)
                {
                    
                    while (maxTotal > 0 && playerClass.Any())
                    {
                        int chance = UnityEngine.Random.Range(1, 100);
                        if (chance <= spawnChance)
                        {
                            var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                            dictionaire[pair.Key] = (int)nouveauRole;
                            playerClass = dictionaire.Where(p => p.Value == (int)ancienRole);
                        }
                        maxTotal--;
                    }
                }
            }
        }

        public static void SpawnUnRole(this PlayerSetClassEventArgs ev, RoleType ancienRole, MoreClasseID nouveauRole, AbstractConfigSection config)
        {
            var player = ev.Player;
            var playerClass = Server.Get.Players.Where(p => p.RoleID == (int)ancienRole);
            int spawnChance = config.GetConfigValue<int>("SpawnChance", 0);
            int maxRespawn = config.GetConfigValue<int>("MaxRespawn", 0);
            int maxTotal = config.GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = config.GetConfigValue<int>("RequiredPlayers", 0);
            int respawned = RespawnedPlayer.ContainsKey(nouveauRole) ? RespawnedPlayer[nouveauRole] : 0;
            if (ev.Role == (RoleType)ancienRole && playerClass.Count() >= minActuClass && maxRespawn > respawned)
            {
                if (Server.Get.Players.Where(p => p.RoleID == (int)nouveauRole).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance)
                    {
                        Player pl = ev.Player;
                        Timing.CallDelayed(2f, () =>
                        {
                            pl.RoleID = (int)nouveauRole;
                            PluginClass.Plugin.PlayerRole[ev.Player] =(int) nouveauRole;

                        });
                        RespawnedPlayer[nouveauRole] = respawned + 1;
                    }
                }
            }
        }


        public static void SpawnUnRoleSCP(this Dictionary<Synapse.Api.Player, int> dictionaire, MoreClasseID nouveauRole, int spawnChance, int maxTotal = -1)
        {
            var playerClass = dictionaire.Where(p => SCP.Contains(p.Value));
            if (playerClass.Any())
            {
                if (maxTotal < 0 || Server.Get.Players.Where(p => p.RoleID == (int)nouveauRole).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance)
                    {
                        var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                        dictionaire[pair.Key] = (int)nouveauRole;
                    }
                }
            }
        }

        static List<int> SCP = new List<int> { (int)RoleType.Scp049, (int)RoleType.Scp096, (int)RoleType.Scp096, (int)RoleType.Scp079, (int)RoleType.Scp106, (int)RoleType.Scp173 };

    }
}
