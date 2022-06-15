using CustomPlayerEffects;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;
using PlayerStatsSystem;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Networking;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Enum;
using VT_Api.Core.Items;
using VT_Api.Extension;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 200, BasedItemType = ItemType.GunLogicer, Name = "MiniGun")] // Fix Bug 
    class MiniGunScript : AbstractWeapon
    {
        #region Attributes & Properties
        static Dictionary<Player, DateTime> StartShoot = new Dictionary<Player, DateTime>();
        static Dictionary<Player, DateTime> LastSoot = new Dictionary<Player, DateTime>();

        public override string ScreenName => Plugin.Instance.Translation.ActiveTranslation.NameMiniGun;
        public override ushort MaxAmmos => 0;
        public override AmmoType AmmoType => AmmoType.Ammo762x39;
        public override int DamageAmmont => Plugin.MiniGunConfig.Damage;

        public int Distance { get; set; } = 100;
        public ShootSound Sound { get; set; } = ShootSound.Logicer;
        #endregion

        #region Methods
        public override bool PickUp(Player player)
        {
            Ammo = player.AmmoBox[AmmoType.Ammo762x39];
            return base.PickUp(player);
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
            return base.Change(isNewItem);
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
                ushort ammo = (ushort)(MultiShoot(Holder) / 2);
                Holder.AmmoBox[AmmoType.Ammo762x39] -= ammo == 0 ? (ushort)1 : ammo;
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

        private Ray GetRay()
        {
            return new Ray(Holder.CameraReference.position + Holder.CameraReference.forward, RandomAimcone() * Holder.CameraReference.forward);
        }

        private int MultiShoot(Player player)
        {
            player.PlayerInteract.OnInteract();

            int bullets = Plugin.MiniGunConfig.bullets;
            if (Ammo <= bullets)
                bullets = (int)Math.Floor(Ammo);

            AutomaticFirearm firearm = (AutomaticFirearm)Item.ItemBase;

            for (int i = 0; i < bullets; i++)
            {
                if (i != 0)
                    VT_Api.Core.MapAndRoundManger.Get.PlayShoot(Sound, Holder.Position, (byte)Distance);

                var ray = GetRay();
                
                if (Physics.Raycast(ray, out var hit, Distance, StandardHitregBase.HitregMask))
                    ExecuteShoot(ray, hit, firearm, out _);
            }

            return bullets;
        }

        public bool ExecuteShoot(Ray ray, RaycastHit hit, AutomaticFirearm firearm , out IDestructible destructible)
        {
            Synapse.Api.Logger.Get.Info("1");

            if (hit.collider.TryGetComponent(out destructible))
            {
                Synapse.Api.Logger.Get.Info("2.1");
                float damage = Plugin.MiniGunConfig.Damage;
                Synapse.Api.Logger.Get.Info("2.2");
                if (destructible.Damage(damage, new FirearmDamageHandler(firearm, damage, false), hit.point))
                {
                    Synapse.Api.Logger.Get.Info("3");
                    if (!ReferenceHub.TryGetHubNetID(destructible.NetworkId, out var hub))
                    {
                        Synapse.Api.Logger.Get.Info("4");
                        var player = hub.GetPlayer();
                        Synapse.Api.Logger.Get.Info("5");
                        foreach (var hubPlayer in player.SpectatorManager.ServerCurrentSpectatingPlayers)
                            hubPlayer.GetPlayer()?.Connection.Send(new GunHitMessage(false, damage, ray.origin));

                            Synapse.Api.Logger.Get.Info("6");
                        if (player.ClassManager.IsHuman())
                            new GunHitMessage(hit.point + (ray.origin - hit.point).normalized, ray.direction, true).SendToAuthenticated();
                    }
                    Synapse.Api.Logger.Get.Info("7");
                    Hitmarker.SendHitmarker(Holder.Hub, 1.2f);
                    Synapse.Api.Logger.Get.Info("8");
                    return true;
                }
            }
            else new GunHitMessage(hit.point + (ray.origin - hit.point).normalized, ray.direction, false).SendToAuthenticated();
            Synapse.Api.Logger.Get.Info("9");
            return false;
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
                player.GiveEffect(Effect.Disabled, 1, 0.5f);

                if (!Plugin.MiniGunConfig.ByPasseID.Contains(player.RoleID))
                    player.GiveEffect(Effect.Ensnared, 1, 0.5f);

                if (player.ItemInHand.ID != (int)ItemID.MiniGun)
                    this.enabled = false;
            }
        }
        #endregion
    }
}
