using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Api.Teams;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Extension;
using VT_Api.Reflexion;

using SynapseTeamManger = Synapse.Api.Teams.TeamManager;
using VtRoleManager = VT_Api.Core.Roles.RoleManager;

namespace Common_Utiles
{
    public class EventHandlers
    {

        #region Properties & Variables
        System.Func<float, float, float> floatRnd = (min, max) => Random.Range(min, max);
        System.Func<int, int, int> intRnd = (min, max) => Random.Range(min, max);

        bool firstStart = true;
        #endregion

        #region Constructor & Destructor
        public EventHandlers()
        {
            VtController.Get.Events.Map.Scp914UpgradeItemEvent += OnUpgrade;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;            
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
        }
        #endregion

        #region Events
        private void OnRoundStart()
        {
            Plugin plugin = Plugin.Instance;
            plugin.RespawnAllow = true;
        
            if (firstStart)
            {
                firstStart = false;
                List<ISynapseTeam> teams = SynapseTeamManger.Get.GetFieldValueOrPerties<List<ISynapseTeam>>("teams");
                foreach (var team in teams)
                {
                    int teamID = team.Info.ID;
                    var roles = VtRoleManager.Get.GetRoles(teamID);
                    plugin.TeamIDRolesID.Add(teamID, roles);
                }
                int[] vanillateam = { 0, 1, 2, 3, 4 };
                foreach (var team in vanillateam)
                {
                    var roles = VtRoleManager.Get.GetRoles(team);
                    plugin.TeamIDRolesID.Add(team, roles);
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
            Config.Config config = Plugin.Instance.Config;
            Config.Serialized914Recipe NewIdItems = config.Recipes.FirstOrDefault(x => x.ItemID == ev.Item.ID);

            if (NewIdItems != null)
            {
                var Ids = NewIdItems.Parse(Map.Get.Scp914.KnobState);
                ev.NewItem = new SynapseItem(Ids[intRnd(0, Ids.Count - 1)]);
            }
            else if(config.RemouvRecipes)
            {
                    ev.KeepOldItem = true;
                    ev.NewItem = SynapseItem.None;
            }
        }

        private void On914Activate(Synapse.Api.Events.SynapseEventArguments.Scp914ActivateEventArgs ev)
        {
            if (!ev.Players.Any()) return;
                    
            foreach (var player in ev.Players)
                Player914(player);
        }

        private void Player914(Player player)
        {
            var config = Plugin.Instance.Config;
            if (Plugin.Instance.Config.Rnd914Size)
            {
                float newScaleX = floatRnd(config.Min914SizeX, config.Max914SizeX);
                float newScaleY = floatRnd(config.Min914SizeY, config.Max914SizeY);
                float newScaleZ = floatRnd(config.Min914SizeZ, config.Max914SizeZ);
                player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
            }     
            if (config.Rnd914Roles.Any())
            {
                foreach (var role in config.Rnd914Roles)
                    role.Apply(player, Server.Get.Map.Scp914.KnobState);
            }
                 
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            var config = Plugin.Instance.Config;

            if (ev.Role == RoleType.Spectator && ev.Player.Scale != new Vector3(1, 1, 1))
            {
                ev.Player.Scale = new Vector3(1, 1, 1);
                return;
            }

            var roleId = ev.Player.CustomRole != null ? ev.Player.CustomRole.GetRoleID() : (int)ev.Role;

            if (config.configClasses.ContainsKey(roleId))
            {
                var playerRole = config.configClasses[roleId];

                playerRole.Extract(ev.Player, out var mapPoint, out var rotation, out var items, out var ammos);
                
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
        #endregion


    }
}