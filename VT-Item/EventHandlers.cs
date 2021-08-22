using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;

namespace VT_Item
{
    internal class EventHandlers
    {
        // https://github.com/sanyae2439/SanyaPlugin_Exiled
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerShootEvent += OnShootEvent;
        }

        private void OnShootEvent(PlayerShootEventArgs ev)
        {
            if (ev.TargetPosition != Vector3.zero
                && Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit raycastHit, 1049088))
            {
                if (Plugin.PluginConfig.ShootMouve)
                {
                    var pickup = raycastHit.transform.GetComponentInParent<ItemPickupBase>();
                    if (pickup != null && pickup.Rb != null)
                    {
                        pickup.Rb.AddExplosionForce(Vector3.Distance(ev.TargetPosition, ev.Player.Position), ev.Player.Position, 500f, 3f, ForceMode.Impulse);
                    }
                }

                if (Plugin.PluginConfig.ShootInstantFuse)
                {
                    var grenade = raycastHit.transform.GetComponentInParent<ExplosionGrenade>();
                    if (grenade != null) grenade._fuseTime -= grenade._fuseTime;
                    
                }
            }
        }
    }
}