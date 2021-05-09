using Grenades;
using Synapse.Api.Enum;
using Synapse.Api.Items;

namespace VT_Referance.Event.EventArguments
{
    #region Argument
    public class ChangeIntoFragEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Grenade Grenade { get; internal set; }
        public SynapseItem Item { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }
    public class ExplosionGrenadeEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Grenade Grenade { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }

    public class CollisionGrenadeEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Grenade Grenade { get; internal set; }
        public GrenadeType Type { get; internal set; }
        public bool Allow { get; set; }
    }
    #endregion
}
