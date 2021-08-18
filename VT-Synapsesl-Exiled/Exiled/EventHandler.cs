using Synapse.Api.Events.SynapseEventArguments;
using System;
using Synapse;
using Exiled.Events.EventArgs;
using VT_MultieLoder.API;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using System.Linq;

using MapHandlers = Exiled.Events.Handlers.Map;
using PlayerHandlers = Exiled.Events.Handlers.Player;
using Scp106Handlers = Exiled.Events.Handlers.Scp106;
using Scp173Handlers = Exiled.Events.Handlers.Scp173;
using Scp079Handlers = Exiled.Events.Handlers.Scp079;
using Scp096Handlers = Exiled.Events.Handlers.Scp096;
using Scp914Handlers = Exiled.Events.Handlers.Scp914;
using ServerHandlers = Exiled.Events.Handlers.Server;
using WarHeadHandlers = Exiled.Events.Handlers.Warhead;
using ExiledPlayer = Exiled.API.Features.Player;
using Scp914;
using Grenades;

namespace VT_MultieLoder.Exiled
{
    public class EventHandler
    {
        public EventHandler()
        {
            Server.Get.Events.Map.LCZDecontaminationEvent += OnLCZDecontaminationEvent;
            Server.Get.Events.Map.Scp914ActivateEvent += OnScp914ActivateEvent;
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Map.WarheadDetonationEvent += OnWarheadDetonationEvent;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteractEvent;
            Server.Get.Events.Player.LoadComponentsEvent += OnLoadComponentsEvent;
            Server.Get.Events.Player.PlayerBanEvent += OnPlayerBanEvent;
            Server.Get.Events.Player.PlayerChangeItemEvent += OnPlayerChangeItemEvent;
            Server.Get.Events.Player.PlayerConnectWorkstationEvent += OnPlayerConnectWorkstationEvent;
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
            Server.Get.Events.Player.PlayerSyncDataEvent += OnPlayerSyncDataEvent;
            Server.Get.Events.Player.PlayerThrowGrenadeEvent += OnPlayerThrowGrenadeEvent;
            Server.Get.Events.Player.PlayerUnconnectWorkstationEvent += OnPlayerUnconnectWorkstationEvent;
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
            Server.Get.Events.Scp.Scp079.Scp079RecontainEvent += OnScp079RecontainEvent;
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

        private void OnDoorInteractEvent(DoorInteractEventArgs ev)
        {
            try
            {
                var arg = new InteractingDoorEventArgs(ev.Player.ToExiled(), ev.Door.VDoor, ev.Allow);
                PlayerHandlers.OnInteractingDoor(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"InteractingDoorEventCall fail ! \n{e.Message}");
            }
        }

        private void OnUpdateEvent()
        {

        }

        private void OnTransmitPlayerDataEvent(TransmitPlayerDataEventArgs ev)
        {

        }

        private void OnRemoteAdminCommandEvent(RemoteAdminCommandEventArgs ev)
        {
            (string name, string[] arguments) = ev.Command.ExtractCommand();
            var arg = new SendingRemoteAdminCommandEventArgs(ev.Sender, ExiledPlayer.Get(ev.Sender), name, arguments.ToList(), ev.Allow);

            ServerHandlers.OnSendingRemoteAdminCommand(arg);
        }

        private void OnPreAuthenticationEvent(PreAuthenticationEventArgs ev)
        {
            try 
            {
                var arg = new PreAuthenticatingEventArgs(ev.UserId, ev.Request, 0, 0, "null", ev.Allow);
                PlayerHandlers.OnPreAuthenticating(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"PreAuthenticationEventCall fail ! \n{e.Message}");
            }
        }

        private void OnConsoleCommandEvent(ConsoleCommandEventArgs ev)
        {
            try
            {
                (string name, string[] arguments) = ev.Command.ExtractCommand();
                var arg = new SendingConsoleCommandEventArgs(ev.Player.ToExiled(), name, arguments.ToList(), false);

                ServerHandlers.OnSendingConsoleCommand(arg);

                arg.Player.SendConsoleMessage(arg.ReturnMessage, arg.Color);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RemoteAdminCommandEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScpAttackEvent(ScpAttackEventArgs ev)
        {

        }

        private void OnScp173BlinkEvent(Scp173BlinkEventArgs ev)
        {
            try
            {
                var arg = new BlinkingEventArgs(ev.Scp173.ToExiled(), ev.Scp173.Scp173Controller.ConfrontingPlayers.ToList().ToExiled());

                Scp173Handlers.OnBlinking(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"BlinkingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp106ContainmentEvent(Scp106ContainmentEventArgs ev)
        {
            try
            {
                foreach (var player in Server.Get.Players.Where(p => p.RoleType == RoleType.Scp106))
                {
                    var arg = new ContainingEventArgs(player.ToExiled(), ev.Player.ToExiled(), ev.Allow);
                    if (!arg.IsAllowed)
                    {
                        ev.Allow = false;
                        return;
                    }

                    ev.Allow = true;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ContainingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPortalCreateEvent(PortalCreateEventArgs ev)
        {
            try
            {
                var arg = new CreatingPortalEventArgs(ev.Scp106.ToExiled(), ev.Scp106.Position, ev.Allow);

                Scp106Handlers.OnCreatingPortal(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"CreatingPortalEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPocketDimensionLeaveEvent(PocketDimensionLeaveEventArgs ev)
        {
            try
            {
                var pdt = new PocketDimensionTeleport
                {
                    type = ev.TeleportType,
                };

                var arg = new FailingEscapePocketDimensionEventArgs(ev.Player.ToExiled(), pdt, ev.Allow);

                PlayerHandlers.OnFailingEscapePocketDimension(arg);

                ev.Allow = arg.IsAllowed;
                ev.TeleportType = arg.Teleporter.type;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"FailingEscapePocketDimensionEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPocketDimensionEnterEvent(PocketDimensionEnterEventArgs ev)
        {
            try
            {
                var arg = new EnteringPocketDimensionEventArgs(ev.Player.ToExiled(), Vector3.down * 1998.5f, ev.Scp106.ToExiled(), ev.Allow);
                PlayerHandlers.OnEnteringPocketDimension(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"EnteringPocketDimmensionEvnetCall fail ! \n{e.Message}");
            }
        }

        private void OnScp096AddTargetEvent(Scp096AddTargetEventArgument ev)
        {
            try
            {
                var arg = new AddingTargetEventArgs(ev.Scp096.ToExiled(), ev.Player.ToExiled(), (int)ev.Scp096.Scp096Controller.ShieldAmount, ev.Scp096.Scp096Controller.EnrageTimeLeft, ev.Allow);
                Scp096Handlers.OnAddingTarget(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Scp079RecontainEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079RecontainEvent(Scp079RecontainEventArgs ev)
        {
            try
            {
                foreach(var player in Server.Get.Players.Where(p => p.RoleType == RoleType.Scp079))
                    Scp079Handlers.OnRecontained(new RecontainedEventArgs(player.ToExiled()));
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Scp079RecontainEventCall fail ! \n{e.Message}");
            }
        }

        private void OnWaitingForPlayersEvent()
        {
            try
            {
                ServerHandlers.OnWaitingForPlayers();
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"WaitingForPlayseEventCall fail ! \n{e.Message}");
            }
        }

        private void OnTeamRespawnEvent(TeamRespawnEventArgs ev)
        {
            try
            {
                if (ev.TeamID > 2)
                    return;

                var arg = new RespawningTeamEventArgs(ev.Players.ToExiled(), ev.Team, ev.Allow);

                ServerHandlers.OnRespawningTeam(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RespawningTeamEventCall fail ! \n{e.Message}");
            }
        }

        private void OnSpawnPlayersEvent(SpawnPlayersEventArgs ev)
        {
            try
            {
                foreach (var spawnPlayer in ev.SpawnPlayers)
                {
                    var arg = new SpawningEventArgs(spawnPlayer.Key.ToExiled(), (RoleType)spawnPlayer.Value, Vector3.zero, 0);
                    PlayerHandlers.OnSpawning(arg);
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"SpawnPlayersEventCall fail ! \n{e.Message}");
            }
        }

        private void OnRoundStartEvent()
        {
            try
            {
                ServerHandlers.OnRoundStarted();
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RoundStartedEventCall fail ! \n{e.Message}");
            }
        }

        private void OnRoundRestartEvent()
        {
            try
            {
                ServerHandlers.OnRestartingRound();
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RestartingRoundEventCall fail ! \n{e.Message}");
            }
        }

        private void OnRoundEndEvent()
        {

        }

        private void OnRoundCheckEvent(RoundCheckEventArgs ev)
        {
            try
            {
                var result = default(RoundSummary.SumInfo_ClassList);

                foreach (var player in Synapse.Server.Get.Players)
                {
                    switch (player.RealTeam)
                    {
                        case Team.SCP:
                            if (player.RoleID == (int)RoleType.Scp0492)
                                result.zombies++;
                            else
                                result.scps_except_zombies++;
                            break;

                        case Team.MTF:
                            result.mtf_and_guards++;
                            break;

                        case Team.CHI:
                            result.chaos_insurgents++;
                            break;

                        case Team.RSC:
                            result.scientists++;
                            break;

                        case Team.CDP:
                            result.class_ds++;
                            break;
                    }
                }

                result.warhead_kills = Server.Get.Map.Nuke.Detonated ? Server.Get.Map.Nuke.NukeKills : -1;

                result.time = (int)Time.realtimeSinceStartup;

                var arg1 = new EndingRoundEventArgs((LeadingTeam)ev.Team, result, ev.EndRound, ev.EndRound);

                ServerHandlers.OnEndingRound(arg1);

                ev.Team = (RoundSummary.LeadingTeam)arg1.LeadingTeam;
                ev.EndRound = arg1.IsAllowed;
                if (ev.EndRound)
                {
                    var arg2 = new RoundEndedEventArgs(arg1.LeadingTeam, arg1.ClassList, Mathf.Clamp(GameCore.ConfigFile.ServerConfig.GetInt("auto_round_restart_time", 10), 5, 1000));

                    ServerHandlers.OnRoundEnded(arg2);
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"EndingEndEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerWalkOnSinkholeEvent(PlayerWalkOnSinkholeEventArgs ev)
        {

        }

        private void OnPlayerUseMicroEvent(PlayerUseMicroEventArgs ev)
        {
            try 
            {
                var arg = new UsingMicroHIDEnergyEventArgs(ev.Player.ToExiled(), ev.Micro.pickup.gameObject.GetComponent<MicroHID>(), ev.State, ev.Energy, ev.Energy - 0.1f, true);

                PlayerHandlers.OnUsingMicroHIDEnergy(arg);

                ev.Energy = arg.NewValue + 0.1f;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseMicroEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerUncuffTargetEvent(PlayerUnCuffTargetEventArgs ev)
        {
            try
            {
                var arg = new RemovingHandcuffsEventArgs(ev.Player.ToExiled(), ev.Player.ToExiled(), ev.Allow);
                PlayerHandlers.OnRemovingHandcuffs(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RemovingCuffingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerUnconnectWorkstationEvent(PlayerUnconnectWorkstationEventArgs ev)
        {
            try
            {
                var arg = new DeactivatingWorkstationEventArgs(ev.Player.ToExiled(), ev.WorkStation.GameObject.GetComponent<WorkStation>(), ev.Allow);
                PlayerHandlers.OnDeactivatingWorkstation(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"DeactivatingWorkstationEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerThrowGrenadeEvent(PlayerThrowGrenadeEventArgs ev)
        {
            try
            {
                GrenadeType type = GrenadeType.FragGrenade;
                switch (ev.Item.ItemType)
                {
                    case ItemType.GrenadeFlash:
                        type = GrenadeType.Flashbang;
                        break;
                    case ItemType.GrenadeFrag:
                        type = GrenadeType.FragGrenade;
                        break;
                    case ItemType.SCP018:
                        type = GrenadeType.Scp018;
                        break;
                }

                var arg = new ThrowingGrenadeEventArgs(ev.Player.ToExiled(), ev.Item.pickup.gameObject.GetComponent<GrenadeManager>(), type, ev.ForceMultiplier > 1, ev.Delay, ev.Allow);

                PlayerHandlers.OnThrowingGrenade(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ThrowGrenadeEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerSyncDataEvent(PlayerSyncDataEventArgs ev)
        {
            try
            {
                var arg = new SyncingDataEventArgs(ev.Player.ToExiled(), ev.Player.AnimationController.speed, ev.Player.AnimationController.Network_curMoveState, ev.Allow);

                PlayerHandlers.OnSyncingData(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"SyncDataEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerSpeakEvent(PlayerSpeakEventArgs ev)
        {

        }

        private void OnPlayerShootEvent(PlayerShootEventArgs ev)
        {
            try
            {
                Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit raycastHit, 1049088);

                var arg = new ShootingEventArgs(ev.Player.ToExiled(), raycastHit.transform?.gameObject, ev.TargetPosition, ev.Allow);

                PlayerHandlers.OnShooting(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ShootRoleEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            try
            {
                List<ItemType> items = new List<ItemType>();
                foreach (var synapseItem in ev.Items)
                    items.Add(synapseItem.ItemType);

                var arg = new ChangingRoleEventArgs(ev.Player.ToExiled(), ev.Role, items, ev.Player.Position == ev.Position, ev.IsEscaping);

                PlayerHandlers.OnChangingRole(arg);

                if (arg.ShouldPreservePosition)
                    ev.Position = ev.Player.Position;
                ev.Role = arg.NewRole;
                ev.Allow = ev.Player.RoleType != ev.Role;

                Timing.RunCoroutine(ChangedRoleEventCall(ev));
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ClassChangingRoleEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerReportEvent(PlayerReportEventArgs ev)
        {
            try
            {
                if (ev.GlobalReport)
                {
                    var arg = new ReportingCheaterEventArgs(ev.Reporter.ToExiled(), ev.Target.ToExiled(), Server.Get.Port, ev.Reason, ev.Allow);

                    ServerHandlers.OnReportingCheater(arg);

                    ev.Allow = arg.IsAllowed;
                }
                else
                {
                    var arg = new LocalReportingEventArgs(ev.Reporter.ToExiled(), ev.Target.ToExiled(), ev.Reason, ev.Allow);

                    ServerHandlers.OnLocalReporting(arg);

                    ev.Allow = arg.IsAllowed;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ReportingCheaterEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerReloadEvent(PlayerReloadEventArgs ev)
        {
            try
            {
                var arg = new ReloadingWeaponEventArgs(ev.Player.ToExiled(), false, ev.Allow);
                PlayerHandlers.OnReloadingWeapon(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ReloadingWeaponEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerPickUpItemEvent(PlayerPickUpItemEventArgs ev)
        {
            try
            {
                if (ev.Item.ItemType != ItemType.Ammo556 && ev.Item.ItemType != ItemType.Ammo9mm && ev.Item.ItemType != ItemType.Ammo762)
                {
                    var arg = new PickingUpItemEventArgs(ev.Player.ToExiled(), ev.Item.pickup, ev.Allow);

                    PlayerHandlers.OnPickingUpItem(arg);

                    ev.Allow = arg.IsAllowed;
                }
                else
                {
                    var arg = new PickingUpAmmoEventArgs(ev.Player.ToExiled(), ev.Item.pickup, ev.Allow);

                    PlayerHandlers.OnPickingUpAmmo(arg);

                    ev.Allow = arg.IsAllowed;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"PickupItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerLeaveEvent(PlayerLeaveEventArgs ev)
        {
            try
            {
                var arg = new LeftEventArgs(ev.Player.ToExiled());

                PlayerHandlers.OnLeft(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"LeftEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerKeyPressEvent(PlayerKeyPressEventArgs ev)
        {

        }

        private void OnPlayerJoinEvent(PlayerJoinEventArgs ev)
        {
            try
            {
                var arg = new JoinedEventArgs(ev.Player.ToExiled());

                PlayerHandlers.OnJoined(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"JoinEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerItemUseEvent(PlayerItemInteractEventArgs ev)
        {
            try
            {
                if (ev.CurrentItem.ItemCategory != ItemCategory.Medical)
                    return;

                var consumable = ev.Player.GetComponent<ConsumableAndWearableItems>();

                switch (ev.State)
                {
                    case ItemInteractState.Initiating:

                        for (int i = 0; i < consumable.usableItems.Length; ++i)
                        {
                            if (consumable.usableItems[i].inventoryID == ev.CurrentItem.ItemType &&
                                consumable.usableCooldowns[i] <= 0.0)
                            {
                                var arg1 = new UsingMedicalItemEventArgs(ev.Player.ToExiled(), ev.CurrentItem.ItemType, consumable.usableItems[i].animationDuration);

                                PlayerHandlers.OnUsingMedicalItem(arg1);

                                consumable.cooldown = arg1.Cooldown;

                                ev.Allow = arg1.IsAllowed;
                            }
                        }

                        break;
                    case ItemInteractState.Finalizing:
                        var arg2 = new UsedMedicalItemEventArgs(ev.Player.ToExiled(), ev.CurrentItem.ItemType);
                        var arg3 = new DequippedMedicalItemEventArgs(ev.Player.ToExiled(), ev.CurrentItem.ItemType);

                        PlayerHandlers.OnMedicalItemUsed(arg2);
                        PlayerHandlers.OnMedicalItemDequipped(arg3);
                        break;
                    case ItemInteractState.Stopping:
                        for (int i = 0; i < consumable.usableItems.Length; ++i)
                        {
                            if (consumable.usableItems[i].inventoryID == ev.CurrentItem.ItemType &&
                                consumable.usableCooldowns[i] <= 0.0)
                            {
                                var arg4 = new StoppingMedicalItemEventArgs(ev.Player.ToExiled(), ev.CurrentItem.ItemType, consumable.usableItems[i].animationDuration);

                                PlayerHandlers.OnStoppingMedicalItem(arg4);

                                consumable.cooldown = arg4.Cooldown;

                                ev.Allow = arg4.IsAllowed;
                            }
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseMedicalItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerHealEvent(PlayerHealEventArgs ev)
        {

        }

        private void OnPlayerGeneratorInteractEvent(PlayerGeneratorInteractEventArgs ev)
        {
            try
            {
                Generator079 generator = ev.Generator.GameObject.GetComponent<Generator079>();
                switch (ev.GeneratorInteraction)
                {
                    case Synapse.Api.Enum.GeneratorInteraction.CloseDoor:
                        var arg1 = new ClosingGeneratorEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnOpeningGenerator(arg1);
                        ev.Allow = arg1.IsAllowed;
                        break;
                    case Synapse.Api.Enum.GeneratorInteraction.OpenDoor:
                        var arg2 = new OpeningGeneratorEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnOpeningGenerator(arg2);
                        ev.Allow = arg2.IsAllowed;
                        break;
                    case Synapse.Api.Enum.GeneratorInteraction.TabledEjected:
                        var arg3 = new EjectingGeneratorTabletEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnEjectingGeneratorTablet(arg3);
                        ev.Allow = arg3.IsAllowed;
                        break;
                    case Synapse.Api.Enum.GeneratorInteraction.TabletInjected:
                        var arg4 = new InsertingGeneratorTabletEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnInsertingGeneratorTablet(arg4);
                        ev.Allow = arg4.IsAllowed;
                        break;
                    case Synapse.Api.Enum.GeneratorInteraction.Unlocked:
                        var arg5 = new UnlockingGeneratorEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnUnlockingGenerator(arg5);
                        ev.Allow = arg5.IsAllowed;
                        break;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"GenratorEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerEscapesEvent(PlayerEscapeEventArgs ev)
        {
            try
            {
                var arg = new ChangedRoleEventArgs(ev.Player.ToExiled(), (RoleType)ev.SpawnRole, ev.Player.Cuffer.PlayerId, false, true);
                PlayerHandlers.OnChangedRole(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"EscapeChangingRoleEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerEnterFemurEvent(PlayerEnterFemurEventArgs ev)
        {
            try
            {
                var arg = new EnteringFemurBreakerEventArgs(ev.Player.ToExiled(), ev.Allow);

                PlayerHandlers.OnEnteringFemurBreaker(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"EnteringFemurBreakerEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerDropItemEvent(PlayerDropItemEventArgs ev)
        {
            try
            {
                var arg = new DroppingItemEventArgs(ev.Player.ToExiled(), ev.Item.itemInfo, ev.Allow);
                PlayerHandlers.OnDroppingItem(arg);
                ev.Allow = arg.IsAllowed;
                if (arg.IsAllowed)
                    Timing.RunCoroutine(ItemDroppedEventCall(ev));
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ItemDropEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerDropAmmoEvent(PlayerDropAmmoEventArgs ev)
        {

        }

        private void OnPlayerDeathEvent(PlayerDeathEventArgs ev)
        {
            try
            {
                PlayerHandlers.OnDied(new DiedEventArgs(ev.Killer.ToExiled(), ev.Victim.ToExiled(), ev.HitInfo));
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"DiedEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerDamagePermissionEvent(PlayerDamagePermissionEventArgs ev)
        {

        }

        private void OnPlayerDamageEvent(PlayerDamageEventArgs ev)
        {
            try
            {
                var arg = new HurtingEventArgs(ev.Killer.ToExiled(), ev.Victim.ToExiled(), ev.HitInfo, ev.Allow);
                
                PlayerHandlers.OnHurting(arg);    
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"HurtingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerCuffTargetEvent(PlayerCuffTargetEventArgs ev)
        {
            try
            {
                var arg = new HandcuffingEventArgs(ev.Cuffer.ToExiled(), ev.Target.ToExiled(), ev.Allow);
                PlayerHandlers.OnHandcuffing(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"HandcuffingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerConnectWorkstationEvent(PlayerConnectWorkstationEventArgs ev)
        {
            try
            {
                var arg = new ActivatingWorkstationEventArgs(ev.Player.ToExiled(), ev.WorkStation.GameObject.GetComponent<WorkStation>(), ev.Allow);
                PlayerHandlers.OnActivatingWorkstation(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ActivatingWarkstationEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerChangeItemEvent(PlayerChangeItemEventArgs ev)
        {
            try
            {
                Inventory.SyncItemInfo newItemInfo;
                if (ev.NewItem != null)
                    newItemInfo = ev.NewItem.itemInfo;
                else
                    newItemInfo = new Inventory.SyncItemInfo() { id = ItemType.None };
                Inventory.SyncItemInfo oldItemInfo;
                if (ev.OldItem != null)
                    oldItemInfo = ev.NewItem.itemInfo;
                else
                    oldItemInfo = new Inventory.SyncItemInfo() { id = ItemType.None };

                var arg = new ChangingItemEventArgs(ev.Player.ToExiled(), oldItemInfo, newItemInfo);
                PlayerHandlers.OnChangingItem(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ChangingItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnPlayerBanEvent(PlayerBanEventArgs ev)
        {
            try
            {
                if (ev.Duration > 1)
                {
                    var arg = new KickedEventArgs(ev.BannedPlayer.ToExiled(), ev.Reason, ev.Allow);
                    PlayerHandlers.OnKicked(arg);
                    ev.Allow = arg.IsAllowed;
                    ev.Reason = arg.Reason;
                }
                else
                {
                    var arg = new BanningEventArgs(ev.BannedPlayer.ToExiled(), ev.Issuer.ToExiled(), ev.Duration, ev.Reason, ev.Reason);
                    PlayerHandlers.OnBanning(arg);
                    ev.Allow = arg.IsAllowed;
                    ev.Reason = arg.Reason;
                    ev.Duration = arg.Duration;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"BannedAndKickingEventCall fail ! \n{e.Message}");
            }
        }

        private void OnLoadComponentsEvent(LoadComponentEventArgs ev)
        {

        }

        private void OnWarheadDetonationEvent() => WarHeadHandlers.OnDetonated();


        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            try
            {
                var arg = new TriggeringTeslaEventArgs(ev.Player.ToExiled(), ev.Tesla.GameObject.GetComponent<TeslaGate>().PlayerInHurtRange(ev.Player.gameObject), ev.Trigger);
                PlayerHandlers.OnTriggeringTesla(arg);
                ev.Trigger = arg.IsTriggerable;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"TriggeringTeslaEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp914ActivateEvent(Scp914ActivateEventArgs ev)
        {
            try
            {
                List<Pickup> pickups = new List<Pickup>();
                Scp914Machine scp914 = Server.Get.Map.Scp914.GameObject.GetComponent<Scp914Machine>();
                foreach (var synapseItem in ev.Items)
                    pickups.Add(synapseItem.pickup);

                var arg = new UpgradingItemsEventArgs(scp914, ev.Players.ToExiled(), pickups, scp914.knobState, ev.Allow);

                Scp914Handlers.OnUpgradingItems(arg);

                ev.Allow = arg.IsAllowed;
                Synapse.Server.Get.Map.Scp914.KnobState = arg.KnobSetting;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Scp914UpgradingItemsEventCall fail ! \n{e.Message}");
            }
        }

        private void OnLCZDecontaminationEvent(LCZDecontaminationEventArgs ev)
        {
            try
            {
                var arg = new DecontaminatingEventArgs(ev.Allow);

                MapHandlers.OnDecontaminating(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"DecontaminatingEventCall fail ! \n{e.Message}");
            }
        }


        private static IEnumerator<float> ItemDroppedEventCall(PlayerDropItemEventArgs ev)
        {
            yield return Timing.WaitUntilTrue(() => ev.Item.pickup != ev.Player);

            try
            {
                var arg = new ItemDroppedEventArgs(ev.Player.ToExiled(), ev.Item.pickup);

                PlayerHandlers.OnItemDropped(arg);

                yield break;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ItemDroppedEventCall fail ! \n{e.Message}");
            }
        }

        private static IEnumerator<float> ChangedRoleEventCall(PlayerSetClassEventArgs ev)
        {
            RoleType oldRole = ev.Player.RoleType;
            int oldCufferId = ev.Player.Cuffer.PlayerId;
            Vector3 oldPos = ev.Player.Position;
            yield return Timing.WaitUntilTrue(() => ev.Role != ev.Player.RoleType);

            try
            {
                var arg = new ChangedRoleEventArgs(ev.Player.ToExiled(), oldRole, oldCufferId, oldPos == ev.Player.Position, ev.IsEscaping);

                PlayerHandlers.OnChangedRole(arg);

                yield break;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"OnChangedRole fail ! \n{e.Message}");
            }
        }
    }
}
