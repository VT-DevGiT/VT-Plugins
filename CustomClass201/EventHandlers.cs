using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;

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
            VT_Referance.Variable.Dictionary.PlayerRole[ev.Player] = (int)ev.Role;
            if (RespawnPlayer.Contains(ev.Player))
            {
                ev.SpawnUnRole(RoleType.ChaosInsurgency, RoleID.CHILeader, PluginClass.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, RoleID.CHIHacker, PluginClass.ConfigCHIHacker);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, RoleID.CHIExpertPyrotechnie, PluginClass.ConfigCHIExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, RoleID.CHIKamikaze, PluginClass.ConfigCHIKamikaze);
                ev.SpawnUnRole(RoleType.ChaosInsurgency, RoleID.CHIMastodonte, PluginClass.ConfigCHIMastondonte);
                ev.SpawnUnRole(RoleType.NtfCadet, RoleID.CHISpy, PluginClass.ConfigCHISPY);
                ev.SpawnUnRole(RoleType.NtfLieutenant, RoleID.CHILeader, PluginClass.ConfigCHILeader);
                ev.SpawnUnRole(RoleType.NtfLieutenant, RoleID.NTFExpertPyrotechnie, PluginClass.ConfigNTFExpertPyrotechnie);
                ev.SpawnUnRole(RoleType.NtfLieutenant, RoleID.NTFExpertReconfinement, PluginClass.ConfigNTFExpertReconfinement);
                ev.SpawnUnRole(RoleType.NtfLieutenant, RoleID.NTFInfirmier, PluginClass.ConfigNTFInfirmier);
                ev.SpawnUnRole(RoleType.NtfLieutenant, RoleID.NTFVirologue, PluginClass.ConfigNTFVirologue);
                RespawnPlayer.Remove(ev.Player);
            }
        }

        public static List<Player> RespawnPlayer = new List<Player>();

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            RespawnPlayer.Clear();
            PluginClass.Plugin.RespawnedPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
        {
            if (ev.SpawnPlayers != null)
            {
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.DirecteurSite, PluginClass.ConfigDirecteurSite);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.Concierge, PluginClass.ConfigConcierge);
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.SCP507, PluginClass.ConfigSCP507);
                //ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.SCP999, PluginClass.ConfigSCP999); choisire une bonne taile 
                ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.CHIIntrus, PluginClass.ConfigCHIntrus);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, RoleID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, RoleID.GardeSuperviseur, PluginClass.ConfigGardeSuperviseur);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, RoleID.Technicien, PluginClass.ConfigTechnicien);
                ev.SpawnPlayers.SpawnUnRole(RoleType.FacilityGuard, RoleID.FoundationUTR, PluginClass.ConfigFoundationUTR);
                ev.SpawnPlayers.SpawnUnRole(RoleType.Scientist, RoleID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.SCP008, PluginClass.ConfigSCP008);
                    ev.SpawnPlayers.SpawnUnRole(RoleType.ClassD, RoleID.SCP966, PluginClass.ConfigSCP966);
                }
                else
                {
                    ev.SpawnPlayers.SpawnUnRoleSCP(RoleID.SCP008, PluginClass.ConfigSCP008.SpawnChance, PluginClass.ConfigSCP008.MaxAlive);
                    ev.SpawnPlayers.SpawnUnRoleSCP(RoleID.SCP966, PluginClass.ConfigSCP966.SpawnChance, PluginClass.ConfigSCP966.MaxAlive);
                }
                foreach (var player in ev.SpawnPlayers.Keys)
                {
                    Dictionary.PlayerRole[player] = ev.SpawnPlayers[player];
                }
            }
        }


    }
}
