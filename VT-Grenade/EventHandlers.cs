using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VTGrenad
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            //Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent += PlayedDead;
            Server.Get.Events.Player.PlayerDropItemEvent += ItemDropped;
            Server.Get.Events.Player.PlayerPickUpItemEvent += PickingUpItem;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
        }

        private void OnThrowGrenade(PlayerThrowGrenadeEventArgs ev)
        {
            throw new NotImplementedException();
        }

        private void PickingUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Item != null)
            {

                foreach (var key in Plugin.DictTabletteGrenades.Keys)
                {
                    var list = Plugin.DictTabletteGrenades[key];
                    if (list.Any(p => p.GrItem == ev.Item))
                    {
                        Plugin.DictTabletteGrenades[key] = list.Where(p => p.GrItem != ev.Item).ToList();
                    }
                }
            }
            ev.Allow = true;
        }
        private void ItemDropped(PlayerDropItemEventArgs ev)
        {
            //SynapseController.Server.Logger.Info("AdvencedGrenade : -> Run");
            //SynapseController.Server.Logger.Info($"AdvencedGrenade : ItemDropped is {ev.Item.ItemType} ");
            if (ev.Item != null && (ev.Item.ItemType == ItemType.GrenadeFrag || (Plugin.Config.FlashRemot && ev.Item.ItemType == ItemType.GrenadeFlash)) 
                && ev.Player.ItemInHand?.ItemType == ItemType.WeaponManagerTablet)
            {
                //SynapseController.Server.Logger.Info("AdvencedGrenade : ItemDropped is Grenade && Item In Hand is Weapon Manager Tablet");
                if (Plugin.DictTabletteGrenades == null)
                {
                    //SynapseController.Server.Logger.Info("AdvencedGrenade : DictTabletteGrenades  null");
                }
                else if (ev.Item == null)
                {
                    //SynapseController.Server.Logger.Info("AdvencedGrenade : Item  null");
                }
                else
                {
                    List<AmorcableGrenade> listGrenade = Plugin.DictTabletteGrenades.ContainsKey(ev.Player.PlayerId) ? Plugin.DictTabletteGrenades[ev.Player.PlayerId] : new List<AmorcableGrenade>();
                    if (listGrenade == null)
                    {
                        //SynapseController.Server.Logger.Info("AdvencedGrenade : -> List null");
                    }
                    else if (listGrenade != null && !listGrenade.Any(p => p.GrItem == ev.Item))
                    {

                        listGrenade.Add(new AmorcableGrenade(ev.Item));
                        if (!Plugin.DictTabletteGrenades.ContainsKey(ev.Player.PlayerId))
                        {
                            //SynapseController.Server.Logger.Info($"AdvencedGrenade : {ev.Item} add to DictTabletteGrenades");
                            Plugin.DictTabletteGrenades.Add(ev.Player.PlayerId, listGrenade);
                        }
                        else
                        {
                            //SynapseController.Server.Logger.Info($"AdvencedGrenade : {ev.Item} add to DictTabletteGrenades list");
                        }
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
