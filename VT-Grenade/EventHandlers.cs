using InventorySystem.Items.ThrowableProjectiles;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Events.EventArguments;

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

            RealoadEventConfig();
        }
        
        public void RealoadEventConfig()
        {
            Server.Get.Events.Player.PlayerDropItemEvent -= ItemDropped;
            VtController.Get.Events.Item.ChangeIntoFragEvent -= OnChangeIntoFragEvent;
            VtController.Get.Events.Item.CollisionEvent -= OnCollisionGrenade;

            if (null != Plugin.Instance.Config.Key)
                Server.Get.Events.Player.PlayerDropItemEvent += ItemDropped;

            if (Plugin.Instance.Config.ChaineFuseFragGrenad)
                VtController.Get.Events.Item.ChangeIntoFragEvent += OnChangeIntoFragEvent;

            if (Plugin.Instance.Config.FlashbangFuseWithCollision)
                VtController.Get.Events.Item.CollisionEvent += OnCollisionGrenade;
        }
        
        private void OnCollisionGrenade(CollisionEventArgs ev)
        {
            if (ev.Item.ItemType == ItemType.GrenadeFlash && ev.Item.PickupBase is TimeGrenade grenad)
                grenad._fuseTime = 0.01f;
        }

        private void OnChangeIntoFragEvent(ChangeIntoFragEventArgs ev)
        {
            if (ev.Item != null && ev.Grenade != null)
            { 
                Map.Get.Explode(ev.Item.Position, GrenadeType.Grenade, ev.Grenade.PreviousOwner.Hub.GetPlayer());
                ev.Item.Despawn();
            }
            ev.Allow = false;
        }   

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode == Plugin.Instance.Config.Key)
            {
                ev.Player.ExecuteCommand(".Boom", false);
            }
        }

        private void PickingUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Item != null)
            {
                var keys = Plugin.DictRadioGrenads.Keys.ToList();
                foreach (var key in keys)
                {
                    var list = Plugin.DictRadioGrenads[key];
                    if (list.Any(p => p.GrItem == ev.Item))
                    {
                        Plugin.DictRadioGrenads[key] = list.Where(p => p.GrItem != ev.Item).ToList();
                    }
                }
            }
        }
        private void ItemDropped(PlayerDropItemEventArgs ev)
        {
            if (ev.Item != null && (ev.Item.ItemType == ItemType.GrenadeHE 
                || (Plugin.Instance.Config.FlashRemot && ev.Item.ItemType == ItemType.GrenadeFlash)) 
                && ev.Player?.ItemInHand?.ID == (int)ItemType.Radio 
                && !ev.Player.ItemInHand.IsCustomItem)
            {
                List<AmorcableGrenade> listGrenade = Plugin.DictRadioGrenads.ContainsKey(ev.Player.PlayerId) ? Plugin.DictRadioGrenads[ev.Player.PlayerId] : new List<AmorcableGrenade>();

                if (listGrenade != null && !listGrenade.Any(p => p.GrItem == ev.Item))
                {
                    listGrenade.Add(new AmorcableGrenade(ev.Item));
                    if (!Plugin.DictRadioGrenads.ContainsKey(ev.Player.PlayerId))
                    {
                        Plugin.DictRadioGrenads.Add(ev.Player.PlayerId, listGrenade);
                    }
                }
            }
            ev.Allow = true;
        }
        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (Plugin.DictRadioGrenads.ContainsKey(ev.Target.PlayerId))
            {
                Plugin.DictRadioGrenads.Remove(ev.Target.PlayerId);
            }
        }

        private void PlayedDead(PlayerDeathEventArgs ev)
        {
            if (Plugin.DictRadioGrenads.ContainsKey(ev.Victim.PlayerId))
            {
                Plugin.DictRadioGrenads.Remove(ev.Victim.PlayerId);
            }
        }
    }
}
