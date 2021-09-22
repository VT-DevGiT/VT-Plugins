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
using VT_Referance.Variable;
using Synapse.Api.Items;

namespace VT_Referance.ItemScript
{
    public abstract class BaseWeaponScript : BaseItemScript//, IDamageDealer
    {
        #region Attributes & Properties
        
        public readonly FirearmEquivalence Firearm;
        public readonly BuckshotHitreg FuckshotHitreg;
        public readonly SingleBulletHitreg SingleBulletHitreg;
        public Player Handler;
        public SynapseItem BaseItem;
        

        public abstract ushort Ammos { get; }
        public abstract AmmoType AmmoType { get; }
        public abstract int DamageAmmont { get; }

        
        public IAmmoManagerModule AmmoManagerModule { get => Firearm.AmmoManagerModule; set => Firearm.AmmoManagerModule = value; }
        public IEquipperModule EquipperModule { get => Firearm.EquipperModule; set => Firearm.EquipperModule = value; }
        public IActionModule ActionModule { get => Firearm.ActionModule; set => Firearm.ActionModule = value; }
        public IInspectorModule InspectorModule { get => Firearm.InspectorModule; set => Firearm.InspectorModule = value; }
        public IHitregModule HitregModule { get => Firearm.HitregModule; set => Firearm.HitregModule = value; }
        public IAdsModule AdsModule { get => Firearm.AdsModule; set => Firearm.AdsModule = value; }
        public DamageTypes.DamageType DamageType { get => Firearm.DamageType; set => Firearm.SetProperty<DamageTypes.DamageType>(nameof(DamageType), value); }
        public float ArmorPenetration { get => Firearm.ArmorPenetration; set => Firearm.SetProperty<float>(nameof(ArmorPenetration), value); }
        //public bool UseHumanHitboxMultipliers { get => Firearm.UseHumanHitboxMultipliers; set => Firearm.SetProperty<bool>(nameof(Firearm.UseHumanHitboxMultipliers), value); }
        //public bool UseScpHitboxMultipliers { get => Firearm.UseScpHitboxMultipliers; set => Firearm.SetProperty<bool>(nameof(Firearm.UseScpHitboxMultipliers), value); }

        #endregion

        #region Constructors & Destructor

        public BaseWeaponScript()
        {
            Firearm = new FirearmEquivalence();
            //FuckshotHitreg = new BuckshotHitreg(Firearm, null, new BuckshotHitreg.BuckshotSettings());
            //SingleBulletHitreg = new SingleBulletHitreg(Firearm, null); //,Firearm.stats);
        }
        
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
        }

        private void OnReload(PlayerReloadEventArgs ev)
        {
            if (ev.Item.ID == ID)
                this.Reload(ev);
        }

        /// <summary>
        /// Override only if you use special ammunition or a special reload
        /// </summary>
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
        /// Override only if you use shoot
        /// The basic shoot is a single shoot (not multiple)
        /// </summary>
        /// <param name="arg">The contexte</param>
        [API]
        protected virtual void Shoot(PlayerShootEventArgs ev)
        {
            
        }
        #endregion

        #region Methods
        //public Firearm GetFiream() => Firearm;

        public void ProcesShoot(Player player)
        {
            bool hit = false;
            Ray rays = new Ray();
            rays = new Ray(player.CameraReference.position + player.CameraReference.forward, /*RandomAimcone() **/ player.CameraReference.forward);

            if (Physics.Raycast(rays, out RaycastHit hitPlayer, 100f, (int)LayerID.Player) && hitPlayer.collider.TryGetComponent(out HitboxIdentity hitbox))
            {
                var target = hitPlayer.collider.GetComponentInParent<Player>();
                if (target == player) return;

                if (SynapseExtensions.GetHarmPermission(player, target))
                {
                    //hitbox.Damage(DamageAmmont, this, new Footprinting.Footprint(player.Hub), player.Position);

                    Server.Get.Map.PlaceBlood(hitPlayer.point + hitPlayer.normal * 0.01f);

                    hit = true;
                }
            }

            if (Physics.Raycast(rays, 100f, (int)LayerID.Glass) && hitPlayer.collider.TryGetComponent(out IDestructible window))
            {
                //window.Damage(DamageAmmont, this, new Footprinting.Footprint(player.Hub), player.Position);
                hit = true;
            }

            //this.BuckshotHitreg.PlaceBullethole(new Ray(player.Hub.PlayerCameraReference.position, player.Hub.PlayerCameraReference.forward), hitPlayer);

            //if (hit) Hitmarker.SendHitmarker(player.Hub, 1);
        }
        #endregion

        #region Class
        
        public class FirearmEquivalence : Firearm
        {
            //public FirearmBaseStats stats = new FirearmBaseStats();
             
            public override DamageTypes.DamageType DamageType { get; }
            public override ItemType AmmoType { get; }

            public override IAmmoManagerModule AmmoManagerModule { get; set; }
            public override IEquipperModule EquipperModule { get; set; } 
            public override IActionModule ActionModule { get; set; } 
            public override IInspectorModule InspectorModule { get; set; }
            public override IHitregModule HitregModule { get; set; }
            public override IAdsModule AdsModule { get; set; }

            //public override FirearmBaseStats BaseStats => throw new NotImplementedException();
        }   
        #endregion
    }
}
