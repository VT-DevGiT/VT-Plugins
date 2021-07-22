using Synapse.Api;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{

    public class VT_PlayerEvents
    {
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerDamagePostEventArgs> PlayerDamagePostEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerSetClassEventArgs> PlayerSetClassEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerDestroyEventArgs> PlayerDestroyEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerSpeakIntercomEventEventArgs> PlayerSpeakIntercomEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerVerifEventArgs> PlayerVerif;
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

        internal void InvokeSetClassEvent(Player player, int oldId, ref int newId, ref bool allow)
        {
            Synapse.Server.Get.Logger.Info(player);
            var ev = new PlayerSetClassEventArgs
            {
                Player = player,
                OldID = oldId,
                NewID = newId,
                Allow = allow,
            };

            PlayerSetClassEvent?.Invoke(ev);
            newId = ev.NewID;
            allow = ev.Allow;
        }

        internal void InvokePlayerDestroyEvent(Player player)
        {
            var ev = new PlayerDestroyEventArgs
            {
                Player = player,
            };

            PlayerDestroyEvent?.Invoke(ev);
        }

        internal void InvokePlayerSpeakIntercomEvent(Player player, ref bool allow)
        {
            var ev = new PlayerSpeakIntercomEventEventArgs
            {
                Player = player,
                Allow = allow
            };

            PlayerSpeakIntercomEvent?.Invoke(ev);

            allow = ev.Allow;
        }

        internal void InvokePlayerVerifEvent(Player player, ref bool allow)
        {
            var ev = new PlayerVerifEventArgs
            {
                Player = player,
            };

            PlayerVerif?.Invoke(ev);
        }
        #endregion
    }
}
