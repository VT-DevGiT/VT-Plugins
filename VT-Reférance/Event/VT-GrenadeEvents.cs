using Grenades;
using Synapse.Api.Enum;
using Synapse.Api.Items;

namespace VT_Referance.Event
{
    public class ChangeIntoFragEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public FragGrenade Grenade { get; internal set; }
        public SynapseItem Item { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }
    public class ExplosionGrenadeEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public EffectGrenade Grenade { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }

    public class CollisionGrenadeEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public EffectGrenade Grenade { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }
    public class VT_GrenadeEvents
    {

        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<ChangeIntoFragEventArgs> ChangeIntoFragEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<ExplosionGrenadeEventArgs> ExplosionGrenadeEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<CollisionGrenadeEventArgs> CollisionGrenadeEvent;

        #region Invoke
        internal void InvokeCollisionGrenadeEvent(EffectGrenade grenade, GrenadeType type, ref bool allow)
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

        internal void InvokeExplosionGrenadeEvent(EffectGrenade grenade, GrenadeType type,  ref bool allow)
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

        internal void InvokeChangeIntoFragEvent(SynapseItem item, FragGrenade grenade, GrenadeType type, ref bool allow)
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
