using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Exiled.Loader.Features;
using HarmonyLib;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VT_MultieLoder.API;
using VT_Referance.Method;
using MapHandlers = Exiled.Events.Handlers.Map;
using PlayerHandlers = Exiled.Events.Handlers.Player;
using Scp079Handlers = Exiled.Events.Handlers.Scp079;
using Scp096Handlers = Exiled.Events.Handlers.Scp096;
using Scp106Handlers = Exiled.Events.Handlers.Scp106;
using Scp173Handlers = Exiled.Events.Handlers.Scp173;
using Scp914Handlers = Exiled.Events.Handlers.Scp914;
using ServerHandlers = Exiled.Events.Handlers.Server;
using WarHeadHandlers = Exiled.Events.Handlers.Warhead;

namespace VT_MultieLoder.Exiled.Event
{
    public class EventHandler
    {
        public EventHandler()
        {
            
            VT_Referance.VTController.Server.Events.Map.Scp914ActivateEvent += OnScp914ActivateEvent;
            Server.Get.Events.Map.LCZDecontaminationEvent += OnLCZDecontaminationEvent;
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Map.WarheadDetonationEvent += OnWarheadDetonationEvent;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteractEvent;
            Server.Get.Events.Player.LoadComponentsEvent += OnLoadComponentsEvent;
            Server.Get.Events.Player.PlayerBanEvent += OnPlayerBanEvent;
            Server.Get.Events.Player.PlayerChangeItemEvent += OnPlayerChangeItemEvent;
            Server.Get.Events.Player.PlayerStartWorkstationEvent += OnPlayerConnectWorkstationEvent;
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
            Server.Get.Events.Player.PlayerJoinEvent += OnPlayerJoinEvent;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnPlayerKeyPressEvent;
            Server.Get.Events.Player.PlayerLeaveEvent += OnPlayerLeaveEvent;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPlayerPickUpItemEvent;
            Server.Get.Events.Player.PlayerReloadEvent += OnPlayerReloadEvent;
            Server.Get.Events.Player.PlayerReportEvent += OnPlayerReportEvent;
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
            Server.Get.Events.Player.PlayerShootEvent += OnPlayerShootEvent;
            Server.Get.Events.Player.PlayerSpeakEvent += OnPlayerSpeakEvent;
            Server.Get.Events.Player.PlayerUncuffTargetEvent += OnPlayerUncuffTargetEvent;
            Server.Get.Events.Player.PlayerUseMicroEvent += OnPlayerUseMicroEvent;
            Server.Get.Events.Player.PlayerWalkOnSinkholeEvent += OnPlayerWalkOnSinkholeEvent;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItemEvent;
            Server.Get.Events.Round.RoundCheckEvent += OnRoundCheckEvent;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEndEvent;
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestartEvent;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStartEvent;
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawnPlayersEvent;
            Server.Get.Events.Round.TeamRespawnEvent += OnTeamRespawnEvent;
            Server.Get.Events.Round.WaitingForPlayersEvent += OnWaitingForPlayersEvent;
            Server.Get.Events.Scp.Scp079.CameraSwitch += OnScp079CameraSwitch;
            Server.Get.Events.Scp.Scp079.DoorInteract += OnScp079DoorInteract;
            Server.Get.Events.Scp.Scp079.ElevatorInteract += OnScp079ElevatorInteract;
            Server.Get.Events.Scp.Scp079.RecontainEvent += OnScp079RecontainEvent;
            Server.Get.Events.Scp.Scp079.RoomLockdown += OnScp079RoomLockdown;
            Server.Get.Events.Scp.Scp079.SpeakerInteract += OnScp079SpeakerInteract;
            Server.Get.Events.Scp.Scp079.TeslaInteract += OnScp079TeslaInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnScp096AddTargetEvent;
            Server.Get.Events.Scp.Scp106.PocketDimensionEnterEvent += OnPocketDimensionEnterEvent;
            Server.Get.Events.Scp.Scp106.PocketDimensionLeaveEvent += OnPocketDimensionLeaveEvent;
            Server.Get.Events.Scp.Scp106.PortalCreateEvent += OnPortalCreateEvent;
            Server.Get.Events.Scp.Scp106.Scp106ContainmentEvent += OnScp106ContainmentEvent;
            Server.Get.Events.Scp.Scp173.Scp173BlinkEvent += OnScp173BlinkEvent;
            Server.Get.Events.Scp.ScpAttackEvent += OnScpAttackEvent;
            Server.Get.Events.Server.ConsoleCommandEvent += OnConsoleCommandEvent;
            //Server.Get.Events.Server.PreAuthenticationEvent += OnPreAuthenticationEvent; error repatch this method
            Server.Get.Events.Server.RemoteAdminCommandEvent += OnRemoteAdminCommandEvent;
            Server.Get.Events.Server.TransmitPlayerDataEvent += OnTransmitPlayerDataEvent;
            Server.Get.Events.Server.UpdateEvent += OnUpdateEvent;
        }
        private void OnUseItemEvent(PlayerItemInteractEventArgs ev)
        {
            try
            {
                var arg = new UsedItemEventArgs(ev.Player.ToExiled(), (InventorySystem.Items.Usables.UsableItem)ev.CurrentItem.ItemBase);

                PlayerHandlers.OnItemUsed(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079TeslaInteract(Scp079TeslaInteractEventArgs ev)
        {
            try
            {
                var arg = new InteractingTeslaEventArgs(ev.Scp079.ToExiled(), ev.Tesla.GameObject.GetComponent<TeslaGate>(), ev.EnergyNeeded);

                Scp079Handlers.OnInteractingTesla(arg);

                ev.Result = arg.IsAllowed ? Scp079EventMisc.InteractionResult.Allow : Scp079EventMisc.InteractionResult.Disallow;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079SpeakerInteract(Scp079SpeakerInteractEventArgs ev)
        {
            try
            {
                var arg = new StartingSpeakerEventArgs(ev.Scp079.ToExiled(), ev.Scp079.Room.GameObject.GetComponent<global::Exiled.API.Features.Room>(), ev.EnergyNeeded);

                Scp079Handlers.OnStartingSpeaker(arg);

                ev.Result = arg.IsAllowed ? Scp079EventMisc.InteractionResult.Allow : Scp079EventMisc.InteractionResult.Disallow;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079RoomLockdown(Scp079RoomLockdownEventArgs ev)
        {
            try
            {
                var arg = new LockingDownEventArgs(ev.Scp079.ToExiled(), ev.Room.Identifier, ev.EnergyNeeded);

                Scp079Handlers.OnLockingDown(arg);

                ev.Result = arg.IsAllowed ? Scp079EventMisc.InteractionResult.Allow : Scp079EventMisc.InteractionResult.Disallow;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079ElevatorInteract(Scp079ElevatorInteractEventArgs ev)
        {
            try
            {
                var arg = new ElevatorTeleportEventArgs(ev.Scp079.ToExiled(), ev.Scp079.Scp079Controller.Camera.GameObject.GetComponent<Camera079>(), ev.EnergyNeeded);

                Scp079Handlers.OnElevatorTeleporting(arg);

                ev.Result = arg.IsAllowed ? Scp079EventMisc.InteractionResult.Allow : Scp079EventMisc.InteractionResult.Disallow;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079DoorInteract(Scp079DoorInteractEventArgs ev)
        {
            try
            {
                var arg = new TriggeringDoorEventArgs(ev.Scp079.ToExiled(), ev.Door.GameObject.GetComponent<Interactables.Interobjects.DoorUtils.DoorVariant>(), ev.EnergyNeeded);

                Scp079Handlers.OnTriggeringDoor(arg);

                ev.Result = arg.IsAllowed ? Scp079EventMisc.InteractionResult.Allow : Scp079EventMisc.InteractionResult.Disallow;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
        }

        private void OnScp079CameraSwitch(Scp079CameraSwitchEventArgs ev)
        {
            try
            {
                var arg = new ChangingCameraEventArgs(ev.Scp079.ToExiled(), ev.Camera.GameObject.GetComponent<Camera079>(), 0);

                Scp079Handlers.OnChangingCamera(arg);

                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"UseItemEventCall fail ! \n{e.Message}");
            }
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
            /*
            try
            {
                (string name, string[] arguments) = ev.Command.ExtractCommand();
                var arg = new SendingRemoteAdminCommandEventArgs(ev.Sender, ExiledPlayer.Get(ev.Sender), name, arguments.ToList(), ev.Allow);

                ServerHandlers.OnSendingRemoteAdminCommand(arg);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"RemoteAdminCommandEventCall fail ! \n{e.Message}");
            }
            */
        }

        private void OnPreAuthenticationEvent(PreAuthenticationEventArgs ev)
        {
            try 
            {
                var arg = new PreAuthenticatingEventArgs(ev.UserId, ev.Request, 0, 0, "null", 0);
                arg.IsAllowed = ev.Allow;
                PlayerHandlers.OnPreAuthenticating(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"PreAuthenticationEventCall fail ! \n{e.Message}");
            }
        }

        private void OnConsoleCommandEvent(ConsoleCommandEventArgs ev)
        {
            /*
            try
            {
                (string name, string[] arguments) = ev.Command.ExtractCommand();
                var arg = new SendingConsoleCommandEventArgs(ev.Player.ToExiled(), name, arguments.ToList(), false);

                ServerHandlers.OnSendingConsoleCommand(arg);

                arg.Player.SendConsoleMessage(arg.ReturnMessage, arg.Color);
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"ConsoleCommandEventCall fail ! \n{e.Message}");
            }
            */
        }

        private void OnScpAttackEvent(ScpAttackEventArgs ev)
        {

        }

        private void OnScp173BlinkEvent(Scp173BlinkEventArgs ev)
        {
            try
            {
                var arg = new BlinkingEventArgs(ev.Scp173.ToExiled(), ev.Scp173.Scp173Controller.ConfrontingPlayers.ToExiled(), ev.Position);

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
                    _type = ev.TeleportType,
                };

                var arg = new FailingEscapePocketDimensionEventArgs(ev.Player.ToExiled(), pdt, ev.Allow);

                PlayerHandlers.OnFailingEscapePocketDimension(arg);

                ev.Allow = arg.IsAllowed;
                ev.TeleportType = arg.Teleporter._type;
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
                var arg = new AddingTargetEventArgs(ev.Scp096.ToExiled(), ev.Player.ToExiled(), ev.Scp096.Scp096Controller.EnrageTimeLeft, ev.Allow);
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
                MultiAdminFeatures.CallEvent(MultiAdminFeatures.EventType.WAITING_FOR_PLAYERS);

                if (global::Exiled.Events.Events.Instance.Config.ShouldReloadConfigsAtRoundRestart)
                {
                    global::Exiled.Loader.ConfigManager.Reload();
                }

                if (global::Exiled.Events.Events.Instance.Config.ShouldReloadTranslationsAtRoundRestart)
                {
                    global::Exiled.Loader.TranslationManager.Reload();
                }

                RoundSummary.RoundLock = false;

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

                var arg = new RespawningTeamEventArgs(ev.Players.ToExiled(), ev.Players.Count, ev.Team, ev.Allow);

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
                    var arg1 = new SpawningEventArgs(spawnPlayer.Key.ToExiled(), (RoleType)spawnPlayer.Value);
                    PlayerHandlers.OnSpawning(arg1);

                    var arg2 = new ChangingRoleEventArgs(spawnPlayer.Key.ToExiled(), (RoleType)spawnPlayer.Value, false, CharacterClassManager.SpawnReason.RoundStart);
                    PlayerHandlers.OnChangingRole(arg2);
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
                MultiAdminFeatures.CallEvent(MultiAdminFeatures.EventType.ROUND_START);
                ServerHandlers.OnRoundStarted();
                
                var ass = Assembly.GetAssembly(typeof(global::Exiled.Events.Events));
                var type = ass.GetType("Exiled.Events.Handlers.Internal.MapGenerated");
                type.CallMethod("OnMapGenerated");
                
                
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
                MultiAdminFeatures.CallEvent(MultiAdminFeatures.EventType.ROUND_END);

                global::Exiled.API.Features.Scp173.TurnedPlayers.Clear();
                global::Exiled.API.Features.Scp096.TurnedPlayers.Clear();
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
                UsingMicroHIDEnergyEventArgs arg = new UsingMicroHIDEnergyEventArgs(ev.Player.ToExiled(), (InventorySystem.Items.MicroHID.MicroHIDItem)ev.Micro.ItemBase, ev.State, 1, true);

                PlayerHandlers.OnUsingMicroHIDEnergy(arg);
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
                var arg = new DeactivatingWorkstationEventArgs(ev.WorkStation.GameObject.GetComponent<InventorySystem.Items.Firearms.Attachments.WorkstationController>(), ev.Allow);
                PlayerHandlers.OnDeactivatingWorkstation(arg);
                ev.Allow = arg.IsAllowed;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"DeactivatingWorkstationEventCall fail ! \n{e.Message}");
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

                var arg = new ShootingEventArgs(ev.Player.ToExiled(), new InventorySystem.Items.Firearms.BasicMessages.ShotMessage()
                {
                    ShooterCameraRotation = ev.Player.Rotation.y,
                    ShooterCharacterRotation = ev.Player.Rotation.x,
                    ShooterPosition = ev.Player.Position,
                    ShooterWeaponSerial = ev.Weapon.Serial,
                    TargetNetId = ev.Target.NetworkIdentity.netId,
                    TargetPosition = ev.Target.Position,
                    TargetRotation = new Quaternion(ev.Target.Rotation.x, ev.Target.Rotation.y, 0, 0)
                });
                arg.IsAllowed = ev.Allow;

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
                if (ev.Player != null || string.IsNullOrEmpty(ev.Player.UserId))
                    return;

                if (ev.Role == RoleType.Spectator && global::Exiled.Events.Events.Instance.Config.ShouldDropInventory)
                    ev.Player.Inventory.DropAll();

                var arg = new ChangingRoleEventArgs(ev.Player.ToExiled(), ev.Role, false, CharacterClassManager.SpawnReason.Respawn);

                PlayerHandlers.OnChangingRole(arg);

                ev.Role = arg.NewRole;
                ev.Allow = arg.NewRole != ev.Role && arg.IsAllowed;
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
                var arg = new ReloadingWeaponEventArgs(ev.Player.ToExiled(), ev.Allow);
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
                if (ev.Item.ItemType.ToString().Contains("ammo"))
                {

                    var arg = new PickingUpAmmoEventArgs(ev.Player.ToExiled(), ev.Item.PickupBase, ev.Allow);

                    PlayerHandlers.OnPickingUpAmmo(arg);

                    ev.Allow = arg.IsAllowed;
                }
                else
                {
                    var arg = new PickingUpItemEventArgs(ev.Player.ToExiled(), ev.Item.PickupBase, ev.Allow);

                    PlayerHandlers.OnPickingUpItem(arg);

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

        private void OnPlayerHealEvent(PlayerHealEventArgs ev)
        {

        }

        private void OnPlayerGeneratorInteractEvent(PlayerGeneratorInteractEventArgs ev)
        {
            try
            {
                MapGeneration.Distributors.Scp079Generator generator = ev.Generator.GameObject.GetComponent<MapGeneration.Distributors.Scp079Generator>();
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
                    case Synapse.Api.Enum.GeneratorInteraction.Activated:
                        var arg3 = new ActivatingGeneratorEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnActivatingGenerator(arg3);
                        ev.Allow = arg3.IsAllowed;
                        break;
                    case Synapse.Api.Enum.GeneratorInteraction.Disabled:
                        var arg4 = new StoppingGeneratorEventArgs(ev.Player.ToExiled(), generator, ev.Allow);

                        PlayerHandlers.OnStoppingGenerator(arg4);
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
                var arg = new ChangingRoleEventArgs(ev.Player.ToExiled(), (RoleType)ev.SpawnRole, false, CharacterClassManager.SpawnReason.Escaped);

                PlayerHandlers.OnChangingRole(arg);


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
                var arg = new DroppingItemEventArgs(ev.Player.ToExiled(), ev.Item.ItemBase, ev.Allow);
                PlayerHandlers.OnDroppingItem(arg);
                ev.Allow = arg.IsAllowed;
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
                //PlayerHandlers.OnDied(new DiedEventArgs(ev.Victim.ToExiled()));
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
/*                var arg = new HurtingEventArgs(ev.Killer.ToExiled(), ev.Victim.ToExiled(), ev.HitInfo, ev.Allow);
                
                PlayerHandlers.OnHurting(arg);    */
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

        private void OnPlayerConnectWorkstationEvent(PlayerStartWorkstationEventArgs ev)
        {
            try
            {
                var arg = new ActivatingWorkstationEventArgs(ev.Player.ToExiled(), ev.WorkStation.GameObject.GetComponent<InventorySystem.Items.Firearms.Attachments.WorkstationController>(), ev.Allow);
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
                InventorySystem.Items.ItemBase newItemInfo;
                if (ev.NewItem != null)
                { 
                    newItemInfo = ev.NewItem.ItemBase;
                    var arg = new ChangingItemEventArgs(ev.Player.ToExiled(), newItemInfo);
                    PlayerHandlers.OnChangingItem(arg);
                }
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
                if (ev.Allow)
                {
                    var arg = new BanningEventArgs(ev.BannedPlayer.ToExiled(), ev.Issuer.ToExiled(), ev.BanDuration, ev.Reason, ev.Reason);
                    PlayerHandlers.OnBanning(arg);
                    ev.Allow = arg.IsAllowed;
                    ev.Reason = arg.Reason;
                    ev.BanDuration = arg.Duration;
                }
                else if (ev.BanDuration > 1 && ev.Allow)
                {
                    var arg = new KickedEventArgs(ev.BannedPlayer.ToExiled(), ev.Reason);
                    PlayerHandlers.OnKicked(arg);
                    ev.Reason = arg.Reason;
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

        private void OnScp914ActivateEvent(VT_Referance.Event.EventArguments.Scp914ActivateEventArgs ev)
        {
            try
            {
                var arg = new ActivatingEventArgs(ev.Player.ToExiled(), ev.Allow);

                Scp914Handlers.OnActivating(arg);

                ev.Allow = arg.IsAllowed;
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
    }

    [HarmonyPatch(typeof(global::Exiled.Events.Events), nameof(global::Exiled.Events.Events.OnEnabled))]
    internal static class KillExiledEventPatch
    {
        [HarmonyPrefix]
        private static bool killExiledPatching(global::Exiled.Events.Events __instance) => false;
    }

    [HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
    internal static class ServerNamePatch
    {
        [HarmonyPostfix]
        private static void ExiledTagName()
        {
            var instance = global::Exiled.Events.Events.Instance;
            if (!instance.Config.IsNameTrackingEnabled)
                return;

            ServerConsole._serverName += $"<color=#00000000><size=1>Exiled {instance.Version.ToString(3)}</size></color>";
        }
    }
}
