using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VT_Referance.ItemScript
{
    public abstract class BaseWeaponScript : BaseItemScript
    {
        #region Attributes & Properties
        protected abstract uint Ammo { get; }

        protected abstract AmmoType AmmoType { get; }
        #endregion

        #region Constructors & Destructor
        #endregion

        #region Methods
        /// <summary>
        ///  for attached additional events 
        ///  Waring, there are already events that are attached
        /// </summary>
        protected override void Event()
        {
            base.Event();
            Server.Get.Events.Player.PlayerShootEvent += OnShoot;
            Server.Get.Events.Player.PlayerReloadEvent += OnReload;
        }

        private void OnReload(PlayerReloadEventArgs ev)
        {
            if (ev.Item.ID == ID)
                this.Reload(ev);
        }

        /// <summary>
        /// this method is called when the object is Reloaded
        /// </summary>
        /// <param name="ev">The contexte</param>
        protected virtual void Reload(PlayerReloadEventArgs ev)
        {
            uint ammo;
            if (ev.Item.Durabillity < this.Ammo)
            {
                switch (this.AmmoType)
                {
                    case AmmoType.Ammo5:
                        ammo = Math.Min(ev.Player.Ammo9, Ammo);
                        ev.Player.Ammo5 -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo7:
                        ammo = Math.Min(ev.Player.Ammo9, Ammo);
                        ev.Player.Ammo5 -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo9:
                        ammo = Math.Min(ev.Player.Ammo9, Ammo);
                        ev.Player.Ammo9 -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                }
            }
        }

        private void OnShoot(PlayerShootEventArgs ev)
        {
            if (ev.Weapon.ID == ID)
                this.Shoot(ev);
        }

        /// <summary>
        /// this method is called when the object is used to shoot
        /// </summary>
        /// <param name="arg">The contexte</param>
        protected virtual void Shoot(PlayerShootEventArgs ev)
        { }

        #endregion
    }
}
