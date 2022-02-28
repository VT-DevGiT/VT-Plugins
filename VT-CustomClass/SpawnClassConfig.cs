using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass
{
    public interface IClassConfig
    {
        int GetRoleID();

        int GetSpawnChance();

        int GetMaxSpawn();

        int GetMinRequiredPlayers();

        int GetMaxRequiredPlayers();

        int GetMinRequiredPlayersInGame();

        int GetMaxRequiredPlayersInGame();
    }



    [Serializable]
    public struct SpawnClassConfig : IClassConfig
    {
        public int RoleID;

        public int ReplaceRoleID;

        public int SpawnChance;

        public int MaxSpawn;

        public int MinRequiredPlayers;

        public int MaxRequiredPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;

        public int GetMaxRequiredPlayers() => MaxRequiredPlayers;

        public int GetMaxRequiredPlayersInGame() => MaxRequiredPlayersInGame;

        public int GetMaxSpawn() => MaxSpawn;

        public int GetMinRequiredPlayers() => MinRequiredPlayers;

        public int GetMinRequiredPlayersInGame() => MinRequiredPlayersInGame;

        public int GetRoleID() => RoleID;

        public int GetSpawnChance() => SpawnChance;
    }

    [Serializable]
    public struct RespawnClassConfig : IClassConfig
    {
        public int RoleID;

        public int ReplaceRoleID;

        public int SpawnChance;

        public int MaxPerRespawn;

        public int MaxPerRound;

        public int MaxAlive;

        public int MinRequiredPlayers;

        public int MaxRequiredPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;

        public int GetMaxRequiredPlayers() => MaxRequiredPlayers;

        public int GetMaxRequiredPlayersInGame() => MaxRequiredPlayersInGame;

        public int GetMaxSpawn() => MaxPerRespawn;

        public int GetMinRequiredPlayers() => MinRequiredPlayers;

        public int GetMinRequiredPlayersInGame() => MinRequiredPlayersInGame;

        public int GetRoleID() => RoleID;

        public int GetSpawnChance() => SpawnChance;
    }

    [Serializable]
    public struct SpawnReplaceScpClassConfig : IClassConfig
    {
        public int RoleID;

        public int SpawnChance;

        public int MaxSpawn;

        public int MinRequiredScpPlayers;

        public int MaxRequiredScpPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;

        public int GetMaxRequiredPlayers() => MaxRequiredScpPlayers;

        public int GetMaxRequiredPlayersInGame() => MaxRequiredPlayersInGame;

        public int GetMaxSpawn() => MaxSpawn;

        public int GetMinRequiredPlayers() => MinRequiredScpPlayers;

        public int GetMinRequiredPlayersInGame() => MinRequiredPlayersInGame;

        public int GetRoleID() => RoleID;

        public int GetSpawnChance() => SpawnChance;
    }
}
