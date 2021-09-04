using VT_Referance.Method;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using UnityEngine;

namespace VT_Referance.ItemScript
{
    public abstract class BaseWeaponScript : BaseItemScript, IDamageDealer
    {
        #region Attributes & Properties
        public readonly FirearmEquivalence Firearm;
        public readonly BulletHitregManager BulletHitreg;

        public abstract ushort Ammos { get; }
        public abstract AmmoType AmmoType { get; }
        public abstract int DamageAmmont { get; }

        public IAmmoManagerModule AmmoManagerModule { get => Firearm.AmmoManagerModule; set => Firearm.AmmoManagerModule = value; }
        public IEquipperModule EquipperModule { get => Firearm.EquipperModule; set => Firearm.EquipperModule = value; }
        public IActionModule ActionModule { get => Firearm.ActionModule; set => Firearm.ActionModule = value; }
        public IInspectorModule InspectorModule { get => Firearm.InspectorModule; set => Firearm.InspectorModule = value; }
        public IHitregModule HitregModule { get => Firearm.HitregModule; set => Firearm.HitregModule = value; }
        public IAdsModule AdsModule { get => Firearm.AdsModule; set => Firearm.AdsModule = value; }
        public DamageTypes.DamageType DamageType { get => Firearm.DamageType; set => Firearm.SetProperty<DamageTypes.DamageType>("DamageType", value); }
        public float ArmorPenetration { get => Firearm.ArmorPenetration; set => Firearm.SetProperty<float>("ArmorPenetration", value); }
        public bool UseHitboxMultipliers { get => Firearm.UseHitboxMultipliers; set => Firearm.SetProperty<bool>("UseHitboxMultipliers", value); }
        #endregion

        #region Constructors & Destructor
        public BaseWeaponScript()
        {
            Firearm = new FirearmEquivalence();
            BulletHitreg = new BulletHitregManager(Firearm,  ):
        }
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
        [API]
        protected virtual void Reload(PlayerReloadEventArgs ev)
        {
            ushort ammo;
            if (ev.Item.Durabillity < this.Ammos)
            {
                switch (this.AmmoType)
                {
                    case AmmoType.Ammo556x45:
                        ammo = Math.Min(ev.Player.AmmoBox[AmmoType.Ammo556x45], Ammos);
                        ev.Player.AmmoBox[AmmoType.Ammo556x45] -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo762x39:
                        ammo = Math.Min(ev.Player.AmmoBox[AmmoType.Ammo762x39], Ammos);
                        ev.Player.AmmoBox[AmmoType.Ammo762x39] -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo9x19:
                        ammo = Math.Min(ev.Player.AmmoBox[AmmoType.Ammo9x19], Ammos);
                        ev.Player.AmmoBox[AmmoType.Ammo9x19] -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo12gauge:
                        ammo = Math.Min(ev.Player.AmmoBox[AmmoType.Ammo12gauge], Ammos);
                        ev.Player.AmmoBox[AmmoType.Ammo12gauge] -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                    case AmmoType.Ammo44cal:
                        ammo = Math.Min(ev.Player.AmmoBox[AmmoType.Ammo44cal], Ammos);
                        ev.Player.AmmoBox[AmmoType.Ammo44cal] -= ammo;
                        ev.Item.Durabillity += ammo;
                        break;
                }
            }
        }

        private void OnShoot(PlayerShootEventArgs ev)
        {
            if (ev.Weapon?.ID == ID)
                this.Shoot(ev);
        }

        /// <summary>
        /// this method is called when the object is used to shoot
        /// </summary>
        /// <param name="arg">The contexte</param>
        [API]
        protected virtual void Shoot(PlayerShootEventArgs ev)
        { 
            
        }

        public Firearm GetFiream() => Firearm;
        #endregion

        #region Class
        public class BulletHitregManager : StandardHitregBase
        {
            public BulletHitregManager(Firearm firearm, Player player)
            {
                Firearm = firearm;
                Hub = player.Hub;
            }

            public override FirearmBaseStats BaseStats { get; set; } // ????
            public override Firearm Firearm { get; set; }
            public override ReferenceHub Hub { get; set; }

            public override void ServerPerformShot(Ray ray) => throw new NotImplementedException();
        }

        public class FirearmEquivalence : Firearm
        {
            public override DamageTypes.DamageType DamageType { get; } = DamageTypes.Wall;
            public override ItemType AmmoType { get; }

            public override IAmmoManagerModule AmmoManagerModule { get; set; }
            public override IEquipperModule EquipperModule { get; set; }
            public override IActionModule ActionModule { get; set; } 
            public override IInspectorModule InspectorModule { get; set; }
            public override IHitregModule HitregModule { get; set; }
            public override IAdsModule AdsModule { get; set; }
        }
        #endregion
    }
}
