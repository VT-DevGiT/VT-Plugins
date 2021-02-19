using Grenades;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;

namespace VTTrowItem
{
    internal class EventHandlers
    {
        // https://github.com/RogerFK/ThrowItemsSL
        // https://github.com/sanyae2439/SanyaPlugin_Exiled
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnrKeyPressEvent;
            Server.Get.Events.Player.PlayerShootEvent += OnShootEvent;
        }

        private void OnShootEvent(PlayerShootEventArgs ev)
        {
            if (ev.TargetPosition != Vector3.zero
                && Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit raycastHit, 1049088))
            {
                if (Plugin.Config.ShootMouve)
                {
                    var pickup = raycastHit.transform.GetComponentInParent<Pickup>();
                    if (pickup != null && pickup.Rb != null)
                    {
                        pickup.Rb.AddExplosionForce(Vector3.Distance(ev.TargetPosition, ev.Player.Position), ev.Player.Position, 500f, 3f, ForceMode.Impulse);
                    }
                }

                if (Plugin.Config.ShootMouve)
                {
                    var grenade = raycastHit.transform.GetComponentInParent<FragGrenade>();
                    if (grenade != null)
                    {
                        grenade.NetworkfuseTime -= grenade.NetworkfuseTime;
                    }
                }
            }
        }

        private void OnrKeyPressEvent(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode == Plugin.Config.key)
            {
                var item = ev.Player?.ItemInHand;
                if (item == null)
                    return;
                item.Drop();
                Timing.RunCoroutine(Method.Throw(item.pickup, (ev.Player.Hub.PlayerCameraReference.forward + Plugin.Config.addLaunchForce).normalized));
            }
        }
    }
}