using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTEscape
{
    public class CustomEscapeEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public Player Cuffer { get; internal set; }
        public int CurentRole { get; internal set; }
        public int NewRole { get; internal set; }
        public EscapeType Escape { get; internal set; }
        public bool StartWarHead { get; internal set; }
        public string EscapeMessage { get; internal set; }

    }
}
