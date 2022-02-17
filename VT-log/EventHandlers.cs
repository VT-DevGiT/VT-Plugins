using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTLog
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestart;
            //Server.Get.Events.Player.LoadComponentsEvent += PlayerOnLoadCompenents;
/*          Server.Get.Events.Player.PlayerBanEvent += PlayerOnBan;
            Server.Get.Events.Player.PlayerLeaveEvent += PlayerOnLeave;
            Server.Get.Events.Player.PlayerSetClassEvent += PlayerOnSetClass;
            Server.Get.Events.Player.PlayerJoinEvent += PlayerOnJoin;
            Server.Get.Events.Player.PlayerStartWorkstationEvent += PlayerOnConnectWorkstation;
            Server.Get.Events.Player.PlayerCuffTargetEvent += PlayerOnCuffTarget;
            Server.Get.Events.Player.PlayerDamageEvent += PlayerOnDamage;
            Server.Get.Events.Player.PlayerDeathEvent += PlayerOnDeath;
            Server.Get.Events.Player.PlayerDropAmmoEvent += PlayerOnDropAmmo;
            Server.Get.Events.Player.PlayerDropItemEvent += PlayerOnDropIteam;
            Server.Get.Events.Player.PlayerEnterFemurEvent += PlayerOnEnterFermur;
            Server.Get.Events.Player.PlayerGeneratorInteractEvent += PlayerOnGenerator;
            Server.Get.Events.Player.PlayerHealEvent += PlayerOnHeal;
            Server.Get.Events.Player.PlayerItemUseEvent += PlayerOnUseIteam;
            Server.Get.Events.Player.PlayerPickUpItemEvent += PlayerOnPickUpIteam;
            Server.Get.Events.Player.PlayerReloadEvent += PlayerOnReload;
            Server.Get.Events.Player.PlayerReportEvent += PlayerOnReport;
            Server.Get.Events.Player.PlayerShootEvent += PlayerOnShoot;
            //Server.Get.Events.Player.PlayerSpeakEvent += PlayerOnSpeak;
            Server.Get.Events.Player.PlayerThrowGrenadeEvent += PlayerOnThrowGrenade;
            Server.Get.Events.Player.PlayerUseMicroEvent += PlayerUseHID;
            Server.Get.Events.Player.PlayerWalkOnSinkholeEvent += PlayerWalkOnSinkhole;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += PlayerOnTargetScp096;
            Server.Get.Events.Scp.Scp106.Scp106ContainmentEvent += PlayerOnReconfScp106;
            Server.Get.Events.Scp.Scp106.PortalCreateEvent += PlayerOnCreatSinkhole;
            Server.Get.Events.Scp.Scp106.PocketDimensionEnterEvent += PlayerOnUseSinkhole;
            Server.Get.Events.Scp.Scp079.RecontainEvent += PlayerOnReconfScp079;
            Server.Get.Events.Map.DoorInteractEvent += PlayerOnIntercatDoor;
            Server.Get.Events.Round.SpawnPlayersEvent += RoundOnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += RoundOnRespawn;
            Server.Get.Events.Server.ConsoleCommandEvent += ConsoleOnCommand;
            Server.Get.Events.Server.RemoteAdminCommandEvent += RemoteAdminOnCommand;*/
        }

        private void OnRoundRestart()
        {
            Log.Write("--------------- End of Log ---------------");
            Log.CreateNew();
        }

        /*
        private void PlayerOnDeath(PlayerDeathEventArgs ev)
        {   
            try
            { 
                Log.Write($"Death : {ev?.Victim?.GetInfoPlayer()} killed by {ev?.Killer?.GetInfoPlayer()} -- {ev?.DamageType}");
            }
            catch (Exception e)
            {
                Log.Write($"Death : #?Server Exception! {e}"); 
            }
        }

        private void PlayerOnDamage(PlayerDamageEventArgs ev)
        {
            Log.Write($"Damage ? {ev.Allow} : {ev?.Victim?.GetInfoPlayer()} attaked by {ev?.Killer?.GetInfoPlayer()} Amout {ev?.Damage}");
        }

        private void PlayerOnCuffTarget(PlayerCuffTargetEventArgs ev)
        {
            Log.Write($"Cuff ? {ev.Allow} : {ev?.Cuffer?.GetInfoPlayer()} Cuff {ev?.Target?.GetInfoPlayer()}");
        }

        private void PlayerOnBan(PlayerBanEventArgs ev)
        {
            Log.Write($"Ban ? {ev.Allow} : {ev?.Issuer?.GetInfoPlayer()} ban {ev?.BannedPlayer?.GetInfoPlayer()}---BanInfo : Duration {ev.BanDuration},Reason {ev.Reason}");
        }

        private void PlayerOnConnectWorkstation(PlayerStartWorkstationEventArgs ev)
        {
            Log.Write($"ConnectWorkstation ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} on {ev?.WorkStation?.Name}");
        }

        private void PlayerOnLoadCompenents(LoadComponentEventArgs ev)
        {
            Log.Write($"LoadCompenents /// GameObject#{ev?.Player?.tag}#{ev?.Player?.GetPlayer()?.GetInfoPlayer()}");
        }

        private void PlayerOnDropAmmo(PlayerDropAmmoEventArgs ev)
        {
            Log.Write($"DropAmmo ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} drop {ev?.Amount} of {ev?.AmmoType}");
        }

        private void PlayerOnGenerator(PlayerGeneratorInteractEventArgs ev)
        {
            Log.Write($"Generator ? {ev.Allow} /// Interaction#{ev?.GeneratorInteraction} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnReconfScp106(Scp106ContainmentEventArgs ev)
        {
            Log.Write($"ReconfScp106 ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnUseSinkhole(PocketDimensionEnterEventArgs ev)
        {
            Log.Write($"UseSinkhole ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} 106 {ev?.Player?.GetInfoPlayer()}");
        }

        private void RoundOnRespawn(TeamRespawnEventArgs ev)
        {
            string _playersInfo = "";
            foreach (var player in ev.Players)
            {
                _playersInfo += $" {player?.GetInfoPlayer()} //";
            }

            Log.Write($"RoundRespawn ? {ev.Allow} : #{ev?.TeamID}.{ev?.Team}--- Players : Count {ev.Players.Count}, List {_playersInfo}");
        }

        private void RoundOnSpawn(SpawnPlayersEventArgs ev)
        {
            string _playersInfo = "";
            foreach (var player in ev.SpawnPlayers)
            {
                _playersInfo += $" {player.Key.GetInfoPlayer()}:{player.Value} //";
            }
            Log.Write($"RoundOn ? {ev.Allow}: --- PlayersInfo : Count {ev?.SpawnPlayers?.Count}, List {_playersInfo}");
        }

        private void PlayerOnIntercatDoor(DoorInteractEventArgs ev)
        {
            Log.Write($"IntercatDoor ? {ev.Allow} : {ev?.Player.GetInfoPlayer()} open #{ev?.Door?.Name}.{ev?.Door?.Rooms} whits {ev?.Player?.ItemInHand?.GetInfoItems()}");
        }

        private void PlayerOnReconfScp079(Scp079RecontainEventArgs ev)
        {
            Log.Write($"ReconfScp079 ? {ev.Allow} : Status {ev?.Status}");
        }

        private void PlayerOnCreatSinkhole(PortalCreateEventArgs ev)
        {
            Log.Write($"CreatSinkhole ? {ev.Allow} : {ev?.Scp106?.GetInfoPlayer()}");
        }

        private void PlayerOnTargetScp096(Scp096AddTargetEventArgument ev)
        {
            Log.Write($"Scp096 ?  {ev.Allow} : {ev?.Scp096?.GetInfoPlayer()} target {ev?.Player?.GetInfoPlayer()}--- InfoScp096 : RageStats {ev?.RageState}");
        }

        private void PlayerWalkOnSinkhole(PlayerWalkOnSinkholeEventArgs ev)
        {
            Log.Write($"WalkOnSinkhole ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} on #{ev?.Sinkhole?.name}.{ev?.Sinkhole?.netId}");
        }

        private void PlayerUseHID(PlayerUseMicroEventArgs ev)
        {
            Log.Write($"UseHID : {ev?.Player} use {ev?.Micro?.GetInfoItems()}--- InfoHID : Energy {ev?.Energy}");
        }

        private void PlayerOnThrowGrenade(PlayerThrowGrenadeEventArgs ev)
        {
            Log.Write($"ThrowGrenade ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}--- GrenadInfo {ev?.Item?.GetInfoItems()}");
        }

        private void PlayerOnSpeak(PlayerSpeakEventArgs ev)
        {
            Log.Write($"Speak ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} speak #{ev?.DissonanceUserSetup?.name}.{ev?.DissonanceUserSetup?.netId}--- SpeakInfo : Intercom {ev?.IntercomTalk},Radio {ev?.RadioTalk},Scp939 {ev.Scp939Talk},ScpChat {ev.ScpChat},Spectator {ev.SpectatorChat}");
        }

        private void PlayerOnShoot(PlayerShootEventArgs ev)
        {
            Log.Write($"Shoot ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} shoot #{ev?.Target?.GetInfoPlayer()} whit {ev.Weapon.GetInfoItems()}");
        }

        private void PlayerOnSetClass(PlayerSetClassEventArgs ev)
        {
            Log.Write($"SetClass ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}--- SetClassInfo : Role {ev?.Player?.RoleID}//{ev?.Player?.RoleName} -> {ev?.Role}, Escape {ev.IsEscaping}");
        }

        private void PlayerOnReport(PlayerReportEventArgs ev)
        {
            Log.Write($"Report ? {ev.Allow} : {ev?.Reporter?.GetInfoPlayer()} report {ev?.Target?.GetInfoPlayer()}--- ReportInfo : GlobalReport {ev.GlobalReport}, Reason {ev.Reason}");
        }

        private void PlayerOnReload(PlayerReloadEventArgs ev)
        {
            Log.Write($"Reload ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} reload {ev?.Item.GetInfoItems()}");
        }

        private void PlayerOnPickUpIteam(PlayerPickUpItemEventArgs ev)
        {
            Log.Write($"PickUp ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} pickup {ev?.Item.GetInfoItems()}");
        }

        private void PlayerOnLeave(PlayerLeaveEventArgs ev)
        {
            Log.Write($"Leave : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnJoin(PlayerJoinEventArgs ev)
        {
            Log.Write($"Join : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnUseIteam(PlayerItemInteractEventArgs ev)
        {
            Log.Write($"UseIteam ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()} use {ev?.CurrentItem.GetInfoItems()}--- State {ev?.State}");
        }

        private void PlayerOnHeal(PlayerHealEventArgs ev)
        {
            Log.Write($"Heal ? {ev.Allow} :  {ev?.Player?.GetInfoPlayer()} of {ev?.Amount}");
        }

        private void PlayerOnEnterFermur(PlayerEnterFemurEventArgs ev)
        {
            Log.Write($"EnterFermur ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}");
        }

        private void PlayerOnDropIteam(PlayerDropItemEventArgs ev)
        {
            Log.Write($"DropIteam ? {ev.Allow} : {ev?.Player?.GetInfoPlayer()}) item {ev?.Item.GetInfoItems()}");
        }

        private void RemoteAdminOnCommand(RemoteAdminCommandEventArgs ev)
        {
            Log.Write($"RemoteAdmin ? {ev.Allow} : {ev?.Command} by Staff:#{ev?.Sender?.SenderId}//{ev?.Sender?.LogName}//{ev?.Sender?.GetPlayer()?.GetInfoPlayer()}!#{ev.Sender.Permissions}");
        }

        private void ConsoleOnCommand(ConsoleCommandEventArgs ev)
        {
            if (!ev.Command.Contains("keypress"))
                Log.Write($"Console : {ev?.Command} by {ev?.Player?.GetInfoPlayer()}");
        }
        */
    }
}