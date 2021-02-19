using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomClass
{
    public static class Extension
    {
        public static void SpawnUnRole(this Dictionary<Synapse.Api.Player, int> dictionaire, RoleType ancienRole, MoreClasseID nouveauRole, int spawnChance, int maxTotal = 0, int minActuClass = 0) 
        {
            var playerClass = dictionaire.Where(p => p.Value == (int)ancienRole);
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

        public static void SpawnUnRole(this PlayerSetClassEventArgs ev, RoleType ancienRole, MoreClasseID nouveauRole, int spawnChance, Func<int> MaxRole, int maxTotal = 0, int minActuClass = 0)
        {
            var playerClass = Server.Get.Players.Where(p => p.RoleID == (int)ancienRole);
            if (ev.Player.RoleID == (int)ancienRole && playerClass.Count() > minActuClass)
            {
                if (Server.Get.Players.Where(p => p.RoleID == (int)nouveauRole).Count() < maxTotal)
                {
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= spawnChance)
                    {
                        if (MaxRole.Invoke() > 0)
                        {
                            ev.Role = (RoleType)nouveauRole;
                        }
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
