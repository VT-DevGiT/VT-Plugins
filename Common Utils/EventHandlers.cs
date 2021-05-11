using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            SerializedPlayerInventory nullSerializedPlayerInventory = new SerializedPlayerInventory();
            Timing.CallDelayed(1f, () =>
            {
                switch (ev.Player.RoleID)
                {
                    case (int)RoleType.ClassD when CommonUtiles.Config.ClassDHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.ClassDHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ClassDHealth;
                        break;
                    case (int)RoleType.Scientist when CommonUtiles.Config.ScientistHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.ScientistHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ScientistHealth;
                        break;
                    case (int)RoleType.FacilityGuard when CommonUtiles.Config.GuardHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.GuardHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.GuardHealth;
                        break;
                    case (int)RoleType.NtfCadet when CommonUtiles.Config.CadetHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.CadetHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CadetHealth;
                        break;
                    case (int)RoleType.NtfLieutenant when CommonUtiles.Config.LieutenantHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.LieutenantHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.LieutenantHealth;
                        break;
                    case (int)RoleType.NtfScientist when CommonUtiles.Config.NtfSciHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.NtfSciHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.NtfSciHealth;
                        break;
                    case (int)RoleType.NtfCommander when CommonUtiles.Config.CommanderHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.CommanderHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CommanderHealth;
                        break;
                    case (int)RoleType.ChaosInsurgency when CommonUtiles.Config.ChaosHealth != -1:
                            ev.Player.Health = CommonUtiles.Config.ChaosHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ChaosHealth;
                        break;
                    case (int)RoleType.Scp049 when CommonUtiles.Config.Scp049 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp049;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp049;
                        break;
                    case (int)RoleType.Scp0492 when CommonUtiles.Config.Scp0492 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp0492;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp0492;
                        break;
                    case (int)RoleType.Scp096 when CommonUtiles.Config.Scp096 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp096;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp096;
                        break;
                    case (int)RoleType.Scp106 when CommonUtiles.Config.Scp106 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp106;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp106;
                        break;
                    case (int)RoleType.Scp173 when CommonUtiles.Config.Scp173 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp173;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp173;
                        break;
                    case (int)RoleType.Scp93953 when CommonUtiles.Config.Scp93953 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp93953;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93953;
                        break;
                    case (int)RoleType.Scp93989 when CommonUtiles.Config.Scp93989 != -1:
                            ev.Player.Health = CommonUtiles.Config.Scp93989;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93989;
                        break;


                    case (int)RoleType.ClassD when CommonUtiles.Config.ClassDInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.ClassDInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.ChaosInsurgency when CommonUtiles.Config.ChaosInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.ChaosInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.Scientist when CommonUtiles.Config.ScientistInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.ScientistInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.FacilityGuard when CommonUtiles.Config.GuardInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.GuardInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.NtfCadet when CommonUtiles.Config.CadetInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.CadetInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.NtfLieutenant when CommonUtiles.Config.LieutenantInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.LieutenantInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.NtfScientist when CommonUtiles.Config.NtfSciInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.NtfSciInventory.Apply(ev.Player);
                        break;
                    case (int)RoleType.NtfCommander when CommonUtiles.Config.CommanderInventory.IsDefined():
                        ev.Player.Inventory.Clear();
                        CommonUtiles.Config.CommanderInventory.Apply(ev.Player);
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
    }
}