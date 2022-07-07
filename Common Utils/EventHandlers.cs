using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System.Linq;
using UnityEngine;
using VT_Api.Extension;
using LightContainmentZoneDecontamination;
using VT_Api.Core.Events.EventArguments;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse.Api.Enum;
using VT_Api.Reflexion;
using static LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            VtController.Get.Events.Map.GeneratorActivatedEvent += OnGeneratorActivated;
            VtController.Get.Events.Map.Scp914UpgradeItemEvent += OnUpgrade;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Player.PlayerLeaveEvent += OnLeave;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;          
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            VtController.Get.Events.Player.PlayerDeathReasonEvent += Death;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;            
        }

        private void OnLeave(PlayerLeaveEventArgs ev)
        {
            ev.Player.SetData("poseffect", "false");
            ev.Player.SetData("supeffect", "false");
        }

        private void Death(PlayerKillEventArgs ev)
        {
            ev.Victim.SetData("poseffect", "false");
            ev.Victim.SetData("supeffect", "false");
            if (ev.Victim.GetData("1up") == "true")
            {
                ev.Victim.SetData("1up", "false");
                Synapse.Api.Logger.Get.Info(ev.Victim.Position);
                Synapse.Api.Logger.Get.Info(ev.Victim.RoleID);
                var pos = ev.Victim.Position;
                var role = ev.Victim.RoleID;
                Timing.CallDelayed(10f,() =>
                {
                    ev.Victim.RoleID = role;
                    ev.Victim.Position = pos;
                });
            }
            
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if(ev.Player.GetData("poseffect") == "true 6")
            {
                ev.Item.Destroy();
                ev.Player.Inventory.AddItem(new SynapseItem(Random.Range(0, 47)));
            }
        }

        System.Func<float, float, float> floatRnd = (min, max) => Random.Range(min, max);
        System.Func<int, int, int> intRnd = (min, max) => Random.Range(min, max);

        public Config.Config cfg => Plugin.Instance.Config;
        public bool RespawnAllow { get; set; }                
      
        private void OnRoundStart() => RespawnAllow = true;                

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (!RespawnAllow)
                ev.Allow = false;
        }

        private void OnGeneratorActivated(GeneratorActivatedEventArgs ev)
        {

            if (Map.Get.Generators.Where(p => p.Engaged == true).Count() == 2)
            {
                Map.Get.Cassie("Decontamination sequence commencing in 2 minutes");
                
                VTIntercom.Starting.phase = 0;
                DecontaminationController.Singleton.disableDecontamination = false;
                var phase = DecontaminationController.Singleton.DecontaminationPhases;
                for (int i = 0; i < phase.Length; i++)
                {
                    var elem = phase[i];
                }
                DecontaminationController.DecontaminationPhase[] newPhase = new DecontaminationController.DecontaminationPhase[3];
                
                newPhase[0] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 1,
                    Function = PhaseFunction.None,
                    AnnouncementLine = phase[phase.Length - 3].AnnouncementLine
                };
                
                newPhase[1] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 2,
                    Function = PhaseFunction.OpenCheckpoints,
                    AnnouncementLine = phase[phase.Length - 2].AnnouncementLine,

                };
                
                newPhase[2] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 120,
                    Function = PhaseFunction.Final,
                    AnnouncementLine = phase[phase.Length - 1].AnnouncementLine
                };
                
                VTIntercom.Starting.DecontaminationPhases = newPhase;
                DecontaminationController.Singleton.SetField<int>("_nextPhase", 0);
                foreach (var room in Server.Get.Map.Rooms.FindAll(p => p.Zone == ZoneType.LCZ)) foreach (var door in room.Doors)
                        if (door.DoorPermissions.RequiredPermissions == KeycardPermissions.None)
                        {
                            door.Locked = true;
                            door.Open = true;
                        }
                
                Timing.CallDelayed(60f, () =>
                {
                    Map.Get.Cassie("Decontamination sequence commencing in 1 minute");
                });
                
                Timing.CallDelayed(125f, () =>
                {
                    Map.Get.Cassie("Light Containment Zone is locked down and ready for decontamination .");
                });
                VTIntercom.Plugin.Instance.DecontInProgress = true;
            }
        }
        
        private void OnUpgrade(Scp914UpgradeItemEventArgs ev)
        {            
            var NewIdItems = cfg.Recipes.FirstOrDefault(x => x.ItemID == ev.Item.ID);
            
            if (NewIdItems == null)
            {
                ev.KeepOldItem = true;
            }
            else
            {
                var Ids = NewIdItems.Parse(Map.Get.Scp914.KnobState);
                
                ev.NewItem = new SynapseItem(Ids[intRnd(0, Ids.Count - 1)]);                                
            }
        }

        private void On914Activate(Synapse.Api.Events.SynapseEventArguments.Scp914ActivateEventArgs ev)
        {
            
            if (ev.Players.Any())
            {
                Synapse.Api.Logger.Get.Info("YO");
                foreach (var player in ev.Players)
                {
                    Player914(player);
                }
            }                                
        }

        private void Player914(Player player)
        {
            Synapse.Api.Logger.Get.Info("YO2");
            if (Plugin.Instance.Config.Rnd914Size)
            {
                float newScaleX = floatRnd(cfg.Min914SizeX, cfg.Max914SizeX);
                float newScaleY = floatRnd(cfg.Min914SizeY, cfg.Max914SizeY);
                float newScaleZ = floatRnd(cfg.Min914SizeZ, cfg.Max914SizeZ);
                player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
            }
            if (cfg.list914Effect.Any())
            {
                foreach (var effect in cfg.list914Effect)
                    effect.Apply(player, Server.Get.Map.Scp914.KnobState);
            }
            if (cfg.Rnd914Roles.Any())
            {
                foreach (var role in cfg.Rnd914Roles)
                    role.Apply(player, Server.Get.Map.Scp914.KnobState);
            }
                 
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {

            if (ev.Role == RoleType.Spectator && ev.Player.Scale != new Vector3(1, 1, 1))
            {
                ev.Player.Scale = new Vector3(1, 1, 1);
                return;
            }

            var roleId = ev.Player.CustomRole != null ? ev.Player.CustomRole.GetRoleID() : (int)ev.Role;

            if (cfg.configClasses.ContainsKey(roleId))
            {
                var config = cfg.configClasses[roleId];
                
                config.Extract(ev.Player, out var mapPoint, out var rotation, out var items, out var ammos);
                
                if (mapPoint != null)
                    ev.Position = mapPoint.Position;

                if (rotation != null)
                    ev.Rotation = rotation.x;
                
                if (items != null)
                    ev.Items = items;
                
                if (ammos != null)
                    ev.Ammo = ammos;
            }
            
        }

    }
}