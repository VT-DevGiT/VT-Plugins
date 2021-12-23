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
/*            Server.Get.Logger.Info($"{ev.TargetPosition != Vector3.zero}");
            Server.Get.Logger.Info($"{Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit test)}");
            if (ev.TargetPosition != Vector3.zero
                && Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit raycastHit))*/
            if (ev.Player.LookingAt != null)
            {
                if (Plugin.PluginConfig.ShootMouve)
                {
                    var pickup = ev.Player.LookingAt.GetComponentInParent<ItemPickupBase>();
                    if (pickup != null && pickup.Rb != null)
                    {
                        pickup.Rb.AddExplosionForce(Vector3.Distance(ev.TargetPosition, ev.Player.Position) * 0.01f, ev.Player.Position, 500f, 3f, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}