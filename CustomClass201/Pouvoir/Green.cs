﻿using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace CustomClass.Pouvoir
{
    class Green : NetworkBehaviour
    {
        private Player player;
        private float _timer;
        public bool agressif;
        private Dictionary<Player, float> playerAffected = new Dictionary<Player, float>();
        public List<Vector3> dimanction = new List<Vector3>();
        private void Start()
        {
            player = gameObject.GetPlayer();
        }
        private void Update()
        {
            _timer += Time.deltaTime;
          
            if (_timer > 1)
            {
                var listPlayerPossible = Server.Get.Players.Where(p => p != player && p.TeamID != (int)TeamID.SCP && p.TeamID != (int)TeamID.RIP && Vector3.Distance(p.Position, player.Position) < PluginClass.ConfigSCP166.Distance);
                
                foreach(var target in playerAffected.Keys)
                {
                    if (!listPlayerPossible.Contains(player))
                    {
                        playerAffected[target] = playerAffected[target] - 1;
                        if (playerAffected[target] <= 0)
                            playerAffected.Remove(target);
                    }
                }                
                
                foreach(var target in listPlayerPossible)
                {
                    if (target.IsUTR())
                        target.Hurt(2, DamageTypes.None, player);
                    else if (agressif)
                    {
                        target.Hurt(8, DamageTypes.None, player);
                        target.GiveEffect(Effect.Poisoned, 1, 1.5f);
                    }
                    if (playerAffected.ContainsKey(target))
                        playerAffected[target] = playerAffected[target] + 1;
                    else
                        playerAffected[target] = 0;
                    int chance = UnityEngine.Random.Range(1, 100);
                    if (chance <= playerAffected[target])
                    {
                        AffectPlayerStuff(target);
                        playerAffected.Remove(target);
                    }

                }
            }
            if (_timer > 1)
                _timer = 0f;
        }

        private void AffectPlayerStuff(Player target)
        {
            int chance = UnityEngine.Random.Range(1, 7);
            var listArme = player.Inventory.Items.Where(p => p.ItemCategory == ItemCategory.Weapon).ToList();
            Synapse.Api.Items.SynapseItem Arme = null;
            if (listArme.Any())
            {
                Arme = listArme[UnityEngine.Random.Range(0, listArme.Count() - 1)];
            }
            switch (chance)
            {
                case 1:
                    player.Ammo5 -= 5;
                    break;
                case 2:
                    player.Ammo7 -= 5;
                    break;
                case 3:
                    player.Ammo9 -= 5;
                    break;
                case 4:
                    if (Arme != null)
                        Arme.Barrel = 0;
                    break;
                case 5:
                    if (Arme != null)
                        Arme.Other = 0;
                    break;
                case 6:
                    if (Arme != null)
                        Arme.Sight = 0;
                    break;
                case 7:
                    if (Arme != null)
                        Arme.Durabillity = Arme.Durabillity > 5 ? Arme.Durabillity - 5 : 1;
                    break;
            }
        }
    }
}

