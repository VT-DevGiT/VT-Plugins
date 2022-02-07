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
        public int[] VanilaScpID = { (int)RoleType.Scp049,   (int)RoleType.Scp0492, (int)RoleType.Scp079, 
                                     (int)RoleType.Scp096,   (int)RoleType.Scp106,  (int)RoleType.Scp173, 
                                     (int)RoleType.Scp93953, (int)RoleType.Scp93989 };

        public Dictionary<int, int> RoleIDSpawned = new Dictionary<int, int>();
        public Dictionary<int, int> RoleIDTotalSpawned = new Dictionary<int, int>();

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
            RoleIDTotalSpawned.Clear();
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
            RoleIDTotalSpawned.Clear();
            RespawnPlayer.Clear();
            RespawnPlayer.AddRange(ev.Players);
        }

        private void OnClass(PlayerSetClassEventArgs ev)
        {
            if (RespawnPlayer.Contains(ev.Player))
            {
                var possiblesRoles = Plugin.Instance.Config.RespawnClassConfig.Where(r => r.ReplaceRoleID == (int)ev.Role);

                foreach(var possibleRole in possiblesRoles)
                {
                    if ((0 < possibleRole.MaxRequiredPlayersInGame && Server.Get.PlayersAmount > possibleRole.MaxRequiredPlayersInGame) ||
                        (0 < possibleRole.MinRequiredPlayersInGame && Server.Get.PlayersAmount < possibleRole.MinRequiredPlayersInGame) ||
                        (0 < possibleRole.MaxRequiredPlayers && Server.Get.PlayersAmount > possibleRole.MaxRequiredPlayers) ||
                        (0 < possibleRole.MinRequiredPlayers && Server.Get.PlayersAmount < possibleRole.MinRequiredPlayers))
                        continue;

                    if (possibleRole.SpawnChance >= UnityEngine.Random.Range(1, 100) &&
                        (!RoleIDSpawned.ContainsKey(possibleRole.RoleID) || RoleIDSpawned[possibleRole.RoleID] > possibleRole.MaxPerRespawn) &&
                        (!RoleIDTotalSpawned.ContainsKey(possibleRole.RoleID) || RoleIDTotalSpawned[possibleRole.RoleID] > possibleRole.MaxRespawn) &&
                        (possibleRole.MinRequiredPlayers < 0 ||true /*TODO: Faire un teste sur le max player*/) &&
                        (possibleRole.MinRequiredPlayersInGame < 0 || possibleRole.MinRequiredPlayersInGame <= Server.Get.PlayersAmount) &&
                        (possibleRole.MaxRequiredPlayersInGame < 0 || possibleRole.MaxRequiredPlayersInGame >= Server.Get.PlayersAmount))
                    {
                        ev.Player.RoleID = possibleRole.RoleID;
                        
                        if (RoleIDSpawned.ContainsKey(possibleRole.RoleID))
                            RoleIDSpawned.Add(possibleRole.RoleID, 1);
                        else
                            RoleIDSpawned[possibleRole.RoleID]++;

                        if (RoleIDTotalSpawned.ContainsKey(possibleRole.RoleID))
                            RoleIDTotalSpawned.Add(possibleRole.RoleID, 1);
                        else
                            RoleIDTotalSpawned[possibleRole.RoleID]++;
                    }
                }

                RespawnPlayer.Remove(ev.Player);
            }
            // faire un poste traintement ?
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

                    var playersRoles = ev.SpawnPlayers.Where(r => VanilaScpID.Contains(r.Value));

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
