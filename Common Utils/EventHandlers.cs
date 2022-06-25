using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System.Linq;
using UnityEngine;
using VT_Api.Extension;
using Common_Utiles.Config;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            VtController.Get.Events.Map.Scp914UpgradeItemEvent += On914Upgrade;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        System.Func<float, float, float> floatRnd = (min, max) => Random.Range(min, max);
        System.Func<int, int, int> intRnd = (min, max) => Random.Range(min, max);

        Common_Utiles.Config.Config cfg => CommonUtiles.Instance.Config;
        public bool RespawnAllow { get; set; }
        
        private void OnStart() => RespawnAllow = true;

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (!RespawnAllow)
                ev.Allow = false;
        }

        private void On914Activate(Scp914ActivateEventArgs ev)
        {
            if (!ev.Players.Any()) foreach (var player in ev.Players)
                Player914(player);
        }

        private void Player914(Player player)
        {
            if (CommonUtiles.Instance.Config.Rnd914Size)
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
            if (cfg.Rnd914Life)
            {
                var newLif = intRnd(cfg.Min914Life, cfg.Max914Life);
                player.Health = newLif;
            }
            if (cfg.Rnd914ArtificialLife)
            {
                var newLif = intRnd(cfg.Min914ArtificialLife, cfg.Max914ArtificialLife);
                player.ArtificialHealth = newLif;
            }
            if (cfg.Rnd914ChanceDie != 0)
            {
                var Rnd = floatRnd(0, 100);
                if (Rnd >= cfg.Rnd914ChanceDie)
                    player.Kill("Crush by SCP 914");
            }
        }

        private void On914Upgrade(VT_Api.Core.Events.EventArguments.Scp914UpgradeItemEventArgs ev)
        {
            var NewIdItems = cfg.Recipes.FirstOrDefault(r => r.ItemID == ev.Item.ID).Parse(ev.Setting);
            if (NewIdItems == null)
                ev.NewItem = cfg.RemouvRecipes ? SynapseItem.None : null;
            else ev.NewItem = new SynapseItem(NewIdItems[intRnd(0, NewIdItems.Count - 1)]);
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
            
            if (cfg.RolesInfos.ContainsKey(roleId))
            {
                ev.Player.GiveTextHint(cfg.RolesInfos[roleId]);
            }
        }

    }
}