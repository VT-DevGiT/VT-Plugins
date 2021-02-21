using CustomClass.PlayerScript;
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
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHILeader, PluginClass.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIHacker, PluginClass.ConfigCHIHacker);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIExpertPyrotechnie, PluginClass.ConfigCHIExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIKamikaze, PluginClass.ConfigCHIKamikaze);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, MoreClasseID.CHIMastodonte, PluginClass.ConfigCHIMastondonte);
                ev.SpawnUnRole(RoleType.NtfCadet, MoreClasseID.CHISPY, PluginClass.ConfigCHISPY);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.CHILeader, PluginClass.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFExpertPyrotechnie, PluginClass.ConfigNTFExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFExpertReconfinement, PluginClass.ConfigNTFExpertReconfinement);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFInfirmier, PluginClass.ConfigNTFInfirmier);
                ev.SpawnUnRole(RoleType.NtfLieutenant, MoreClasseID.NTFVirologue, PluginClass.ConfigNTFVirologue);
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
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.DirecteurSite, PluginClass.ConfigDirecteurSite);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.Concierge, PluginClass.ConfigConcierge);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP507, PluginClass.ConfigSCP507);
                //ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP999, PluginClass.ConfigSCP999); choisire une bonne taile 
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.CHIIntrus, PluginClass.ConfigCHIntrus);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.GardeSuperviseur, PluginClass.ConfigGardeSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.Technicien, PluginClass.ConfigTechnicien);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, MoreClasseID.UTR, PluginClass.ConfigRoboticTaticalUnity);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, MoreClasseID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP008, PluginClass.ConfigSCP008);
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, MoreClasseID.SCP966, PluginClass.ConfigSCP966);
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
