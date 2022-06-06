using CustomPlayerEffects;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using PlayerStatsSystem;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Enum;
using VT_Api.Core.Items;
using VT_Api.Extension;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 200, BasedItemType = ItemType.GunLogicer, Name = "MiniGun")]
    class MiniGunScript : AbstractWeapon
    {
        #region Attributes & Properties
        static Dictionary<Player, DateTime> StartShoot = new Dictionary<Player, DateTime>();
        static Dictionary<Player, DateTime> LastSoot = new Dictionary<Player, DateTime>();

        public override string ScreenName => Plugin.Instance.Translation.ActiveTranslation.NameMiniGun;
        public override ushort MaxAmmos => 0;
        public override AmmoType AmmoType => AmmoType.Ammo762x39;
        public override int DamageAmmont => Plugin.MiniGunConfig.Damage;
        #endregion

        #region Methods
        public override bool PickUp(Player player)
        {
            Ammo = player.AmmoBox[AmmoType.Ammo762x39];
            return true;
        }

        public override bool Realod()
        {
            Ammo = Holder.AmmoBox[AmmoType.Ammo762x39];
            return false;
        }

        public override bool Change(bool isNewItem)
        {
            if (!isNewItem)
            {
                if (!Plugin.MiniGunConfig.CanMouveEquip)
                    Holder.GetOrAddComponent<MinGunPlayerScript>().enabled = false;
            }
            else
            {
                if (!Plugin.MiniGunConfig.CanMouveEquip)
                    Holder.GetOrAddComponent<MinGunPlayerScript>().enabled = true;
                Ammo = Holder.AmmoBox[AmmoType.Ammo762x39];
            }
            return true;
        }

        public override bool Shoot(Vector3 targetPosition) => Shoot();

        public override bool Shoot(Vector3 targetPosition, Player target) => Shoot();

        public bool Shoot()
        {
            Ammo = Holder.AmmoBox[AmmoType.Ammo762x39];
            var canShoot = false;
            if (LastSoot.ContainsKey(Holder))
            {
                if ((DateTime.Now - LastSoot[Holder]).TotalSeconds < Plugin.MiniGunConfig.TimeFir)
                {
                    canShoot = true;
                    LastSoot[Holder] = DateTime.Now;
                }
                else
                {
                    LastSoot.Remove(Holder);
                }
            }
            if (!canShoot)
            {
                if (StartShoot.ContainsKey(Holder))
                {
                    if ((DateTime.Now - StartShoot[Holder]).TotalSeconds >= Plugin.MiniGunConfig.TimeFir)
                    {
                        LastSoot[Holder] = DateTime.Now;
                        StartShoot.Remove(Holder);
                        canShoot = true;
                    }
                }
                else
                {
                    StartShoot.Add(Holder, DateTime.Now);
                }
            }

            if (canShoot)
            {
                Holder.GiveEffect(Effect.Ensnared, 1, 2);
                Holder.AmmoBox[AmmoType.Ammo762x39] -= (ushort)(MultiShoot(Holder) / 2);
            }
            return false;
        }

        private Quaternion RandomAimcone()
        {
            return Quaternion.Euler(
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone),
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone),
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone)
                );
        }

        private int MultiShoot(Player player)
        {

            player.PlayerInteract.OnInteract();

            int bullets = Plugin.MiniGunConfig.bullets;
            if (Ammo <= bullets)
                bullets = (int)Math.Floor(Ammo);
            Ray[] rays = new Ray[bullets];
            for (int i = 0; i < rays.Length; i++)
                rays[i] = new Ray(player.CameraReference.position + player.CameraReference.forward, RandomAimcone() * player.CameraReference.forward);

            RaycastHit[] hits = new RaycastHit[bullets];
            bool[] didHit = new bool[hits.Length];
            for (int i = 0; i < hits.Length; i++)
                didHit[i] = Physics.Raycast(rays[i], out hits[i], 100f, (int)LayerID.Hitbox);

            bool hit = false;
            AutomaticFirearm firearm = (AutomaticFirearm)Item.ItemBase;
            for (int i = 0; i < hits.Length; i++)
            {
                if (!didHit[i]) continue;

                HitboxIdentity hitbox = hits[i].collider.GetComponent<HitboxIdentity>();
                if (hitbox != null)
                {
                    var target = hits[i].collider.GetComponentInParent<Player>();
                    if (target == player)
                        continue;

                    if (SynapseExtensions.GetHarmPermission(player, target))
                    {
                        hitbox.Damage(Plugin.MiniGunConfig.Damage, new FirearmDamageHandler(firearm, Plugin.MiniGunConfig.Damage, false), player.Position);

                        Synapse.Server.Get.Map.PlaceBlood(hits[i].point + hits[i].normal * 0.01f);

                        hit = true;
                    }
                    
                    continue;
                }

                IDestructible window = hits[i].collider.GetComponent<IDestructible>();
                if (window != null)
                {
                    window.Damage(Plugin.MiniGunConfig.Damage, new FirearmDamageHandler(firearm, Plugin.MiniGunConfig.Damage, false), player.Position);
                    hit = true;
                    continue;
                }

                ((SingleBulletHitreg)firearm.HitregModule).PlaceBulletholeDecal(new Ray(player.Hub.PlayerCameraReference.position, player.Hub.PlayerCameraReference.forward), hits[i]);
            }

            if (hit) Hitmarker.SendHitmarker(player.Hub, 1.2f);

            return bullets;
        }

        private class MinGunPlayerScript : RepeatingBehaviour
        {
            private Player player;

            MinGunPlayerScript() => base.RefreshTime = 480;

            protected override void Start()
            {
                player = gameObject.GetPlayer();
                base.Start();
            }

            protected override void BehaviourAction()
            {
                PlayerEffect effect1 = player.PlayerEffectsController.GetEffect<Disabled>();
                if (effect1 == null || effect1.Duration < 1 )
                    player.GiveEffect(Effect.Disabled, 1, 0.5f);

                PlayerEffect effect2 = player.PlayerEffectsController.GetEffect<Ensnared>();
                if ((effect2 == null || effect2.Duration < 1) && !Plugin.MiniGunConfig.ByPasseID.Contains(player.RoleID))
                    player.GiveEffect(Effect.Ensnared, 1, 0.5f);

                if (player.ItemInHand.ID != (int)ItemID.MiniGun)
                    this.enabled = false;
            }
        }
        #endregion
    }
}
