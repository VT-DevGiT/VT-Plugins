using VTCustomClass.PlayerScript;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;

namespace VTCustomClass
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnReSpawn;
            Server.Get.Events.Player.PlayerSetClassEvent += OnClass;
            Server.Get.Events.Server.TransmitPlayerDataEvent += OnTransmitPlayerData;
            Server.Get.Events.Round.RoundRestartEvent += () => Data.PlayerRole.Clear();
        }
        
        private void OnTransmitPlayerData(TransmitPlayerDataEventArgs ev)
        {
            if (ev.PlayerToShow == ev.Player)
                return;
            if (ev.PlayerToShow.RoleID == (int)RoleID.SCP966)
            {
                if (ev.Player.RoleID != (int)RoleID.Staff && ev.Player.RoleID != (int)RoleID.Spectator && ev.Player.TeamID != (int)TeamID.SCP)
                    ev.Invisible = true;
                else
                    ev.Invisible = false;
            }
            else if(ev.PlayerToShow.RoleID == (int)RoleID.Staff)
            {
                if (ev.Player.RoleID != (int)RoleID.Staff)
                    ev.Invisible = true;
                else
                    ev.Invisible = false;
            }
        }
        
        private void OnClass(PlayerSetClassEventArgs ev)
        {
            Data.PlayerRole[ev.Player] = (int)ev.Role;
            if (RespawnPlayer.Contains(ev.Player))
            {
                ev.SpawnRole(RoleType.ChaosRifleman, new CHILeaderScript());
                ev.SpawnRole(RoleType.ChaosRifleman, new CHIHackerScript());
                ev.SpawnRole(RoleType.ChaosRifleman, new CHIExpertPyrotechnieScript());
                ev.SpawnRole(RoleType.ChaosRifleman, new CHIInfirmierScript());
                ev.SpawnRole(RoleType.ChaosMarauder, new CHIKamikazeScript());
                ev.SpawnRole(RoleType.ChaosRepressor, new CHIMastodonteScript());
                ev.SpawnRole(RoleType.NtfPrivate, new CHISPYScript());
                ev.SpawnRole(RoleType.NtfPrivate, new NTFCommanderScript());
                ev.SpawnRole(RoleType.NtfPrivate, new NTFLieutenantScript());
                ev.SpawnRole(RoleType.NtfSergeant, new NTFExpertPyrotechnieScript());
                ev.SpawnRole(RoleType.NtfSergeant, new NTFExpertReconfinementScript());
                ev.SpawnRole(RoleType.NtfSergeant, new NTFInfirmierScript());
                ev.SpawnRole(RoleType.NtfSergeant, new NTFVirologueScript());
                RespawnPlayer.Remove(ev.Player);
            }
        }

        public static List<Player> RespawnPlayer = new List<Player>();

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            RespawnPlayer.Clear();
            Plugin.Instance.RespawnedPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
        {
            if (ev.SpawnPlayers != null)
            {
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new ConciergeScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new CHIIntrusScript());
                ev.SpawnPlayers.SpawnRole(RoleType.Scientist, new ScientifiqueSuperviseurScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new FoundationUTRScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new GardeSuperviseurScript());
                ev.SpawnPlayers.SpawnRole(RoleType.FacilityGuard, new TechnicienScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new DirecteurSiteScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new ZoneManagerScript());
                ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new GardePrisonScript());
                    
                if (ev.SpawnPlayers.Count() > 25)
                {
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new SCP008Script());
                    ev.SpawnPlayers.SpawnRole(RoleType.ClassD, new SCP966cript());
                }
                else
                {
                    ev.SpawnPlayers.SpawnSCPRole(new SCP008Script());
                    ev.SpawnPlayers.SpawnSCPRole(new SCP966cript());
                }
                foreach (var player in ev.SpawnPlayers.Keys)
                {
                    Data.PlayerRole[player] = ev.SpawnPlayers[player];
                }
            }
        }
    }
}
