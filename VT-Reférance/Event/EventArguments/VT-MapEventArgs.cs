using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Event.EventArguments
{
    public class WarHeadStartEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public bool Allow { get; set; }
    }

    public class CassieAnnouncementEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public string Words { get; internal set; }
        public bool MakeHold { get; internal set; }
        public bool MakeNoise { get; internal set; }
        public bool Allow { get; set; }
    }
}
