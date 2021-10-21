using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance;
using VT_Referance.Method;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            VTController.Server.Events.Player.PlayerSetClassEvent += OnSpawn;
            VTController.Server.Events.Map.Scp914UpgradeItemEvent += On914Upgrade;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        System.Func<float, float, float> floatRnd = (min, max) => Random.Range(min, max);
        System.Func<int, int, int> intRnd = (min, max) => Random.Range(min, max);

        private void OnStart() => CommonUtiles.Instance.RespawnAllow = true;

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (!CommonUtiles.Instance.RespawnAllow)
                ev.Allow = false;
        }

        private void On914Activate(Scp914ActivateEventArgs ev)
        {
            if (!ev.Players.Any()) foreach (var player in ev.Players)
                Player914(player);
        }

        private void Player914(Player player)
        {
            if (CommonUtiles.Config.Rnd914Size)
            {
                float newScaleX = floatRnd(CommonUtiles.Config.Min914SizeX, CommonUtiles.Config.Max914SizeX);
                float newScaleY = floatRnd(CommonUtiles.Config.Min914SizeY, CommonUtiles.Config.Max914SizeY);
                float newScaleZ = floatRnd(CommonUtiles.Config.Min914SizeZ, CommonUtiles.Config.Max914SizeZ);
                player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
            }
            if (CommonUtiles.Config.list914Effect.Any())
            {
                Effect effect = CommonUtiles.Config.list914Effect[intRnd(0, CommonUtiles.Config.list914Effect.Count() - 1)];
                player.GiveEffect(effect);
            }
            if (CommonUtiles.Config.Rnd914Life)
            {
                int newLif = intRnd(CommonUtiles.Config.Min914Life, CommonUtiles.Config.Max914Life);
                player.Health = newLif;
            }
            if (CommonUtiles.Config.Rnd914ArtificialLife)
            {
                int newLif = intRnd(CommonUtiles.Config.Min914ArtificialLife, CommonUtiles.Config.Max914ArtificialLife);
                player.ArtificialHealth = newLif;
            }
            if (CommonUtiles.Config.Rnd914ChanceDie != 0)
            {
                float Rnd = floatRnd(0, 100);
                if (Rnd >= CommonUtiles.Config.Rnd914ChanceDie)
                    player.Kill();
            }
        }

        private void On914Upgrade(VT_Referance.Event.EventArguments.Scp914UpgradeItemEventArgs ev)
        {
            List<int> NewIdItems = CommonUtiles.Config.Recipes.FirstOrDefault(r => r.ItemID == ev.Item.ID).Parse(ev.Setting);
            if (NewIdItems == null)
                ev.NewItem = CommonUtiles.Config.RemouvRecipes ? SynapseItem.None : null;
            else ev.NewItem = new SynapseItem(NewIdItems[intRnd(0, NewIdItems.Count - 1)]);
        }

        private void OnSpawn(VT_Referance.Event.EventArguments.PlayerSetClassEventArgs ev)
        {
            Timing.CallDelayed(0.015f, () =>
            {
                if (ev.NewID == (int)RoleType.Spectator && ev.Player.Scale != new Vector3(1, 1, 1))
                    ev.Player.Scale = new Vector3(1, 1, 1);

                SerializedPlayerInventory nullSerializedPlayerInventory = new SerializedPlayerInventory();

                SerializedConfigClass config = CommonUtiles.Config.configClasses.Where(x => x.ClassID == ev.Player.RoleID).FirstOrDefault();
                config?.Applied(ev.Player);
            });
        }

    }
}