using Synapse.Api;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{
    public class VT_MapEvents
    {
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<WarHeadStartEventArgs> WarHeadStartEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<CassieAnnouncementEventArgs> CassieAnnouncementEvent;


        #region Invoke
        internal void InvokeWarHeadStartEvent(Player player, ref bool allow)
        {
            var ev = new WarHeadStartEventArgs
            {
                Player = player,
                Allow = allow
            };

            WarHeadStartEvent?.Invoke(ev);

            allow = ev.Allow;
        }
        internal void InvokeCassieAnnouncementEvent(string worlds, bool makeHold, bool makeNoise, ref bool allow)
        {
            var ev = new CassieAnnouncementEventArgs
            {
                Words = worlds,
                MakeHold = makeHold,
                MakeNoise = makeNoise,
                Allow = allow
            };

            CassieAnnouncementEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        #endregion
    }
}