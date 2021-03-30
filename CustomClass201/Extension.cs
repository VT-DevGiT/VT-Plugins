using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;

namespace CustomClass
{
    public static class Extension
    {
        public static void SpawnRole(this Dictionary<Player, int> dictionaire, RoleType oldRole, RoleID newRole, AbstractConfigSection config) 
        {
            var playerClass = dictionaire.Where(p => p.Value == (int)oldRole);
            int spawnChance = config.GetConfigValue<int>("SpawnChance", 0);
            int maxTotal = config.GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = config.GetConfigValue<int>("RequiredPlayers", 0);
            if (playerClass.Count() > minActuClass)
            {
                if (Server.Get.Players.Where(p => p.RoleID == (int)newRole).Count() <  maxTotal)
                {
                    
                    while (maxTotal > 0 && playerClass.Any())
                    {
                        int chance = UnityEngine.Random.Range(1, 100);
                        if (chance <= spawnChance)
                        {
                            var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                            dictionaire[pair.Key] = (int)newRole;
                            playerClass = dictionaire.Where(p => p.Value == (int)oldRole);
                        }
                        maxTotal--;
                    }
                }
            }
        }

        public static void SpawnRole(this PlayerSetClassEventArgs ev, RoleType oldRole, RoleID newRole, AbstractConfigSection config)
        {
            Player player = ev.Player;
            var playerClass = Server.Get.Players.Where(p => p.RoleID == (int)oldRole);
            int spawnChance = config.GetConfigValue<int>("SpawnChance", 0);
            int maxRespawn = config.GetConfigValue<int>("MaxRespawn", 0);
            int maxTotal = config.GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = config.GetConfigValue<int>("RequiredPlayers", 0);
            int respawned = PluginClass.Plugin.RespawnedPlayer.ContainsKey(newRole) ? PluginClass.Plugin.RespawnedPlayer[newRole] : 0;
            if (ev.Role == (RoleType)oldRole && playerClass.Count() >= minActuClass && maxRespawn > respawned)
            {
                if (Server.Get.Players.Where(p => p.RoleID == (int)newRole).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance && EventHandlers.RespawnPlayer.Contains(ev.Player))
                    {
                        EventHandlers.RespawnPlayer.Remove(ev.Player);
                        Player pl = ev.Player;
                        pl.RoleID = (int)newRole;
                        Dictionary.PlayerRole[ev.Player] = (int)newRole;
                        PluginClass.Plugin.RespawnedPlayer[newRole] = respawned + 1;
                    }
                }
            }
        }
        public static void SpawnSCPRole(this Dictionary<Player, int> dictionaire, RoleID newRole, int spawnChance, int maxTotal = -1)
        {
            List<int> SCP = new List<int>() { (int)RoleType.Scp173, (int)RoleType.Scp079, (int)RoleType.Scp096, 
                (int)RoleType.Scp106, (int)RoleType.Scp93953, (int)RoleType.Scp93989 };
            var playerClass = dictionaire.Where(p => SCP.Contains(p.Value));
            if (playerClass.Any())
            {
                if (maxTotal < 0 || Server.Get.Players.Where(p => p.RoleID == (int)newRole).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance)
                    {
                        var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                        dictionaire[pair.Key] = (int)newRole;
                    }
                }
            }
        }

        public static T GetConfigValue<T>(this AbstractConfigSection Config, string Name, T defaultValue)
        {
            T value = defaultValue;
            if (Config != null && Config.GetType().GetField(Name) != null)
            {
                value = (T)Config.GetType().GetField(Name).GetValue(Config);
            }
            return value;
        }

    }
}