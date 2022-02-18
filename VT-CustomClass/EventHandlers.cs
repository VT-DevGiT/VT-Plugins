using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;

namespace VTCustomClass
{
    public class EventHandlers
    {
        public List<Player> RespawnPlayer = new List<Player>();

        public Dictionary<int, int> RoleIDSpawnedInRound = new Dictionary<int, int>();
        public Dictionary<Player, int> IDRespawnPlayer = new Dictionary<Player, int>();

        public EventHandlers()
        {
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnReSpawn;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;
            Server.Get.Events.Player.PlayerSetClassEvent += OnClass;
            Server.Get.Events.Server.TransmitPlayerDataEvent += OnTransmitPlayerData;
        }

        private void OnRestart()
        {
            IDRespawnPlayer.Clear();
        }

        private void OnTransmitPlayerData(TransmitPlayerDataEventArgs ev)
        {
            if (ev.PlayerToShow == ev.Player)
                return;
            if (ev.PlayerToShow.RoleID == (int)RoleID.SCP966)
            {
                if (ev.Player.RoleID != (int)RoleID.Staff && ev.Player.RoleID != (int)RoleID.Spectator && ev.Player.TeamID != (int)TeamID.SCP)
                    ev.Invisible = true;
                else
                    ev.Invisible = false;
            }
            else if(ev.PlayerToShow.RoleID == (int)RoleID.Staff)
            {
                if (ev.Player.RoleID != (int)RoleID.Staff)
                    ev.Invisible = true;
                else
                    ev.Invisible = false;
            }
        }

        private void OnReSpawn(TeamRespawnEventArgs ev)
        {
            Server.Get.Logger.Info($"OnReSpawn !!!!");
            IDRespawnPlayer.Clear();
            RespawnPlayer = ev.Players ?? new List<Player>();
        }

        private void OnClass(PlayerSetClassEventArgs ev)
        {
            if (RespawnPlayer.Contains(ev.Player))
            {
                if (ev.Player.CustomRole == null)
                {
                    IDRespawnPlayer.Add(ev.Player, (int)ev.Role);
                    ev.Allow = false;
                }

                RespawnPlayer.Remove(ev.Player);
            }

            if (!RespawnPlayer.Any() && IDRespawnPlayer.Any())
            {
                if (Plugin.Instance.Config.SpawnClassConfigs != null && Plugin.Instance.Config.SpawnClassConfigs.Any())
                {
                    foreach (var classToSpawn in Plugin.Instance.Config.RespawnClassConfig)
                    {
                        if ((0 < classToSpawn.MaxRequiredPlayersInGame && Server.Get.PlayersAmount > classToSpawn.MaxRequiredPlayersInGame) ||
                            (0 < classToSpawn.MinRequiredPlayersInGame && Server.Get.PlayersAmount < classToSpawn.MinRequiredPlayersInGame) ||
                            (0 < classToSpawn.MaxRespawnPerRound && classToSpawn.MaxRespawnPerRound >= RoleIDSpawnedInRound[classToSpawn.RoleID]))
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
                            }
                        }
                    }
                }

                foreach (var rolePlayer in IDRespawnPlayer)
                    rolePlayer.Key.RoleID = rolePlayer.Value;

                IDRespawnPlayer.Clear();
            }
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
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
