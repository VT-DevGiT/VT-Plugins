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
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHILeader, Plugin.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIHacker, Plugin.ConfigCHIHacker);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIExpertPyrotechnie, Plugin.ConfigCHIExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIKamikaze, Plugin.ConfigCHIKamikaze);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIMastodonte, Plugin.ConfigCHIMastondonte);
                ev.SpawnUnRole(RoleType.NtfCadet, MoreClasseID.CHISPY, Plugin.ConfigCHISPY);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.CHILeader, Plugin.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFExpertPyrotechnie, Plugin.ConfigNTFExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFExpertReconfinement, Plugin.ConfigNTFExpertReconfinement);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFInfirmier, Plugin.ConfigNTFInfirmier);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFVirologue, Plugin.ConfigNTFVirologue);
                RespawnPlayer.Remove(ev.Player);
            }
        }

        public List<Player> RespawnPlayer = new List<Player>();

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            RespawnPlayer.Clear();
            Extension.RespawnedPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnSpawn(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            if (ev.SpawnPlayers != null)
            {
                // Role avec nombre limite
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, Plugin.ConfigDirecteurSite);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.Concierge, Plugin.ConfigConcierge);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP507, Plugin.ConfigSCP507);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP999, Plugin.ConfigSCP999);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.CHIIntrus, Plugin.ConfigCHIntrus);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, Plugin.ConfigScientifiqueSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.GardeSuperviseur, Plugin.ConfigGardeSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.Technicien, Plugin.ConfigTechnicien);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.UTR, Plugin.ConfigRoboticTaticalUnity);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, Plugin.ConfigScientifiqueSuperviseur);
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP008, Plugin.ConfigSCP008);
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP966, Plugin.ConfigSCP966);
                }
                else
                {
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP008, Plugin.ConfigSCP008.SpawnChance, Plugin.ConfigSCP008.MaxAlive);
                    ev.SpawnPlayers.SpawnUnRoleSCP(MoreClasseID.SCP966, Plugin.ConfigSCP966.SpawnChance, Plugin.ConfigSCP966.MaxAlive);
                }
            }
        }


    }
}
