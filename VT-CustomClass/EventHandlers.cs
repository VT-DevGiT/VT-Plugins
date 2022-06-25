using MapGeneration;
using PlayerStatsSystem;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Enum;
using VT_Api.Extension;
using VTCustomClass.PlayerScript;
using VTCustomClass.Pouvoir;

namespace VTCustomClass
{
    public class EventHandlers
    {
        public List<Player> RespawnPlayer { get; private set; } = new List<Player>();
        public Dictionary<int, int> RoleIDSpawnedInRound { get; private set; } = new Dictionary<int, int>();
        public Dictionary<Player, int> IDRespawnPlayer { get; private set; } = new Dictionary<Player, int>();

        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnReSpawn;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;
            Server.Get.Events.Map.TriggerTeslaEvent += OnTesla;
            Server.Get.Events.Player.PlayerSetClassEvent += OnClass;
            Server.Get.Events.Server.TransmitPlayerDataEvent += OnTransmitPlayerData;
        }

        public void OnTesla(TriggerTeslaEventArgs ev)
        {
            if (ev.Player.RoleID == (int)RoleID.Staff)
                ev.Trigger = false;
        }

        public void OnRestart()
        {   
            IDRespawnPlayer.Clear();
        }

        public void OnTransmitPlayerData(TransmitPlayerDataEventArgs ev)
        {
            if (ev.PlayerToShow == ev.Player)
                return;
            if (ev.PlayerToShow.RoleID == (int)RoleID.SCP966)
            {
                if (CanSee966())
                    ev.Invisible = false;
                else
                    ev.Invisible = true;
            }
            else if (ev.PlayerToShow.CustomRole is StaffClassScript staff)
            {
                if (!staff.Invisible || ev.Player.RoleID == (int)RoleID.Staff)
                    ev.Invisible = false;
                else
                    ev.Invisible = true;
            }

            bool CanSee966()
            {
                return ev.Player.RoleID == (int)RoleID.Staff || ev.Player.RoleID == (int)RoleID.Spectator || ev.Player.TeamID == (int)TeamID.SCP ||
                    (ev.Player.ItemInHand?.ItemType == ItemType.Flashlight && Vector3.Distance(ev.Player.Position, ev.PlayerToShow.Position) < 5);
            }
        }

        public void OnReSpawn(TeamRespawnEventArgs ev)
        {
            IDRespawnPlayer.Clear();
            RespawnPlayer = ev.Players ?? new List<Player>();
        }

        public void OnClass(PlayerSetClassEventArgs ev)
        {

            if (!RespawnPlayer.Contains(ev.Player))
                return;

            if (ev.Player.CustomRole == null)
            {
                IDRespawnPlayer.Add(ev.Player, (int)ev.Role);
                ev.Allow = false;
            }

            RespawnPlayer.Remove(ev.Player);
            

            if (!RespawnPlayer.Any() && IDRespawnPlayer.Any())
            {
                if (Plugin.Instance.Config.SpawnClassConfigs != null && Plugin.Instance.Config.SpawnClassConfigs.Any())
                {
                    foreach (var classToSpawn in Plugin.Instance.Config.RespawnClassConfig)
                    {
                        if (!RoleIDSpawnedInRound.ContainsKey(classToSpawn.RoleID))
                            RoleIDSpawnedInRound.Add(classToSpawn.RoleID, 0);

                        if ((0 < classToSpawn.MaxRequiredPlayersInGame && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayersInGame) ||
                            (0 < classToSpawn.MinRequiredPlayersInGame && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayersInGame) ||
                            (0 < classToSpawn.MaxPerRound && classToSpawn.MaxPerRound >= RoleIDSpawnedInRound[classToSpawn.RoleID]))
                            continue;

                        var playersRoles = IDRespawnPlayer.Where(r => r.Value == classToSpawn.ReplaceRoleID);

                        for (int i = 0; i < classToSpawn.MaxPerRespawn; i++)
                        {
                            if (!playersRoles.Any() ||
                                (0 < classToSpawn.MaxRequiredPlayers && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayers) ||
                                (0 < classToSpawn.MinRequiredPlayers && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayers))
                                break;

                            if (classToSpawn.SpawnChance >= UnityEngine.Random.Range(1, 100))
                            {
                                var key = playersRoles.ElementAt(UnityEngine.Random.Range(0, playersRoles.Count() - 1)).Key;
                                IDRespawnPlayer[key] = classToSpawn.RoleID;
                                RoleIDSpawnedInRound[classToSpawn.RoleID]++;
                            }
                        }
                    }
                }

                foreach (var rolePlayer in IDRespawnPlayer)
                    rolePlayer.Key.RoleID = rolePlayer.Value;

                IDRespawnPlayer.Clear();
            }
        }

        public void OnSpawn(SpawnPlayersEventArgs ev)
        {
            if (ev.SpawnPlayers == null)
                return;
        
            if (Plugin.Instance.Config.SpawnClassConfigs != null && Plugin.Instance.Config.SpawnClassConfigs.Any())
            {
                foreach (var classToSpawn in Plugin.Instance.Config.SpawnClassConfigs)
                {
                    if ((0 < classToSpawn.MaxRequiredPlayersInGame && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayersInGame) ||
                        (0 < classToSpawn.MinRequiredPlayersInGame && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayersInGame))
                        continue;

                    var playersRoles = ev.SpawnPlayers.Where(r => r.Value == classToSpawn.ReplaceRoleID);
                    
                    for (int i = 0; i < classToSpawn.MaxSpawn; i++)
                    {
                        if (!playersRoles.Any() ||
                            (0 < classToSpawn.MaxRequiredPlayers && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayers) ||
                            (0 < classToSpawn.MinRequiredPlayers && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayers))
                            break;

                        if (classToSpawn.SpawnChance >= UnityEngine.Random.Range(1, 100))
                        {
                            var key = playersRoles.ElementAt(UnityEngine.Random.Range(0, playersRoles.Count() - 1)).Key;
                            ev.SpawnPlayers[key] = classToSpawn.RoleID;
                        }
                    }
                }
            }
            
            if (Plugin.Instance.Config.SpawnReplaceScpClassConfig != null && Plugin.Instance.Config.SpawnReplaceScpClassConfig.Any())
            {
                foreach (var classToSpawn in Plugin.Instance.Config.SpawnReplaceScpClassConfig)
                {
                    if ((0 < classToSpawn.MaxRequiredPlayersInGame && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayersInGame) ||
                        (0 < classToSpawn.MinRequiredPlayersInGame && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayersInGame))
                        continue;

                    var playersRoles = ev.SpawnPlayers.Where(r => VT_Api.Core.Roles.RoleManager.VanilaScpID.Contains(r.Value));

                    for (int i = 0; i < classToSpawn.MaxSpawn; i++)
                    {
                        if (!playersRoles.Any() ||
                            (0 < classToSpawn.MaxRequiredScpPlayers && Server.Get.PlayersAmount > classToSpawn.MaxRequiredScpPlayers) ||
                            (0 < classToSpawn.MinRequiredScpPlayers && Server.Get.PlayersAmount < classToSpawn.MinRequiredScpPlayers))
                            break;

                        if (classToSpawn.SpawnChance >= UnityEngine.Random.Range(1, 100))
                        {
                            var key = playersRoles.ElementAt(UnityEngine.Random.Range(0, playersRoles.Count() - 1)).Key;
                            ev.SpawnPlayers[key] = classToSpawn.RoleID;
                        }
                    }
                }
            }
        }
    }
}
