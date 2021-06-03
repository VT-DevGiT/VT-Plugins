using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.ItemScript;
using VT_Referance.Variable;
using VT_Referance.Behaviour;
using VT_Referance.Method;

namespace VT_Item.Item
{
    class MiniGun : BaseWeaponScript
    {
        static Dictionary<Player, DateTime> StartShoot = new Dictionary<Player, DateTime>();
        static Dictionary<Player, DateTime> LastSoot = new Dictionary<Player, DateTime>();
        protected override uint Ammo => 0;

        protected override AmmoType AmmoType => (AmmoType)-1;

        protected override int ID => (int)ItemID.MiniGun;

        protected override ItemType ItemType => ItemType.GunLogicer;

        protected override string Name => "MiniGun";

        protected override void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            if (ev.Player.TryGetComponent<MinGunPlayerScript>(out var Script))
                Script.enabled = true;
            else
                ev.Player.gameObject.AddComponent<MinGunPlayerScript>();
        }

        protected override void ChangedFromItem(PlayerChangeItemEventArgs ev)
        {
            if (ev.Player.TryGetComponent<MinGunPlayerScript>(out var Script))
                Script.enabled = false;
        }

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            ev.Weapon.Durabillity = ev.Player.Ammo7;
            ev.Allow = false;
            if (LastSoot.ContainsKey(ev.Player))
            {
                if ((DateTime.Now - LastSoot[ev.Player]).TotalSeconds < 3)
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
                    if ((DateTime.Now - StartShoot[ev.Player]).TotalSeconds >= 3)
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
                ev.Player.Ammo7 -= (uint)MultiShoot(ev.Player, ev.Weapon);
                ev.Allow = false;

            }
        }

        private int MultiShoot(Player player, Synapse.Api.Items.SynapseItem Weapon)
        {

            player.PlayerInteract.OnInteract();

            //nombre de balle par tire
            int bullets = 3;
            if (Weapon.Durabillity <= bullets)
                bullets = (int)Weapon.Durabillity;
            var rays = new Ray[bullets];
            for (int i = 0; i < rays.Length; i++)
                rays[i] = new Ray(player.CameraReference.position + player.CameraReference.forward, RandomAimcone() * player.CameraReference.forward);

            var hits = new RaycastHit[bullets];
            var didHit = new bool[hits.Length];
            for (int i = 0; i < hits.Length; i++)
                didHit[i] = Physics.Raycast(rays[i], out hits[i], 500f, 1208246273);

            var component = player.GetComponent<WeaponManager>();
            var confirm = false;
            for (int i = 0; i < hits.Length; i++)
            {
                if (!didHit[i]) continue;

                var hitbox = hits[i].collider.GetComponent<HitboxIdentity>();
                if (hitbox != null)
                {
                    var target = hits[i].collider.GetComponentInParent<Synapse.Api.Player>();

                    if (component.GetShootPermission(target.ClassManager))
                    {
                        int damage;
                        switch (hitbox.id)
                        {
                            case HitBoxType.HEAD: damage = 50; break; // ajoutée une config pour chaque zone
                            case HitBoxType.ARM: damage = 15; break;
                            case HitBoxType.LEG: damage = 15; break;
                            default: damage = 20; break;
                        }
                        if (target.RoleType == RoleType.Scp106)
                            damage /= 10;

                        target.Hurt(damage, DamageTypes.Mp7, player);
                        component.RpcPlaceDecal(true, (sbyte)target.ClassManager.Classes.SafeGet(target.RoleType).bloodType, hits[i].point + hits[i].normal * 0.01f, Quaternion.FromToRotation(Vector3.up, hits[i].normal));
                        confirm = true;
                    }

                    continue;
                }
                var window = hits[i].collider.GetComponent<BreakableWindow>();
                if (window != null)
                {
                    window.ServerDamageWindow(20);
                    confirm = true;
                    continue;
                }
                component.RpcPlaceDecal(false, component.curWeapon, hits[i].point + hits[i].normal * 0.01f, Quaternion.FromToRotation(Vector3.up, hits[i].normal));
            }

            for (int i = 0; i < bullets; i++)
                component.RpcConfirmShot(confirm, component.curWeapon);
            return bullets;

        }

        private Quaternion RandomAimcone()
        {
            return Quaternion.Euler(
                UnityEngine.Random.Range(5, 5),
                UnityEngine.Random.Range(5, 5),
                UnityEngine.Random.Range(5, 5)
                // pour le azard des balles ajoutée une config :)
                );
        }

        private class MinGunPlayerScript : BaseRepeatingBehaviour
        {
            private Player player;

            protected override void Start()
            {
                player = gameObject.GetPlayer();
                base.Start();
            }

            protected override void BehaviourAction()
            {
                if (player.IsUTR() /* Ajoutée config pour les rôle peux affectée */)
                    player.GiveEffect(Effect.Disabled, 1, 2);
                else
                    player.GiveEffect(Effect.Ensnared, 1, 2);

                if (player.ItemInHand.ID != (int)ItemID.MiniGun)
                    this.enabled = false;
            }
        }
    }
}
