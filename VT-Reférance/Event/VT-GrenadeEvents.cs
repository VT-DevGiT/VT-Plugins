using Grenades;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{

    public class VT_GrenadeEvents
    {

        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<ChangeIntoFragEventArgs> ChangeIntoFragEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<ExplosionGrenadeEventArgs> ExplosionGrenadeEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<CollisionGrenadeEventArgs> CollisionGrenadeEvent;

        #region Invoke
        internal void InvokeCollisionGrenadeEvent(Grenade grenade, GrenadeType type, ref bool allow)
        {
            var ev = new CollisionGrenadeEventArgs
            {
                Grenade = grenade,
                Type = type,
                Allow = allow
            };

            CollisionGrenadeEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeExplosionGrenadeEvent(Grenade grenade, GrenadeType type,  ref bool allow)
        {
            var ev = new ExplosionGrenadeEventArgs
            {
                Grenade = grenade,
                Type = type,
                Allow = allow
            };

            ExplosionGrenadeEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokeChangeIntoFragEvent(SynapseItem item, Grenade grenade, GrenadeType type, ref bool allow)
        {
            var ev = new ChangeIntoFragEventArgs
            {
                Item = item,
                Grenade = grenade,
                Type = type,
                Allow = allow
            };

            ChangeIntoFragEvent?.Invoke(ev);

            allow = ev.Allow;
        }
        #endregion
    }
}
