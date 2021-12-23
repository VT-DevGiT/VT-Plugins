using Synapse.Api;
using Synapse.Api.Items;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Event
{

    public class VT_PlayerEvents
    {
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerDamagePostEventArgs> PlayerDamagePostEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerDestroyEventArgs> PlayerDestroyEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerSpeakIntercomEventEventArgs> PlayerSpeakIntercomEvent;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerVerifEventArgs> PlayerVerif;
        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<PlayerSetClassEventArgs> PlayerSetClassEvent;
        #region Invoke
        internal void InvokePlayerDamagePostEvent(Player victim, Player killer, ref float damage, ItemType weaponType, SynapseItem weapon)
        {
            var ev = new PlayerDamagePostEventArgs
            {
                Killer = killer,
                Victim = victim,
                Damage = damage,
                Weapon = weapon,
                WeaponType = weaponType
            };
            PlayerDamagePostEvent?.Invoke(ev);

            damage = ev.Damage;
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

        internal void InvokeSetClassEvent(Player player, int oldId, int newId)
        {
            Synapse.Server.Get.Logger.Info(player);
            var ev = new PlayerSetClassEventArgs
            {
                Player = player,
                OldID = oldId,
                NewID = newId,
            };

            PlayerSetClassEvent?.Invoke(ev);
        }

        #endregion
    }
}
