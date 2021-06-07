using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.PlayerScript;

namespace VTCustomClass
{
    public static class Extension
    {
        public static T GetConfigValue<T>(this AbstractConfigSection Config, string Name, T defaultValue)
        {
            T value = defaultValue;
            if (Config != null && Config.GetType().GetField(Name) != null)
            {
                value = (T)Config.GetType().GetField(Name).GetValue(Config);
            }
            return value;
        }

        public static void SpawnRole(this Dictionary<Player, int> dictionaire, RoleType oldRole, BasePlayerScript playerScript)
        {
            
            var playerClass = dictionaire.Where(p => p.Value == (int)oldRole);
            int spawnChance = playerScript.GetConfig().GetConfigValue<int>("SpawnChance", 0);
            int maxTotal = playerScript.GetConfig().GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = playerScript.GetConfig().GetConfigValue<int>("RequiredPlayers", 0);
            
            if (playerClass.Count() > minActuClass)
            {
                if (Server.Get.Players.Where(p => p.RoleID == playerScript.GetRoleID()).Count() < maxTotal)
                {

                    while (maxTotal > 0 && playerClass.Any())
                    {
                        int chance = UnityEngine.Random.Range(1, 100);
                        if (chance <= spawnChance)
                        {
                            var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                            dictionaire[pair.Key] = playerScript.GetRoleID();
                            playerClass = dictionaire.Where(p => p.Value == (int)oldRole);
                            Data.PlayerRole[pair.Key] = playerScript.GetRoleID();
                        }
                        maxTotal--;
                    }
                }
            }
        }

        public static void SpawnRole(this PlayerSetClassEventArgs ev, RoleType oldRole, BasePlayerScript playerScript)
        {
            Player player = ev.Player;
            var playerClass = Server.Get.Players.Where(p => p.RoleID == (int)oldRole);
            int spawnChance = playerScript.GetConfig().GetConfigValue<int>("SpawnChance", 0);
            int maxRespawn = playerScript.GetConfig().GetConfigValue<int>("MaxRespawn", 0);
            int maxTotal = playerScript.GetConfig().GetConfigValue<int>("MaxAlive", 0);
            int minActuClass = playerScript.GetConfig().GetConfigValue<int>("RequiredPlayers", 0);
            int respawned = PluginClass.Plugin.RespawnedPlayer.ContainsKey((RoleID)playerScript.GetRoleID()) ? PluginClass.Plugin.RespawnedPlayer[(RoleID)playerScript.GetRoleID()] : 0;
            
            if (ev.Role == (RoleType)oldRole && playerClass.Count() >= minActuClass && maxRespawn > respawned)
            {
                if (Server.Get.Players.Where(p => p.RoleID == playerScript.GetRoleID()).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance && EventHandlers.RespawnPlayer.Contains(ev.Player))
                    {
                        EventHandlers.RespawnPlayer.Remove(ev.Player);
                        Player pl = ev.Player;
                        pl.RoleID = playerScript.GetRoleID();
                        PluginClass.Plugin.RespawnedPlayer[(RoleID)playerScript.GetRoleID()] = respawned + 1;
                        Data.PlayerRole[ev.Player] = playerScript.GetRoleID();
                    }
                }
            }
        }
        public static void SpawnSCPRole(this Dictionary<Player, int> dictionaire, BasePlayerScript playerScript)
        {
            int maxTotal = playerScript.GetConfig().GetConfigValue<int>("MaxAlive", 0);
            List<int> SCP = new List<int>() { (int)RoleType.Scp173, (int)RoleType.Scp079, (int)RoleType.Scp096,
                                              (int)RoleType.Scp106, (int)RoleType.Scp93953, (int)RoleType.Scp93989 };
            var playerClass = dictionaire.Where(p => SCP.Contains(p.Value));
            
            if (playerClass.Any())
            {
                if (maxTotal < 0 || Server.Get.Players.Where(p => p.RoleID == playerScript.GetRoleID()).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= playerScript.GetConfig().GetConfigValue<int>("SpawnChance", 0))
                    {
                        var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                        dictionaire[pair.Key] = playerScript.GetRoleID();
                        Data.PlayerRole[pair.Key] = playerScript.GetRoleID();
                    }
                }
            }
        }
    }
}