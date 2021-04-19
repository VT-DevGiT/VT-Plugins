using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VTGrenad
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent += PlayedDead;
            Server.Get.Events.Player.PlayerDropItemEvent += ItemDropped;
            Server.Get.Events.Player.PlayerPickUpItemEvent += PickingUpItem;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode == Plugin.Config.Key)
            {
                if (!Plugin.Config.NotAGrenadeRole.Contains(ev.Player.RoleID) 
                    && ev.Player?.ItemInHand?.ID == (int)ItemType.WeaponManagerTablet 
                    && !ev.Player.ItemInHand.IsCustomItem)
                {
                    if (Plugin.DictTabletteGrenades.ContainsKey(ev.Player.PlayerId))
                    {
                        List<AmorcableGrenade> listGrenade = Plugin.DictTabletteGrenades[ev.Player.PlayerId];
                        foreach (AmorcableGrenade grenade in listGrenade)
                        {
                            try
                            {
                                if (grenade.IsArmed)
                                {
                                    Synapse.Api.Map.Get.SpawnGrenade(grenade.GrItem.Position, Vector3.zero, 0.2f, grenade.IsFlash ? GrenadeType.Flashbang : GrenadeType.Grenade, ev.Player);
                                    grenade.Used = true;
                                    grenade.GrItem.Destroy();
                                }
                            }
                            catch
                            {

                            }
                        }
                        var listGrenadeNotUsed = listGrenade.Where(p => !p.Used);
                        if (listGrenadeNotUsed == null || !listGrenadeNotUsed.Any())
                        {
                            Plugin.DictTabletteGrenades.Remove(ev.Player.PlayerId);
                        }
                        else
                        {
                            Plugin.DictTabletteGrenades[ev.Player.PlayerId] = listGrenadeNotUsed.ToList();
                        }
                    }
                }
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
