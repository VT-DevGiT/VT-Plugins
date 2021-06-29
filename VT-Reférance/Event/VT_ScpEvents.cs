using Synapse.Api;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{
    public class VT_ScpEvents
    {
        public Scp106Events Scp106 { get; } = new Scp106Events();

        public class Scp106Events
        {
            internal Scp106Events() { }

            public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PortalUseEventArgs> PortalUseEvent;

            #region Invoke106Events

            internal void InvokePortalUseEvent(Player player, ref bool allow)
            {
                var ev = new PortalUseEventArgs
                {
                    Scp106 = player,
                    Allow = allow
                };

                PortalUseEvent?.Invoke(ev);

                allow = ev.Allow;
            }

            #endregion
        }

    }
}