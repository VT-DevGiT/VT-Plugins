using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTCustomClass.Pouvoir
{
    class Green : BaseRepeatingBehaviour
    {
        private Player player;
        public bool DamagGreen;
        private Dictionary<Player, float> playerAffected = new Dictionary<Player, float>();

        public Green() => this.RefreshTime = 250;

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }
        protected override void BehaviourAction()
        {
            var listPlayerPossible = Server.Get.Players.Where(p => p != player && p.TeamID != (int)TeamID.SCP
                && p.TeamID != (int)TeamID.RIP && Vector3.Distance(p.Position, player.Position) < Plugin.ConfigSCP166Az.Distance);

            foreach (var target in playerAffected.Keys)
            {
                if (!listPlayerPossible.Contains(player))
                {
                    playerAffected[target] = playerAffected[target] - 1;
                    if (playerAffected[target] <= 0)
                        playerAffected.Remove(target);
                }
            }

            foreach (var target in listPlayerPossible)
            {
                if (target.IsUTR())
                    target.Hurt(2, DamageTypes.None, player);
                else if (DamagGreen)
                {
                    target.Hurt(12, DamageTypes.None, player);
                    target.GiveEffect(Effect.Poisoned, 1, 1.5f);
                }
                if (playerAffected.ContainsKey(target))
                    playerAffected[target] = playerAffected[target] + 15;
                else
                    playerAffected[target] = 0;
                int chance = Random.Range(1, 100);
                if (chance <= playerAffected[target])
                {
                    AffectPlayerStuff(target);
                    playerAffected.Remove(target);
                }
            }
        }
        private void AffectPlayerStuff(Player target)
        {
            int chance = Random.Range(1, 7);
            var listArme = target.Inventory.Items.Where(p => p.ItemCategory == ItemCategory.Firearm).ToList();
            Synapse.Api.Items.SynapseItem Arme = null;
            if (listArme.Any())
            {
                Arme = listArme[Random.Range(0, listArme.Count() - 1)];
            }
            switch (chance)
            {
                case 1:
                    target.AmmoBox[AmmoType.Ammo12gauge] -= target.AmmoBox[AmmoType.Ammo12gauge] > 5 ? (ushort)(target.AmmoBox[AmmoType.Ammo12gauge] - 5) : (ushort)0;
                    break;
                case 2:
                    target.AmmoBox[AmmoType.Ammo44cal] -= target.AmmoBox[AmmoType.Ammo44cal] > 5 ? (ushort)(target.AmmoBox[AmmoType.Ammo44cal] - 5) : (ushort)0;
                    break;
                case 3:
                    target.AmmoBox[AmmoType.Ammo556x45] -= target.AmmoBox[AmmoType.Ammo556x45] > 5 ? (ushort)(target.AmmoBox[AmmoType.Ammo556x45] - 5) : (ushort)0;
                    break;
                case 4:
                    target.AmmoBox[AmmoType.Ammo762x39] -= target.AmmoBox[AmmoType.Ammo762x39] > 5 ? (ushort)(target.AmmoBox[AmmoType.Ammo762x39] - 5) : (ushort)0;
                    break;
                case 5:
                    target.AmmoBox[AmmoType.Ammo9x19] -= target.AmmoBox[AmmoType.Ammo9x19] > 5 ? (ushort)(target.AmmoBox[AmmoType.Ammo9x19] - 5) : (ushort)0;
                    break;
                case 6:
                    if (Arme != null)
                        Arme.WeaponAttachments = 0;
                    break;
                case 7:
                    if (Arme != null)
                        Arme.Durabillity = Arme.Durabillity > 5 ? Arme.Durabillity - 5 : 0;
                    break;
            }
        }
    }
}

