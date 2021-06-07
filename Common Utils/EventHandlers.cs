using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;
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
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            CommonUtiles.Instance.RespawnAllow = true;
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            ev.Allow = CommonUtiles.Instance.RespawnAllow;
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Spectator && ev.Player.Scale != new Vector3(1, 1, 1))
                ev.Player.Scale = new Vector3(1, 1, 1);
            SerializedPlayerInventory nullSerializedPlayerInventory = new SerializedPlayerInventory();
            Timing.CallDelayed(1f, () =>
            {
                ShieldControler shield;
                if (!ev.Player.gameObject.TryGetComponent(out shield))
                    shield = ev.Player.gameObject.AddComponent<ShieldControler>();

                if (CommonUtiles.Config.BaseShieldMax != -1)
                    shield.MaxShield = CommonUtiles.Config.BaseShieldMax;

                switch (ev.Player.RoleID)
                {
                    case (int)RoleType.ClassD:
                        if (CommonUtiles.Config.ClassDHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ClassDHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ClassDHealth;
                        }
                        if (CommonUtiles.Config.ClassDInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.ClassDInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.ClassDShield != -1)
                        {
                            if (CommonUtiles.Config.ClassDShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.ClassDShield;
                            shield.Shield = CommonUtiles.Config.ClassDShield;
                        }
                        break;
                    case (int)RoleType.Scientist:
                        if (CommonUtiles.Config.ScientistHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ScientistHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ScientistHealth;
                        }
                        if (CommonUtiles.Config.ScientistInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.ScientistInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.ScientistShield != -1)
                        {
                            if (CommonUtiles.Config.ScientistShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.ScientistShield;
                            shield.Shield = CommonUtiles.Config.ScientistShield;
                        }
                        break;
                    case (int)RoleType.FacilityGuard:
                        if (CommonUtiles.Config.GuardHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.GuardHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.GuardHealth;
                        }
                        if (CommonUtiles.Config.GuardInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.GuardInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.GuardShield != -1)
                        {
                            if (CommonUtiles.Config.GuardShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.GuardShield;
                            shield.Shield = CommonUtiles.Config.GuardShield;
                        }
                        break;
                    case (int)RoleType.NtfCadet:
                        if (CommonUtiles.Config.CadetHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.CadetHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CadetHealth;
                        }
                        if (CommonUtiles.Config.CadetInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.CadetInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.CadetShield != -1)
                        {
                            if (CommonUtiles.Config.CadetShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.CadetShield;
                            shield.Shield = CommonUtiles.Config.CadetShield;
                        }
                        break;
                    case (int)RoleType.NtfLieutenant:
                        if (CommonUtiles.Config.LieutenantHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.LieutenantHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.LieutenantHealth;
                        }
                        if (CommonUtiles.Config.LieutenantInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.LieutenantInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.LieutenantShield != -1)
                        {
                            if (CommonUtiles.Config.LieutenantShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.LieutenantShield;
                            shield.Shield = CommonUtiles.Config.LieutenantShield;
                        }
                        break;
                    case (int)RoleType.NtfScientist:
                        if (CommonUtiles.Config.NtfSciHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.NtfSciHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.NtfSciHealth;
                        }
                        if (CommonUtiles.Config.NtfSciInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.NtfSciInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.NtfSciShield != -1)
                        {
                            if (CommonUtiles.Config.NtfSciShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.NtfSciShield;
                            shield.Shield = CommonUtiles.Config.NtfSciShield;
                        }
                        break;
                    case (int)RoleType.NtfCommander:
                        if (CommonUtiles.Config.CommanderHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.CommanderHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.CommanderHealth;
                        }
                        if (CommonUtiles.Config.CommanderInventory.IsDefined())
                        {
                            ev.Player.Inventory.Clear();
                            CommonUtiles.Config.CommanderInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.CommanderShield != -1)
                        {
                            if (CommonUtiles.Config.CommanderShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.CommanderShield;
                            shield.Shield = CommonUtiles.Config.CommanderShield;
                        }
                        break;
                    case (int)RoleType.ChaosInsurgency:
                        if (CommonUtiles.Config.ChaosHealth != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.ChaosHealth;
                            ev.Player.MaxHealth = CommonUtiles.Config.ChaosHealth;
                        }
                        if (CommonUtiles.Config.ChaosInventory.IsDefined())
                        { 
                            ev.Player.Inventory.Clear();
                           CommonUtiles.Config.ChaosInventory.Apply(ev.Player);
                        }
                        if (CommonUtiles.Config.ChaosShield != -1)
                        {
                            if (CommonUtiles.Config.ChaosShield > shield.MaxShield)
                                shield.MaxShield = CommonUtiles.Config.ChaosShield;
                            shield.Shield = CommonUtiles.Config.ChaosShield;
                        }
                        break;
                    case (int)RoleType.Scp049: 
                        if (CommonUtiles.Config.Scp049Health != -1)
                        { 
                            ev.Player.Health = CommonUtiles.Config.Scp049Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp049Health;
                        }
                        break;
                    case (int)RoleType.Scp0492:
                        if (CommonUtiles.Config.Scp0492Health != -1)
                        { 
                            ev.Player.Health = CommonUtiles.Config.Scp0492Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp0492Health;
                        }
                        break;
                    case (int)RoleType.Scp096:
                        if (CommonUtiles.Config.Scp096Health != -1)
                        { 
                            ev.Player.Health = CommonUtiles.Config.Scp096Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp096Health;
                        }
                        break;
                    case (int)RoleType.Scp106:
                        if (CommonUtiles.Config.Scp106Health != -1)
                        { 
                            ev.Player.Health = CommonUtiles.Config.Scp106Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp106Health;
                        }
                        break;
                    case (int)RoleType.Scp173:
                        if (CommonUtiles.Config.Scp173Health != -1)
                        {
                            ev.Player.Health = CommonUtiles.Config.Scp173Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp173Health;
                        }
                        break;
                    case (int)RoleType.Scp93953:
                        if (CommonUtiles.Config.Scp93953Health != -1)
                         { 
                            ev.Player.Health = CommonUtiles.Config.Scp93953Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93953Health;
                        }
                        break;
                    case (int)RoleType.Scp93989:
                        if (CommonUtiles.Config.Scp93989Health != -1)
                        { 
                            ev.Player.Health = CommonUtiles.Config.Scp93989Health;
                            ev.Player.MaxHealth = CommonUtiles.Config.Scp93989Health;
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
                    float newLif = UnityEngine.Random.Range(CommonUtiles.Config.Min914Life, CommonUtiles.Config.Max914Life);
                    player.Health = newLif;
                }
                if (CommonUtiles.Config.Rnd914ArtificialLife)
                {
                    float newLif = UnityEngine.Random.Range(CommonUtiles.Config.Min914ArtificialLife, CommonUtiles.Config.Max914ArtificialLife);
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