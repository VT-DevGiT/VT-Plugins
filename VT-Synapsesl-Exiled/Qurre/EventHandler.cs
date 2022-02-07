using Synapse.Api.Events.SynapseEventArguments;
using System;
using Synapse;

namespace VT_MultieLoder.Qurre
{
    public class EventHandler
    {
        public EventHandler()
        {
            Server.Get.Events.Map.LCZDecontaminationEvent += OnLCZDecontaminationEvent;
            Server.Get.Events.Map.Scp914ActivateEvent += OnScp914ActivateEvent;
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Map.WarheadDetonationEvent += OnWarheadDetonationEvent;
            Server.Get.Events.Player.LoadComponentsEvent += OnLoadComponentsEvent;
            Server.Get.Events.Player.PlayerBanEvent += OnPlayerBanEvent;
            Server.Get.Events.Player.PlayerChangeItemEvent += OnPlayerChangeItemEvent;
            //Server.Get.Events.Player.PlayerConnectWorkstationEvent += OnPlayerConnectWorkstationEvent;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnPlayerCuffTargetEvent;
            Server.Get.Events.Player.PlayerDamageEvent += OnPlayerDamageEvent;
            Server.Get.Events.Player.PlayerDamagePermissionEvent += OnPlayerDamagePermissionEvent;
            Server.Get.Events.Player.PlayerDeathEvent += OnPlayerDeathEvent;
            Server.Get.Events.Player.PlayerDropAmmoEvent += OnPlayerDropAmmoEvent;
            Server.Get.Events.Player.PlayerDropItemEvent += OnPlayerDropItemEvent;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnPlayerEnterFemurEvent;
            Server.Get.Events.Player.PlayerEscapesEvent += OnPlayerEscapesEvent;
            Server.Get.Events.Player.PlayerGeneratorInteractEvent += OnPlayerGeneratorInteractEvent;
            Server.Get.Events.Player.PlayerHealEvent += OnPlayerHealEvent;
            Server.Get.Events.Player.PlayerItemUseEvent += OnPlayerItemUseEvent;
            Server.Get.Events.Player.PlayerJoinEvent += OnPlayerJoinEvent;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnPlayerKeyPressEvent;
            Server.Get.Events.Player.PlayerLeaveEvent += OnPlayerLeaveEvent;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPlayerPickUpItemEvent;
            Server.Get.Events.Player.PlayerReloadEvent += OnPlayerReloadEvent;
            Server.Get.Events.Player.PlayerReportEvent += OnPlayerReportEvent;
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
            Server.Get.Events.Player.PlayerShootEvent += OnPlayerShootEvent;
            Server.Get.Events.Player.PlayerSpeakEvent += OnPlayerSpeakEvent;
            Server.Get.Events.Player.PlayerThrowGrenadeEvent += OnPlayerThrowGrenadeEvent;
            //Server.Get.Events.Player.PlayerUnconnectWorkstationEvent += OnPlayerUnconnectWorkstationEvent;
            Server.Get.Events.Player.PlayerUncuffTargetEvent += OnPlayerUncuffTargetEvent;
            Server.Get.Events.Player.PlayerUseMicroEvent += OnPlayerUseMicroEvent;
            Server.Get.Events.Player.PlayerWalkOnSinkholeEvent += OnPlayerWalkOnSinkholeEvent;
            Server.Get.Events.Round.RoundCheckEvent += OnRoundCheckEvent;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEndEvent;
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestartEvent;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStartEvent;
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawnPlayersEvent;
            Server.Get.Events.Round.TeamRespawnEvent += OnTeamRespawnEvent;
            Server.Get.Events.Round.WaitingForPlayersEvent += OnWaitingForPlayersEvent;
            //Server.Get.Events.Scp.Scp079.Scp079RecontainEvent += OnScp079RecontainEvent;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnScp096AddTargetEvent;
            Server.Get.Events.Scp.Scp106.PocketDimensionEnterEvent += OnPocketDimensionEnterEvent;
            Server.Get.Events.Scp.Scp106.PocketDimensionLeaveEvent += OnPocketDimensionLeaveEvent;
            Server.Get.Events.Scp.Scp106.PortalCreateEvent += OnPortalCreateEvent;
            Server.Get.Events.Scp.Scp106.Scp106ContainmentEvent += OnScp106ContainmentEvent;
            Server.Get.Events.Scp.Scp173.Scp173BlinkEvent += OnScp173BlinkEvent;
            Server.Get.Events.Scp.ScpAttackEvent += OnScpAttackEvent;
            Server.Get.Events.Server.ConsoleCommandEvent += OnConsoleCommandEvent;
            Server.Get.Events.Server.PreAuthenticationEvent += OnPreAuthenticationEvent;
            Server.Get.Events.Server.RemoteAdminCommandEvent += OnRemoteAdminCommandEvent;
            Server.Get.Events.Server.TransmitPlayerDataEvent += OnTransmitPlayerDataEvent;
            Server.Get.Events.Server.UpdateEvent += OnUpdateEvent;
        }

        private void OnUpdateEvent()
        {
            
        }

        private void OnTransmitPlayerDataEvent(TransmitPlayerDataEventArgs ev)
        {
            
        }

        private void OnRemoteAdminCommandEvent(RemoteAdminCommandEventArgs ev)
        {
            
        }

        private void OnPreAuthenticationEvent(PreAuthenticationEventArgs ev)
        {

        }

        private void OnConsoleCommandEvent(ConsoleCommandEventArgs ev)
        {

        }

        private void OnScpAttackEvent(ScpAttackEventArgs ev)
        {

        }

        private void OnScp173BlinkEvent(Scp173BlinkEventArgs ev)
        {

        }

        private void OnScp106ContainmentEvent(Scp106ContainmentEventArgs ev)
        {

        }

        private void OnPortalCreateEvent(PortalCreateEventArgs ev)
        {

        }

        private void OnPocketDimensionLeaveEvent(PocketDimensionLeaveEventArgs ev)
        {

        }

        private void OnPocketDimensionEnterEvent(PocketDimensionEnterEventArgs ev)
        {

        }

        private void OnScp096AddTargetEvent(Scp096AddTargetEventArgument ev)
        {

        }

        private void OnScp079RecontainEvent(Scp079RecontainEventArgs ev)
        {

        }

        private void OnWaitingForPlayersEvent()
        {

        }

        private void OnTeamRespawnEvent(TeamRespawnEventArgs ev)
        {

        }

        private void OnSpawnPlayersEvent(SpawnPlayersEventArgs ev)
        {

        }

        private void OnRoundStartEvent()
        {

        }

        private void OnRoundRestartEvent()
        {

        }

        private void OnRoundEndEvent()
        {

        }

        private void OnRoundCheckEvent(RoundCheckEventArgs ev)
        {

        }

        private void OnPlayerWalkOnSinkholeEvent(PlayerWalkOnSinkholeEventArgs ev)
        {

        }

        private void OnPlayerUseMicroEvent(PlayerUseMicroEventArgs ev)
        {

        }

        private void OnPlayerUncuffTargetEvent(PlayerUnCuffTargetEventArgs ev)
        {

        }

        private void OnPlayerUnconnectWorkstationEvent(PlayerUnconnectWorkstationEventArgs ev)
        {

        }

        private void OnPlayerThrowGrenadeEvent(PlayerThrowGrenadeEventArgs ev)
        {

        }


        private void OnPlayerSpeakEvent(PlayerSpeakEventArgs ev)
        {

        }

        private void OnPlayerShootEvent(PlayerShootEventArgs ev)
        {

        }

        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {

        }

        private void OnPlayerReportEvent(PlayerReportEventArgs ev)
        {

        }

        private void OnPlayerReloadEvent(PlayerReloadEventArgs ev)
        {

        }

        private void OnPlayerPickUpItemEvent(PlayerPickUpItemEventArgs ev)
        {

        }

        private void OnPlayerLeaveEvent(PlayerLeaveEventArgs ev)
        {

        }

        private void OnPlayerKeyPressEvent(PlayerKeyPressEventArgs ev)
        {

        }

        private void OnPlayerJoinEvent(PlayerJoinEventArgs ev)
        {

        }

        private void OnPlayerItemUseEvent(PlayerItemInteractEventArgs ev)
        {

        }

        private void OnPlayerHealEvent(PlayerHealEventArgs ev)
        {

        }

        private void OnPlayerGeneratorInteractEvent(PlayerGeneratorInteractEventArgs ev)
        {

        }

        private void OnPlayerEscapesEvent(PlayerEscapeEventArgs ev)
        {

        }

        private void OnPlayerEnterFemurEvent(PlayerEnterFemurEventArgs ev)
        {

        }

        private void OnPlayerDropItemEvent(PlayerDropItemEventArgs ev)
        {

        }

        private void OnPlayerDropAmmoEvent(PlayerDropAmmoEventArgs ev)
        {

        }

        private void OnPlayerDeathEvent(PlayerDeathEventArgs ev)
        {

        }

        private void OnPlayerDamagePermissionEvent(PlayerDamagePermissionEventArgs ev)
        {

        }

        private void OnPlayerDamageEvent(PlayerDamageEventArgs ev)
        {

        }

        private void OnPlayerCuffTargetEvent(PlayerCuffTargetEventArgs ev)
        {

        }

        private void OnPlayerConnectWorkstationEvent(PlayerConnectWorkstationEventArgs ev)
        {

        }

        private void OnPlayerChangeItemEvent(PlayerChangeItemEventArgs ev)
        {

        }

        private void OnPlayerBanEvent(PlayerBanEventArgs ev)
        {

        }

        private void OnLoadComponentsEvent(LoadComponentEventArgs ev)
        {

        }

        private void OnWarheadDetonationEvent()
        {

        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {

        }

        private void OnScp914ActivateEvent(Scp914ActivateEventArgs ev)
        {

        }

        private void OnLCZDecontaminationEvent(LCZDecontaminationEventArgs ev)
        {

        }
    }
}
