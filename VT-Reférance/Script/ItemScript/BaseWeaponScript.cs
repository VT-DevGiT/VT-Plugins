using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VT_Referance.Script.ItemScript
{
    public abstract class BaseWeaponScript : BaseItemScript
    {
        #region Attributes & Properties

        public virtual uint AmmosPerShoot { get; } = 1;
        public abstract ushort Ammos { get; }
        public abstract AmmoType AmmoType { get; }
        public abstract int DamageAmmont { get; }

        #endregion

        #region Event
        /// <summary>
        ///  for attached additional events 
        ///  Waring, there are already events that are attached
        /// </summary>
        protected override void Event()
        {
            base.Event();
            Server.Get.Events.Player.PlayerShootEvent += OnShoot;
            Server.Get.Events.Player.PlayerReloadEvent += OnReload;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }
        private void OnReload(PlayerReloadEventArgs ev)
        {
            if (ev.Item.ID == ID)
                this.Reload(ev);
        }

        /// <summary>
        /// Override only if you use special ammunition or a special reload
        /// </summary>
        [Unstable]
        protected virtual void Reload(PlayerReloadEventArgs ev)
        {
            if (ev.Item.Durabillity < Ammos)
            {
                ushort ammo = Math.Min(ev.Player.AmmoBox[AmmoType], Ammos);
                ev.Player.AmmoBox[AmmoType] -= ammo;
                ev.Item.Durabillity += ammo;
            }
        }

        private void OnShoot(PlayerShootEventArgs ev)
        {
            if (ev.Weapon?.ID == ID)
                this.Shoot(ev);
        }

        [API]
        protected virtual void Shoot(PlayerShootEventArgs ev)
        { }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer.ItemInHand?.ID == ID)
                this.Damage(ev);
        }

        /// <summary>
        /// Be careful when you override. 
        /// The damage is already modified specific for this weapon.
        /// </summary>
        /// <param name="arg">The contexte</param>
        [Unstable]
        protected virtual void Damage(PlayerDamageEventArgs ev)
        {
            ev.DamageAmount = DamageAmmont;
        }
        #endregion
    }
}
