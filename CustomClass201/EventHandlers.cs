using CustomClass.PlayerScript;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomClass
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnReSpawn;
            Server.Get.Events.Player.PlayerSetClassEvent += OnClass;
        }

        private void OnClass(PlayerSetClassEventArgs ev)
        {
            if (RespawnPlayer.Contains(ev.Player))
            {
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHILeader.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHILeader.GetType()), PluginClass.ConfigCHILeader.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHIKamikaze.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHIKamikaze.GetType()), PluginClass.ConfigCHIKamikaze.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHIHacker.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHIHacker.GetType()), PluginClass.ConfigCHIHacker.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHIExpertPyrotechnie.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHIExpertPyrotechnie.GetType()), PluginClass.ConfigCHIExpertPyrotechnie.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHIMastondonte.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHIMastondonte.GetType()), PluginClass.ConfigCHIMastondonte.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigCHISPY.SpawnChance, () => GetMaxValue(PluginClass.ConfigCHISPY.GetType()), PluginClass.ConfigCHISPY.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigNTFExpertPyrotechnie.SpawnChance, () => GetMaxValue(PluginClass.ConfigNTFExpertPyrotechnie.GetType()), PluginClass.ConfigNTFExpertPyrotechnie.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigNTFExpertReconfinement.SpawnChance, () => GetMaxValue(PluginClass.ConfigNTFExpertReconfinement.GetType()), PluginClass.ConfigNTFExpertReconfinement.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigNTFInfirmier.SpawnChance, () => GetMaxValue(PluginClass.ConfigNTFInfirmier.GetType()), PluginClass.ConfigNTFInfirmier.RequiredPlayers);
                ev.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigNTFVirologue.SpawnChance, () => GetMaxValue(PluginClass.ConfigNTFVirologue.GetType()), PluginClass.ConfigNTFVirologue.RequiredPlayers);
            }
        }

        private Dictionary<Type, int> dictMaxPossible = new Dictionary<Type, int>();
        private int GetMaxValue(Type type)
        {
            if (dictMaxPossible.ContainsKey(type))
            {
                int result = dictMaxPossible[type];
                if (result > 0)
                {
                    dictMaxPossible[type] = dictMaxPossible[type] - 1;
                }
                return result;
            }
            return 0;
        }

        private List<Player> RespawnPlayer = new List<Player>();
        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            // Reset du max
            dictMaxPossible[PluginClass.ConfigCHILeader.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigCHIKamikaze.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigCHIHacker.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigCHIExpertPyrotechnie.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigCHIMastondonte.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigCHISPY.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigNTFExpertPyrotechnie.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigNTFExpertReconfinement.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigNTFInfirmier.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            dictMaxPossible[PluginClass.ConfigNTFVirologue.GetType()] = PluginClass.ConfigCHILeader.MaxRespawn;
            RespawnPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnSpawn(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            Synapse.Api.Logger.Get.Info("OnSpawn");
            if (ev.SpawnPlayers != null)
            {
                // Role avec nombre limite
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigDirecteurSite.SpawnChance, PluginClass.ConfigDirecteurSite.MaxAlive, PluginClass.ConfigDirecteurSite.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.Concierge, PluginClass.ConfigConcierge.SpawnChance, PluginClass.ConfigConcierge.MaxAlive, PluginClass.ConfigConcierge.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP507, PluginClass.ConfigSCP507.SpawnChance, PluginClass.ConfigSCP507.MaxAlive, PluginClass.ConfigSCP507.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP999, PluginClass.ConfigSCP999.SpawnChance, PluginClass.ConfigSCP999.MaxAlive, PluginClass.ConfigSCP999.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.CHIIntrus, PluginClass.ConfigCHIntrus.SpawnChance, PluginClass.ConfigCHIntrus.MaxAlive, PluginClass.ConfigCHIntrus.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur.SpawnChance, PluginClass.ConfigScientifiqueSuperviseur.MaxAlive, PluginClass.ConfigScientifiqueSuperviseur.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.GardeSuperviseur, PluginClass.ConfigGardeSuperviseur.SpawnChance, PluginClass.ConfigGardeSuperviseur.MaxAlive, PluginClass.ConfigGardeSuperviseur.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.Technicien, PluginClass.ConfigTechnicien.SpawnChance, PluginClass.ConfigTechnicien.MaxAlive, PluginClass.ConfigTechnicien.RequiredPlayers);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.UTR, PluginClass.ConfigRoboticTaticalUnity.SpawnChance, PluginClass.ConfigRoboticTaticalUnity.MaxAlive, PluginClass.ConfigRoboticTaticalUnity.RequiredPlayers);
                
                if (ev.SpawnPlayers.Where(p => p.Value == (int)RoleType.Scientist).Count() > 1 || Server.Get.Players.Any(p => p.RoleID == (int)RoleType.Scientist))
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur.SpawnChance);
                }
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP008, PluginClass.ConfigSCP008.SpawnChance, PluginClass.ConfigSCP008.MaxAlive, PluginClass.ConfigSCP008.RequiredPlayers);
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP966, PluginClass.ConfigSCP966.SpawnChance, PluginClass.ConfigSCP966.MaxAlive, PluginClass.ConfigSCP966.RequiredPlayers);
                }
                else
                {
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP008, PluginClass.ConfigSCP008.SpawnChance, PluginClass.ConfigSCP008.MaxAlive);
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP966, PluginClass.ConfigSCP966.SpawnChance, PluginClass.ConfigSCP966.MaxAlive);
                }
            }
        }


    }
}
