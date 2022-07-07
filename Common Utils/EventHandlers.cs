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
using System.Collections.Generic;
using Synapse.Api.Teams;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            VtController.Get.Events.Map.Scp914UpgradeItemEvent += OnUpgrade;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;            
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
        }

        System.Func<float, float, float> floatRnd = (min, max) => Random.Range(min, max);
        System.Func<int, int, int> intRnd = (min, max) => Random.Range(min, max);
        Config.Config cfg => Plugin.Instance.Config;
        

        bool firstStart = true;

        private void OnRoundStart()
        {
            Plugin.Instance.RespawnAllow = true;
        
            if (firstStart)
            {
                firstStart = false;
                foreach (var team in Synapse.Api.Teams.TeamManager.Get.GetFieldValueOrPerties<List<ISynapseTeam>>("teams"))
                {
                    int teamID = team.Info.ID;
                    var roles = VT_Api.Core.Roles.RoleManager.Get.GetRoles(teamID);
                    Plugin.Instance.TeamIDRolesID.Add(teamID, roles);
                }
            }
        }
        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (!Plugin.Instance.RespawnAllow)
                ev.Allow = false;
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
                foreach (var player in ev.Players)
                {
                    Player914(player);
                }
            }                                
        }

        private void Player914(Player player)
        {            
            if (Plugin.Instance.Config.Rnd914Size)
            {
                float newScaleX = floatRnd(cfg.Min914SizeX, cfg.Max914SizeX);
                float newScaleY = floatRnd(cfg.Min914SizeY, cfg.Max914SizeY);
                float newScaleZ = floatRnd(cfg.Min914SizeZ, cfg.Max914SizeZ);
                player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
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