using Scp914;
using Synapse.Api;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Words { get; internal set; }
        public bool MakeHold { get; internal set; }
        public bool MakeNoise { get; internal set; }
        public bool Allow { get; set; }
    }
    /*
    public class Change914KnobSettingEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Scp914Knob CurSetting { get; internal set; }
        public Scp914Knob NextSetting { get; set; }
        public bool Allow { get; set; }
    }
    */
    public class ElevatorIneractEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Elevator Elevator { get; internal set; }
        public bool Allow { get; set; }
    }
    /*
    public class LockerInteractEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Locker Locker { get; internal set; }
        public bool Allow { get; set; }
    }
    */
    public class Scp914ActivateEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public bool Allow { get; set; }
    }
}
