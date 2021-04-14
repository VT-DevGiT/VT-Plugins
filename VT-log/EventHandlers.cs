using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTLog
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestart;
            //Server.Get.Events.Player.LoadComponentsEvent += PlayerOnLoadCompenents;
            Server.Get.Events.Player.PlayerBanEvent += PlayerOnBan;
            Server.Get.Events.Player.PlayerConnectWorkstationEvent += PlayerOnConnectWorkstation;
            Server.Get.Events.Player.PlayerCuffTargetEvent += PlayerOnCuffTarget;
            Server.Get.Events.Player.PlayerDamageEvent += PlayerOnDamage;
            Server.Get.Events.Player.PlayerDeathEvent += PlayerOnDeath;
            Server.Get.Events.Player.PlayerDropAmmoEvent += PlayerOnDropAmmo;
            Server.Get.Events.Player.PlayerDropItemEvent += PlayerOnDropIteam;
            Server.Get.Events.Player.PlayerEnterFemurEvent += PlayerOnEnterFermur;
            Server.Get.Events.Player.PlayerGeneratorInteractEvent += PlayerOnGenerator;
            Server.Get.Events.Player.PlayerHealEvent += PlayerOnHeal;
            Server.Get.Events.Player.PlayerItemUseEvent += PlayerOnUseIteam;
            Server.Get.Events.Player.PlayerJoinEvent += PlayerOnJoin;
            Server.Get.Events.Player.PlayerLeaveEvent += PlayerOnLeave;
            Server.Get.Events.Player.PlayerPickUpItemEvent += PlayerOnPickUpIteam;
            Server.Get.Events.Player.PlayerReloadEvent += PlayerOnReload;
            Server.Get.Events.Player.PlayerReportEvent += PlayerOnReport;
            Server.Get.Events.Player.PlayerSetClassEvent += PlayerOnSetClass;
            Server.Get.Events.Player.PlayerShootEvent += PlayerOnShoot;
            //Server.Get.Events.Player.PlayerSpeakEvent += PlayerOnSpeak;
            Server.Get.Events.Player.PlayerThrowGrenadeEvent += PlayerOnThrowGrenade;
            Server.Get.Events.Player.PlayerUnconnectWorkstationEvent += PlayerOnUnconnectWrokStation;
            Server.Get.Events.Player.PlayerUseMicroEvent += PlayerUseHID;
            Server.Get.Events.Player.PlayerWalkOnSinkholeEvent += PlayerWalkOnSinkhole;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += PlayerOnTargetScp096;
            Server.Get.Events.Scp.Scp106.Scp106ContainmentEvent += PlayerOnReconfScp106;
            Server.Get.Events.Scp.Scp106.PortalCreateEvent += PlayerOnCreatSinkhole;
            Server.Get.Events.Scp.Scp106.PocketDimensionEnterEvent += PlayerOnUseSinkhole;
            Server.Get.Events.Scp.Scp079.Scp079RecontainEvent += PlayerOnReconfScp079;
            Server.Get.Events.Map.DoorInteractEvent += PlayerOnIntercatDoor;
            Server.Get.Events.Round.SpawnPlayersEvent += RoundOnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += RoundOnRespawn;
            Server.Get.Events.Server.ConsoleCommandEvent += ConsoleOnCommand;
            Server.Get.Events.Server.RemoteAdminCommandEvent += RemoteAdminOnCommand;
        }
        private void OnRoundRestart()
        {
            Method.CreeUnNouveauxTXT();
        }

        private void PlayerOnDeath(PlayerDeathEventArgs ev)
        {   
            try
            { 
            Method.EcrirTXT($"Death : {ev?.Killer?.GetInfoPlayer()} Kill {ev?.Killer?.GetInfoPlayer()}---HitInfo : Amout {ev?.HitInfo.Amount}, Attacker {ev?.HitInfo.Attacker},Tool {ev?.HitInfo.Tool},Time {ev?.HitInfo.Time},ItemInHand {ev?.Killer?.ItemInHand.GetInfoItems()}");
            }
            catch (Exception e)
            {
                Method.EcrirTXT($"Death : #?Server Exception! {e}"); 
            }
        }

        private void PlayerOnDamage(PlayerDamageEventArgs ev)
        {
            Method.EcrirTXT($"Damage ? {ev.Allow} : {ev?.Killer?.GetInfoPlayer()} Damage {ev?.Victim?.GetInfoPlayer()} by {ev.DamageAmount}---HitInfo : Amout {ev?.HitInfo.Amount}, Attacker {ev?.HitInfo.Attacker},Tool {ev?.HitInfo.Tool},Time {ev?.HitInfo.Time},ItemInHand {ev?.Killer?.ItemInHand} ");
        }

        private void PlayerOnCuffTarget(PlayerCuffTargetEventArgs ev)
        {
            Method.EcrirTXT($"Cuff ? {ev.Allow} : {ev?.Cuffer?.GetInfoPlayer()} Cuff {ev?.Target?.GetInfoPlayer()}---Disarmer : item {ev?.Disarmer.GetInfoItems()}");
        }

        private void PlayerOnBan(PlayerBanEventArgs ev)
        {
            Method.EcrirTXT($"Ban ? {ev.Allow} : {ev?.Issuer?.GetInfoPlayer()} ban {ev?.BannedPlayer?.GetInfoPlayer()}---BanInfo : Duration {ev.Duration},Reason {ev.Reason}");
        }

        private void PlayerOnConnectWorkstation(PlayerConnectWorkstationEventArgs ev)
        {
            Method.EcrirTXT($"ConnectWorkstation ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} on {ev?.WorkStation?.Name}---TabletteInfo : item {ev.Item.GetInfoItems()}");
        }

        private void PlayerOnLoadCompenents(LoadComponentEventArgs ev)
        {
            Method.EcrirTXT($"LoadCompenents /// GameObject#{ev?.Player?.tag}#{ev?.Player?.GetPlayer()?.GetInfoPlayer()}");
        }

        private void PlayerOnDropAmmo(PlayerDropAmmoEventArgs ev)
        {
            Method.EcrirTXT($"DropAmmo ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} drop {ev?.Amount} of {ev?.AmmoType}--- Tablette : item {ev.Tablet.GetInfoItems()}");
        }

        private void PlayerOnGenerator(PlayerGeneratorInteractEventArgs ev)
        {
            Method.EcrirTXT($"Generator ? {ev.Allow} /// Interaction#{ev?.GeneratorInteraction} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnUnconnectWrokStation(PlayerUnconnectWorkstationEventArgs ev)
        {
            Method.EcrirTXT($"UnConnectWorkstation ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} on {ev?.WorkStation?.Name}");
        }

        private void PlayerOnReconfScp106(Scp106ContainmentEventArgs ev)
        {
            Method.EcrirTXT($"ReconfScp106 ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnUseSinkhole(PocketDimensionEnterEventArgs ev)
        {
            Method.EcrirTXT($"UseSinkhole ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} 106 {ev?.Player?.GetInfoPlayer()}");
        }

        private void RoundOnRespawn(TeamRespawnEventArgs ev)
        {
            string _playersInfo = "";
            foreach (var player in ev.Players)
            {
                _playersInfo += $" {player?.GetInfoPlayer()} //";
            }

            Method.EcrirTXT($"RoundRespawn ? {ev.Allow} : #{ev?.TeamID}.{ev?.Team}--- Players : Count {ev.Players.Count}, List {_playersInfo}");
        }

        private void RoundOnSpawn(SpawnPlayersEventArgs ev)
        {
            string _playersInfo = "";
            foreach (var player in ev.SpawnPlayers)
            {
                _playersInfo += $" {player.Key.GetInfoPlayer()}:{player.Value} //";
            }
            Method.EcrirTXT($"RoundOn ? {ev.Allow}: --- PlayersInfo : Count {ev?.SpawnPlayers?.Count}, List {_playersInfo}");
        }

        private void PlayerOnIntercatDoor(DoorInteractEventArgs ev)
        {
            Method.EcrirTXT($"IntercatDoor ? {ev.Allow} : {ev?.Player.GetInfoPlayer()} open #{ev?.Door?.Name}.{ev?.Door?.Rooms} whits {ev?.Player?.ItemInHand?.GetInfoItems()}");
        }

        private void PlayerOnReconfScp079(Scp079RecontainEventArgs ev)
        {
            Method.EcrirTXT($"ReconfScp079 ? {ev.Allow} : Status {ev?.Status}");
        }

        private void PlayerOnCreatSinkhole(PortalCreateEventArgs ev)
        {
            Method.EcrirTXT($"CreatSinkhole ? {ev.Allow} : {ev?.Scp106?.GetInfoPlayer()}");
        }

        private void PlayerOnTargetScp096(Scp096AddTargetEventArgument ev)
        {
            Method.EcrirTXT($"Scp096 ?  {ev.Allow} : {ev?.Scp096?.GetInfoPlayer()} target {ev?.Player?.GetInfoPlayer()}--- InfoScp096 : RageStats {ev?.RageState}");
        }

        private void PlayerWalkOnSinkhole(PlayerWalkOnSinkholeEventArgs ev)
        {
            Method.EcrirTXT($"WalkOnSinkhole ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} on #{ev?.Sinkhole?.name}.{ev?.Sinkhole?.netId}");
        }

        private void PlayerUseHID(PlayerUseMicroEventArgs ev)
        {
            Method.EcrirTXT($"UseHID : {ev?.Player} use {ev?.Micro?.GetInfoItems()}--- InfoHID : Energy {ev?.Energy}");
        }

        private void PlayerOnThrowGrenade(PlayerThrowGrenadeEventArgs ev)
        {
            Method.EcrirTXT($"ThrowGrenade ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}--- GrenadInfo {ev?.Item?.GetInfoItems()}");
        }

        private void PlayerOnSpeak(PlayerSpeakEventArgs ev)
        {
            Method.EcrirTXT($"Speak ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} speak #{ev?.DissonanceUserSetup?.name}.{ev?.DissonanceUserSetup?.netId}--- SpeakInfo : Intercom {ev?.IntercomTalk},Radio {ev?.RadioTalk},Scp939 {ev.Scp939Talk},ScpChat {ev.ScpChat},Spectator {ev.SpectatorChat}");
        }

        private void PlayerOnShoot(PlayerShootEventArgs ev)
        {
            Method.EcrirTXT($"Shoot ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} shoot #{ev?.Target?.GetInfoPlayer()} whit {ev.Weapon.GetInfoItems()}");
        }

        private void PlayerOnSetClass(PlayerSetClassEventArgs ev)
        {
            Method.EcrirTXT($"SetClass ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}--- SetClassInfo : Role {ev?.Player?.RoleID}//{ev?.Player?.RoleName} -> {ev?.Role}, Escape {ev.IsEscaping}");
        }

        private void PlayerOnReport(PlayerReportEventArgs ev)
        {
            Method.EcrirTXT($"Report ? {ev.Allow} : {ev?.Reporter?.GetInfoPlayer()} report {ev?.Target?.GetInfoPlayer()}--- ReportInfo : GlobalReport {ev.GlobalReport}, Reason {ev.Reason}");
        }

        private void PlayerOnReload(PlayerReloadEventArgs ev)
        {
            Method.EcrirTXT($"Reload ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} reload {ev?.Item.GetInfoItems()}");
        }

        private void PlayerOnPickUpIteam(PlayerPickUpItemEventArgs ev)
        {
            Method.EcrirTXT($"PickUp ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} pickup {ev?.Item.GetInfoItems()}");
        }

        private void PlayerOnLeave(PlayerLeaveEventArgs ev)
        {
            Method.EcrirTXT($"Leave : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnJoin(PlayerJoinEventArgs ev)
        {
            Method.EcrirTXT($"Join : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnUseIteam(PlayerItemInteractEventArgs ev)
        {
            Method.EcrirTXT($"UseIteam ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} use {ev?.CurrentItem.GetInfoItems()}--- State {ev?.State}");
        }

        private void PlayerOnHeal(PlayerHealEventArgs ev)
        {
            Method.EcrirTXT($"Heal ? {ev.Allow} :  {ev?.Player?.GetInfoPlayer()} of {ev?.Amount}");
        }

        private void PlayerOnEnterFermur(PlayerEnterFemurEventArgs ev)
        {
            Method.EcrirTXT($"EnterFermur ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnDropIteam(PlayerDropItemEventArgs ev)
        {
            Method.EcrirTXT($"DropIteam ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}) item {ev?.Item.GetInfoItems()}");
        }

        private void RemoteAdminOnCommand(RemoteAdminCommandEventArgs ev)
        {
            Method.EcrirTXT($"RemoteAdmin ? {ev.Allow} : {ev?.Command} by Staff:#{ev?.Sender?.SenderId}//{ev?.Sender?.LogName}//{ev?.Sender?.GetPlayer()?.GetInfoPlayer()}!#{ev.Sender.Permissions}");
        }

        private void ConsoleOnCommand(ConsoleCommandEventArgs ev)
        {
            if (!ev.Command.Contains("keypress"))
                Method.EcrirTXT($"Console : {ev?.Command} by {ev?.Player?.GetInfoPlayer()}");
        }
    }
}