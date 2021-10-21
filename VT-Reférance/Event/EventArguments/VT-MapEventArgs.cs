using MapGeneration.Distributors;
using Scp914;
using Synapse.Api;
using Synapse.Api.Items;

namespace VT_Referance.Event.EventArguments
{
    public class WarHeadInteracteEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public bool Allow { get; set; }
    }

    public class GeneratorActivatedEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Generator Generator { get; internal set; }
    }

    public class CassieAnnouncementEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public string Words { get; set; }
        public bool MakeHold { get; set; }
        public bool MakeNoise { get; set; }
        public bool Allow { get; set; }
    }
    
    public class Change914KnobSettingEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public bool Allow { get; set; }
    }
    
    public class ElevatorIneractEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Elevator Elevator { get; internal set; }
        public bool Allow { get; set; }
    }
    
    public class LockerInteractEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Locker Locker { get; internal set; }
        public bool Allow { get; set; }
    }
    
    public class Scp914ActivateEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public bool Allow { get; set; }
    }

    public class Scp914UpgradeItemEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public SynapseItem Item { get; internal set; }
        public Scp914KnobSetting Setting { get; internal set; }
        public SynapseItem NewItem { get; set; }
    }
}
