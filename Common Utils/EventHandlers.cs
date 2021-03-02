using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawnPlayer;
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
        }


        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            Timing.CallDelayed(1f, () =>
            {
                switch (ev.Player.RoleID)
                {
                    case (int)RoleType.ClassD:
                        if (CommonUtiles.Config.ClassDHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ClassDHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ClassDHealth;
                        }
                        break;
                    case (int)RoleType.Scientist:
                        if (CommonUtiles.Config.ScientistHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ScientistHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ScientistHealth;
                        }
                        break;
                    case (int)RoleType.FacilityGuard:
                        if (CommonUtiles.Config.GuardHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.GuardHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.GuardHealth;
                        }
                        break;
                    case (int)RoleType.NtfCadet:
                        if (CommonUtiles.Config.CadetHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.CadetHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CadetHealth;
                        }
                        break;
                    case (int)RoleType.NtfLieutenant:
                        if (CommonUtiles.Config.LieutenantHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.LieutenantHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.LieutenantHealth;
                        }
                        break;
                    case (int)RoleType.NtfScientist:
                        if (CommonUtiles.Config.NtfSciHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ScientistHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ScientistHealth;
                        }
                        break;
                    case (int)RoleType.NtfCommander:
                        if (CommonUtiles.Config.CommanderHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.CommanderHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CommanderHealth;
                        }
                        break;
                    case (int)RoleType.ChaosInsurgency:
                        if (CommonUtiles.Config.ChaosHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ChaosHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ChaosHealth;
                        }
                        break;
                    case (int)RoleType.Scp049:
                        if (CommonUtiles.Config.Scp049 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp049;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp049;
                        }
                        break;
                    case (int)RoleType.Scp0492:
                        if (CommonUtiles.Config.Scp0492 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp0492;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp0492;
                        }
                        break;
                    case (int)RoleType.Scp096:
                        if (CommonUtiles.Config.Scp096 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp096;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp096;
                        }
                        break;
                    case (int)RoleType.Scp106:
                        if (CommonUtiles.Config.Scp106 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp106;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp106;
                        }
                        break;
                    case (int)RoleType.Scp173:
                        if (CommonUtiles.Config.Scp173 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp173;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp173;
                        }
                        break;
                    case (int)RoleType.Scp93953:
                        if (CommonUtiles.Config.Scp93953 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp93953;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93953;
                        }
                        break;
                    case (int)RoleType.Scp93989:
                        if (CommonUtiles.Config.Scp93989 != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp93989;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93989;
                        }
                        break;
                }
            });
        }

        private void On914Activate(Scp914ActivateEventArgs ev)
        {
            if (!ev.Players.Any())
                return;


            foreach (var player in ev.Players)
            {
                if (CommonUtiles.Config.Rnd914Size)
                {
                    float newScaleX = UnityEngine.Random.Range(CommonUtiles.Config.Max914SizeX, CommonUtiles.Config.Min914SizeX);
                    float newScaleY = UnityEngine.Random.Range(CommonUtiles.Config.Max914SizeY, CommonUtiles.Config.Min914SizeY);
                    float newScaleZ = UnityEngine.Random.Range(CommonUtiles.Config.Max914SizeZ, CommonUtiles.Config.Min914SizeZ);
                    player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
                }
                if (CommonUtiles.Config.list914Effect.Any())
                {
                    var effect = CommonUtiles.Config.list914Effect[UnityEngine.Random.Range(0, CommonUtiles.Config.list914Effect.Count() - 1)];
                    player.GiveEffect(effect);
                }
                if (CommonUtiles.Config.Rnd914Life)
                {
                    float newLif = UnityEngine.Random.Range(CommonUtiles.Config.Max914Life, CommonUtiles.Config.Min914Life);
                    player.Health = newLif;
                }
                if (CommonUtiles.Config.Rnd914ArtificialLife)
                {
                    float newLif = UnityEngine.Random.Range(CommonUtiles.Config.Max914ArtificialLife, CommonUtiles.Config.Min914ArtificialLife);
                    player.ArtificialHealth = newLif;
                }
                if (CommonUtiles.Config.Rnd914ChanceDie != 0)
                {
                    float Rnd = UnityEngine.Random.Range(0, 100);
                    if (Rnd >= CommonUtiles.Config.Rnd914ChanceDie)
                        player.Kill();
                }
            }
        }

        private void OnSpawnPlayer(SpawnPlayersEventArgs ev)
        {

            IEnumerable<Player> playersEntry = ev.SpawnPlayers.Where(x => !x.Key.OverWatch).Select(x => x.Key);
            foreach (var playerKey in playersEntry)
            {
                int currentRole = ev.SpawnPlayers[playerKey];
                switch (currentRole)
                {
                    case (int)RoleType.ClassD:
                        if (CommonUtiles.Config.ClassDInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.ClassDInventory);
                        }
                        break;
                    case (int)RoleType.ChaosInsurgency:
                        if (CommonUtiles.Config.ChaosInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.ChaosInventory);
                        }
                        break;
                    case (int)RoleType.Scientist:
                        if (CommonUtiles.Config.ScientistInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.ScientistInventory);
                        }
                        break;
                    case (int)RoleType.FacilityGuard:
                        if (CommonUtiles.Config.GuardInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.GuardInventory);
                        }
                        break;
                    case (int)RoleType.NtfCadet:
                        if (CommonUtiles.Config.CadetInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.CadetInventory);
                        }
                        break;
                    case (int)RoleType.NtfLieutenant:
                        if (CommonUtiles.Config.LieutenantInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.LieutenantInventory);
                        }
                        break;
                    case (int)RoleType.NtfScientist:
                        if (CommonUtiles.Config.NtfSciInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.NtfSciInventory);
                        }
                        break;
                    case (int)RoleType.NtfCommander:
                        if (CommonUtiles.Config.CommanderInventory.Any())
                        {
                            playerKey.Inventory.Clear();
                            CommonUtiles.Instance.SetItem(playerKey, CommonUtiles.Config.CommanderInventory);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }


    // Test
    // a placée dans Methode
    public static class PlayerExtension
    {
        public static bool IsTargetVisible(this Player player, GameObject obj, float distanceMax, float Rayon)
        {
            var hits = Physics.SphereCastAll(player.CameraReference.transform.position, Rayon, player.CameraReference.transform.forward, distanceMax);
            IEnumerable<GameObject> hitsObject = hits.Select(p => p.transform.gameObject);
            return hitsObject.Any(p => p.transform.position == obj.transform.position);
        }


        public static bool IsTargetVisible(this Player player, GameObject target, float distanceMax)
        {
            float heightOfPlayer = 1.5f;

            Vector3 startVec = player.transform.position;
            startVec.y += heightOfPlayer;
            Vector3 startVecFwd = player.transform.forward;
            startVecFwd.y += heightOfPlayer;

            RaycastHit hit;
            Vector3 rayDirection = target.transform.position - startVec;

            // If the ObjectToSee is close to this object and is in front of it, then return true
            if ((Vector3.Angle(rayDirection, startVecFwd)) < 110 &&
                (Vector3.Distance(startVec, target.transform.position) <= 20f))
            {
                //Debug.Log("close");
                return true;
            }
            if ((Vector3.Angle(rayDirection, startVecFwd)) < 90 &&
                Physics.Raycast(startVec, rayDirection, out hit, 100f))
            { // Detect if player is within the field of view

                if (hit.collider.gameObject == target)
                {
                    //Debug.Log("Can see player");
                    return true;
                }
                else
                {
                    //Debug.Log("Can not see player");
                    return false;
                }
            }
            return false;
        }
    }
}