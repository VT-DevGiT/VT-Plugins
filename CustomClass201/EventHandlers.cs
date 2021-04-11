using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
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
            Server.Get.Logger.Info(ev.Role);
            ev.Player.SynapseGroup.Permissions.Remove("synapse.see.invisible");
            if (RespawnPlayer.Contains(ev.Player))
            {
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiLeader, PluginClass.ConfigCHILeader);
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiHacker, PluginClass.ConfigCHIHacker);
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiExpertPyrotechnie, PluginClass.ConfigCHIExpertPyrotechnie);
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiKamikaze, PluginClass.ConfigCHIKamikaze);
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiMastodonte, PluginClass.ConfigCHIMastondonte);
                ev.SpawnRole(RoleType.ChaosInsurgency, RoleID.ChiInfirmier, PluginClass.ConfigCHIInfirmier);
                ev.SpawnRole(RoleType.NtfCadet, RoleID.ChiSpy, PluginClass.ConfigCHISPY);
                ev.SpawnRole(RoleType.NtfCadet, RoleID.NtfCapitaine, PluginClass.ConfigNTFCapitaine);
                ev.SpawnRole(RoleType.NtfCadet, RoleID.NtfSergent, PluginClass.ConfigNTFSergent);
                ev.SpawnRole(RoleType.NtfLieutenant, RoleID.NtfExpertPyrotechnie, PluginClass.ConfigNTFLieutenantColonel);
                ev.SpawnRole(RoleType.NtfLieutenant, RoleID.NtfExpertReconfinement, PluginClass.ConfigNTFExpertReconfinement);
                ev.SpawnRole(RoleType.NtfLieutenant, RoleID.NtfInfirmier, PluginClass.ConfigNTFInfirmier);
                ev.SpawnRole(RoleType.NtfLieutenant, RoleID.NtfVirologue, PluginClass.ConfigNTFVirologue);
                RespawnPlayer.Remove(ev.Player);
            }
            VT_Referance.Variable.Dictionary.PlayerRole[ev.Player] = (int)ev.Role;
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
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.DirecteurSite, PluginClass.ConfigDirecteurSite);
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.Concierge, PluginClass.ConfigConcierge);
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.SCP507, PluginClass.ConfigSCP507);
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.ChiIntrus, PluginClass.ConfigCHIntrus);
                ev.SpawnPlayers.SpawnRole(RoleType.Scientist, RoleID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, RoleID.GardeSuperviseur, PluginClass.ConfigGardeSuperviseur);
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, RoleID.Technicien, PluginClass.ConfigTechnicien);
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, RoleID.FoundationUTR, PluginClass.ConfigFoundationUTR);
                ev.SpawnPlayers.SpawnRole(RoleType.Scientist, RoleID.ScientifiqueSuperviseur, PluginClass.ConfigScientifiqueSuperviseur);
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.SCP008, PluginClass.ConfigSCP008);
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, RoleID.SCP966, PluginClass.ConfigSCP966);
                }
                else
                {
                    ev.SpawnPlayers.SpawnSCPRole(RoleID.SCP008, PluginClass.ConfigSCP008.SpawnChance, PluginClass.ConfigSCP008.MaxAlive);
                    ev.SpawnPlayers.SpawnSCPRole(RoleID.SCP966, PluginClass.ConfigSCP966.SpawnChance, PluginClass.ConfigSCP966.MaxAlive);
                }
                foreach (var player in ev.SpawnPlayers.Keys)
                {
                    Dictionary.PlayerRole[player] = ev.SpawnPlayers[player];
                }
            }
        }


    }
}
