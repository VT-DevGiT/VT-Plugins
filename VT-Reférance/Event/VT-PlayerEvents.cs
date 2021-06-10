using Synapse.Api;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{

    public class VT_PlayerEvents
    {
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerDamagePostEventArgs> PlayerDamagePostEvent;

        #region Invoke
        internal void InvokePlayerDamagePostEvent(Player victim, Player killer, ref PlayerStats.HitInfo info, ref bool allow)
        {
            var ev = new PlayerDamagePostEventArgs
            {
                HitInfo = info,
                Killer = killer,
                Victim = victim,
                Allow = allow,

            };
            PlayerDamagePostEvent?.Invoke(ev);
            info = ev.HitInfo;
            allow = ev.Allow;
        }

        #endregion
    }
}
