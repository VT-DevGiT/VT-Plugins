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
        }

        private List<int> ListTeamRSC = new List<int>() { (int)MoreClasseID.Concierge, (int)MoreClasseID.ScientifiqueSuperviseur, (int)MoreClasseID.DirecteurSite };

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            Server.Get.Logger.Info($"{ev.Players} - {ev.Team} {ev.TeamID}");
            ev.Players.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHILeader, PluginClass.ConfigCHILeader.SpawnChance, 1);
            ev.Players.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIMastodonte, PluginClass.ConfigCHIMastondonte.SpawnChance, 2, 3, 4);
            SpawnNtf(ev);
            SpawnCHI(ev);
        }

        private void OnSpawn(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            Synapse.Api.Logger.Get.Info("OnSpawn");
            if (ev.SpawnPlayers != null)
            {
                // Role avec nombre limite
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigDirecteurSite.SpawnChance, 1, 0 , 1);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.Concierge, PluginClass.ConfigConcierge.SpawnChance, 2, 0, 5);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP507, PluginClass.ConfigSCP507.SpawnChance, 1, 0, 5);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP999, PluginClass.ConfigSCP999.SpawnChance, 1, 0, 5);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.CHIIntrus, PluginClass.ConfigCHIntrus.SpawnChance, 1);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur.SpawnChance, 1, 0, 2);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.GardeSuperviseur, PluginClass.ConfigGardeSuperviseur.SpawnChance, 1, 0, 2);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.Technicien, PluginClass.ConfigTechnicien.SpawnChance, 1, 0, 2);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.UTR, PluginClass.ConfigRoboticTaticalUnity.SpawnChance, 1, 0, 3);
                ev.SpawnPlayers.SpawnUnRole(RoleType.NtfCadet, MoreClasseID.CHISPY, PluginClass.ConfigCHISPY.SpawnChance, 1, 2, 5);
                if (ev.SpawnPlayers.Where(p => p.Value == (int)RoleType.Scientist).Count() > 1 || Server.Get.Players.Any(p => p.RoleID == (int)RoleType.Scientist))
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur.SpawnChance);
                }
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP008, PluginClass.ConfigSCP008.SpawnChance, 1, 1, 7);
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP966, PluginClass.ConfigSCP966.SpawnChance, 1, 2, 7);
                }
                else
                {
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP008, PluginClass.ConfigSCP008.SpawnChance, 1);
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP966, PluginClass.ConfigSCP966.SpawnChance, 2);
                }
            }
        }

        private void SpawnCHI(TeamRespawnEventArgs ev)
        {
            var playerClass = ev.Players.Where(p => p.RoleID == (int)RoleType.ChaosInsurgency);
            foreach (var player in ev.Players)
            {
                int chance = UnityEngine.Random.Range(1, 100);
                var listPossible = new List<MoreClasseID>();
                listPossible.AddPossibleRole(chance, PluginClass.ConfigCHIExpertPyrotechnie.SpawnChance, MoreClasseID.CHIExpertPyrotechnieIC);
                listPossible.AddPossibleRole(chance, PluginClass.ConfigCHIHacker.SpawnChance, MoreClasseID.CHIHacker);
                listPossible.AddPossibleRole(chance, PluginClass.ConfigCHIKamikaze.SpawnChance, MoreClasseID.CHIKamikaze);
                player.AssignRole(listPossible);

            }
        }

        private void SpawnNtf(TeamRespawnEventArgs ev)
        {
            var playerClass = ev.Players.Where(p => p.RoleID == (int)RoleType.NtfLieutenant);
            foreach (var player in ev.Players)
            {
                int chance = UnityEngine.Random.Range(1, 100);
                var listPossible = new List<MoreClasseID>();
                listPossible.AddPossibleRole(chance, PluginClass.ConfigNTFExpertPyrotechnie.SpawnChance, MoreClasseID.NTFExpertPyrotechnie);
                listPossible.AddPossibleRole(chance, PluginClass.ConfigNTFExpertReconfinement.SpawnChance, MoreClasseID.NTFExpertReconfinement);
                listPossible.AddPossibleRole(chance, PluginClass.ConfigNTFInfirmier.SpawnChance, MoreClasseID.NTFInfirmier);
                listPossible.AddPossibleRole(chance, PluginClass.ConfigNTFVirologue.SpawnChance, MoreClasseID.NTFVirologue);
                player.AssignRole(listPossible);

            }
        }
    }
}
