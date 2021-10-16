using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.ItemScript;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.Behaviour;
using CustomPlayerEffects;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;

namespace VT_Item.Item
{
    [ItemInformation(ID = 200, ItemType = ItemType.GunRevolver, Name = "MiniGun")]
    class MiniGunScript : BaseWeaponScript
    {
        #region Attributes & Properties
        static Dictionary<Player, DateTime> StartShoot = new Dictionary<Player, DateTime>();
        static Dictionary<Player, DateTime> LastSoot = new Dictionary<Player, DateTime>();

        public override string ScrenName => Plugin.PluginTranslation.ActiveTranslation.NameMiniGun;
        public override ushort Ammos => 0;
        public override AmmoType AmmoType => AmmoType-1;
        public override int DamageAmmont => Plugin.MiniGunConfig.Damage;
        #endregion

        #region Methods
        protected override void PickUp(PlayerPickUpItemEventArgs ev)
        {
            ev.Item.Durabillity = ev.Player.AmmoBox[AmmoType.Ammo762x39];
            base.PickUp(ev);
        }

        protected override void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            if (!Plugin.MiniGunConfig.CanMouveEquip && ev.Player.TryGetComponent<MinGunPlayerScript>(out var Script))
                Script.enabled = true;
            else if (!Plugin.MiniGunConfig.CanMouveEquip)
                ev.Player.gameObject.AddComponent<MinGunPlayerScript>();
            ev.NewItem.Durabillity = ev.Player.AmmoBox[AmmoType.Ammo762x39];
            base.ChangeToItem(ev);
        }

        protected override void ChangedFromItem(PlayerChangeItemEventArgs ev)
        {
            if (!Plugin.MiniGunConfig.CanMouveEquip && ev.Player.TryGetComponent<MinGunPlayerScript>(out var Script))
                Script.enabled = false;
            base.ChangedFromItem(ev);
        }

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            ev.Weapon.Durabillity = ev.Player.AmmoBox[AmmoType.Ammo762x39];
            ev.Allow = false;
            if (LastSoot.ContainsKey(ev.Player))
            {
                if ((DateTime.Now - LastSoot[ev.Player]).TotalSeconds < Plugin.MiniGunConfig.TimeFir)
                {
                    ev.Allow = true;
                    LastSoot[ev.Player] = DateTime.Now;
                }
                else
                {
                    LastSoot.Remove(ev.Player);
                }
            }
            if (!ev.Allow)
            {
                if (StartShoot.ContainsKey(ev.Player))
                {
                    if ((DateTime.Now - StartShoot[ev.Player]).TotalSeconds >= Plugin.MiniGunConfig.TimeFir)
                    {
                        LastSoot[ev.Player] = DateTime.Now;
                        StartShoot.Remove(ev.Player);
                        ev.Allow = true;
                    }
                }
                else
                {
                    StartShoot[ev.Player] = DateTime.Now;
                }
            }

            if (ev.Allow)
            {
                ev.Player.GiveEffect(Effect.Ensnared, 1, 2);
                ev.Player.AmmoBox[AmmoType.Ammo762x39] -= (ushort)MultiShoot(ev.Player, ev.Weapon);
                ev.Allow = false;
            }
        }

        private Quaternion RandomAimcone()
        {
            return Quaternion.Euler(
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone),
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone),
                UnityEngine.Random.Range(-Plugin.MiniGunConfig.AimCone, Plugin.MiniGunConfig.AimCone)
                );
        }

        private int MultiShoot(Player player, Synapse.Api.Items.SynapseItem Weapon)
        {

            player.PlayerInteract.CallMethod("OnInteract");

            int bullets = Plugin.MiniGunConfig.bullets;
            if (Weapon.Durabillity <= bullets)
                bullets = (int)Weapon.Durabillity;
            Ray[] rays = new Ray[bullets];
            for (int i = 0; i < rays.Length; i++)
                rays[i] = new Ray(player.CameraReference.position + player.CameraReference.forward, RandomAimcone() * player.CameraReference.forward);

            RaycastHit[] hits = new RaycastHit[bullets];
            bool[] didHit = new bool[hits.Length];
            for (int i = 0; i < hits.Length; i++)
                didHit[i] = Physics.Raycast(rays[i], out hits[i], 100f, (int)LayerID.Hitbox);

            bool hit = false;
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
                        hitbox.Damage(Plugin.MiniGunConfig.Damage, (IDamageDealer)player.ItemInHand.ItemBase, new Footprinting.Footprint(Weapon.ItemHolder.Hub), Weapon.ItemHolder.Position);

                        Synapse.Server.Get.Map.PlaceBlood(hits[i].point + hits[i].normal * 0.01f);

                        hit = true;
                    }
                    
                    continue;
                }

                IDestructible window = hits[i].collider.GetComponent<IDestructible>();
                if (window != null)
                {
                    window.Damage(Plugin.MiniGunConfig.Damage, (IDamageDealer)player.ItemInHand.ItemBase, new Footprinting.Footprint(Weapon.ItemHolder.Hub), Weapon.ItemHolder.Position);
                    hit = true;
                    continue;
                }

                ((SingleBulletHitreg)((AutomaticFirearm)player.ItemInHand.ItemBase).HitregModule).PlaceBullethole(new Ray(player.Hub.PlayerCameraReference.position, player.Hub.PlayerCameraReference.forward), hits[i]);
            }

            if (hit) Hitmarker.SendHitmarker(player.Hub, 1.2f);

            return bullets;
        }

        private class MinGunPlayerScript : BaseRepeatingBehaviour
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
