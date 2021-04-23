using HarmonyLib;
using Synapse.Api;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Event
{

    public class PlayerUnCuffTargetEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Cuffer { get; internal set; }
        public Player Target { get; internal set; }
        public SynapseItem Item { get; internal set; }
        public bool Allow { get; internal set; }
    }

    public class PlayerFreeTeammateEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Cuffer { get; internal set; }
        public Player Target { get; internal set; }
        public SynapseItem Item { get; internal set; }
        public bool Allow { get; internal set; }
    }


    public class VT_PlayerEvents
    {

        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerUnCuffTargetEventArgs> PlayerUnCuffTargetEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerUnCuffTargetEventArgs> PlayerFreeTeammateEvent;
        

        internal void InvokePlayerUnCuffTargetEvent(Player target, Player cuffer, SynapseItem handcuff, ref bool allow)
        {
            var ev = new PlayerUnCuffTargetEventArgs
            {
                Cuffer = cuffer,
                Item = handcuff,
                Target = target,
                Allow = allow
            };

            PlayerUnCuffTargetEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokePlayerFreeTeammateEvent(Player target, Player cuffer, SynapseItem handcuff, ref bool allow)
        {
            var ev = new PlayerUnCuffTargetEventArgs
            {
                Cuffer = cuffer,
                Item = handcuff,
                Target = target,
                Allow = allow
            };

            PlayerUnCuffTargetEvent?.Invoke(ev);

            allow = ev.Allow;
        }
    }
}
