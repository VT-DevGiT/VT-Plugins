using Grenades;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;

namespace VTGrenad
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent += PlayedDead;
            Server.Get.Events.Player.PlayerPickUpItemEvent += PickingUpItem;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
            if (null != Plugin.Config.Key)
                Server.Get.Events.Player.PlayerDropItemEvent += ItemDropped;
            if (Plugin.Config.ChaineFuseFragGrenad)
                VTController.Server.Events.Grenade.ChangeIntoFragEvent += OnChangeIntoFragEvent;
            if (Plugin.Config.FlashbangFuseWithCollision)
                VTController.Server.Events.Grenade.CollisionGrenadeEvent += OnCollisionGrenade;
            if (Plugin.Config.BadFlash)
                VTController.Server.Events.Grenade.ExplosionGrenadeEvent += OnExplosionGrenade;
        }

        private void OnExplosionGrenade(ExplosionGrenadeEventArgs ev)
        {
            if (ev.Type == GrenadeType.Flashbang)
            {
                ev.Grenade.GetComponent<FlashGrenade>();
                foreach (var joueur in Server.Get.Players)
                {
                    GameObject player = joueur.gameObject;
                    Vector3 position = ev.Grenade.transform.position;
                    ReferenceHub hub = ReferenceHub.GetHub(player);
                    FlashGrenade Flash = ev.Grenade.GetComponent<FlashGrenade>();

                    if (!(Object)ev.Grenade.GetFieldValueorOrPerties<GrenadeManager>("thrower") == (Object)null && Flash.GetFieldValueorOrPerties<bool>("_friendlyFlash"))
                    {
                        float num =
                            Flash.powerOverDistance.Evaluate(Vector3.Distance(player.transform.position, position) / ((double)position.y > 900.0 ?
                            Flash.distanceMultiplierSurface :
                            Flash.distanceMultiplierFacility)) *
                                Flash.powerOverDot.Evaluate(Vector3.Dot(hub.PlayerCameraReference.forward,
                                    (hub.PlayerCameraReference.position - position).normalized));
                        if (num > 0.0)
                        {
                            joueur.GiveEffect(Effect.Deafened, 1, 10);
                            joueur.GiveEffect(Effect.Exhausted, 1, 10);
                        }
                    }
                }
            }
        }

        private void OnCollisionGrenade(CollisionGrenadeEventArgs ev)
        {
            if (ev.Type == GrenadeType.Flashbang)
                ev.Grenade.NetworkfuseTime = 0.01f;
        }

        private void OnChangeIntoFragEvent(ChangeIntoFragEventArgs ev)
        {
            if (ev.Item != null && ev.Grenade != null)
            { 
                Map.Get.Explode(ev.Item.Position, GrenadeType.Grenade, ev.Grenade.throwerGameObject.GetPlayer());
                ev.Item.Despawn();
            }
            ev.Allow = false;
        }   

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode == Plugin.Config.Key)
            {
                ev.Player.ExecuteCommand(".Boom", false);
            }
        }

        private void PickingUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Item != null)
            {
                var keys = Plugin.DictTabletteGrenades.Keys.ToList();
                foreach (var key in keys)
                {
                    var list = Plugin.DictTabletteGrenades[key];
                    if (list.Any(p => p.GrItem == ev.Item))
                    {
                        Plugin.DictTabletteGrenades[key] = list.Where(p => p.GrItem != ev.Item).ToList();
                    }
                }
            }
        }
        private void ItemDropped(PlayerDropItemEventArgs ev)
        {
            if (ev.Item != null && (ev.Item.ItemType == ItemType.GrenadeFrag 
                || (Plugin.Config.FlashRemot && ev.Item.ItemType == ItemType.GrenadeFlash)) 
                && ev.Player?.ItemInHand?.ID == (int)ItemType.WeaponManagerTablet 
                && !ev.Player.ItemInHand.IsCustomItem)
            {
                List<AmorcableGrenade> listGrenade = Plugin.DictTabletteGrenades.ContainsKey(ev.Player.PlayerId) ? Plugin.DictTabletteGrenades[ev.Player.PlayerId] : new List<AmorcableGrenade>();

                if (listGrenade != null && !listGrenade.Any(p => p.GrItem == ev.Item))
                {
                    listGrenade.Add(new AmorcableGrenade(ev.Item));
                    if (!Plugin.DictTabletteGrenades.ContainsKey(ev.Player.PlayerId))
                    {
                        Plugin.DictTabletteGrenades.Add(ev.Player.PlayerId, listGrenade);
                    }
                }
            }
            ev.Allow = true;
        }
        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (Plugin.DictTabletteGrenades.ContainsKey(ev.Target.PlayerId))
            {
                Plugin.DictTabletteGrenades.Remove(ev.Target.PlayerId);
            }
        }

        private void PlayedDead(PlayerDeathEventArgs ev)
        {
            if (Plugin.DictTabletteGrenades.ContainsKey(ev.Victim.PlayerId))
            {
                Plugin.DictTabletteGrenades.Remove(ev.Victim.PlayerId);
            }
        }
    }
}
