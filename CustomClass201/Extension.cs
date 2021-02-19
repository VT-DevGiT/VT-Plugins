using Synapse;
using System.Collections.Generic;
using System.Linq;

namespace CustomClass
{
    public static class Extension
    {
        public static void AddPossibleRole(this List<MoreClasseID> list, int chance, int spawnChance, MoreClasseID role)
        {
            if (chance <= spawnChance)
            {
                list.Add(role);
            }
        }

        public static void AssignRole(this Dictionary<Synapse.Api.Player, int> dictionaire, KeyValuePair<Synapse.Api.Player, int> pair, List<MoreClasseID> listPossible)
        {
            if (listPossible.Any())
            {
                dictionaire[pair.Key] = (int)listPossible[UnityEngine.Random.Range(0, listPossible.Count - 1)];
            }
        }

        public static void SpawnUnRole(this Dictionary<Synapse.Api.Player, int> dictionaire, RoleType ancienRole, MoreClasseID nouveauRole, int spawnChance, int maxSpawnRole = -1, int maxTotal = 0, int minActuClass = 0) 
        {
            var playerClass = dictionaire.Where(p => p.Value == (int)ancienRole);
            if (playerClass.Count() > minActuClass)
            {
                if (maxSpawnRole < 0 || Server.Get.Players.Where(p => p.RoleID == (int)nouveauRole).Count() < System.Math.Max(maxSpawnRole, maxTotal))
                {
                    int max = maxSpawnRole;
                    while (max > 0 && playerClass.Any())
                    {
                        int chance = UnityEngine.Random.Range(1, 100);
                        if (chance <= spawnChance)
                        {
                            var pair = playerClass.ElementAt(UnityEngine.Random.Range(0, playerClass.Count() - 1));
                            dictionaire[pair.Key] = (int)nouveauRole;
                            playerClass = dictionaire.Where(p => p.Value == (int)ancienRole);
                        }
                        max--;
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
