using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass
{
    [Serializable]
    public struct SpawnClassConfig
    {
        public int RoleID;

        public int ReplaceRoleID;

        public int SpawnChance;

        public int MaxSpawn;

        public int MinRequiredPlayers;

        public int MaxRequiredPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;
    }

    [Serializable]
    public struct RespawnClassConfig
    {
        public int RoleID;

        public int ReplaceRoleID;

        public int SpawnChance;

        public int MaxPerRespawn;

        public int MaxRespawnPerRound;

        public int MaxAlive;

        public int MinRequiredPlayers;

        public int MaxRequiredPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;
    }

    [Serializable]
    public struct SpawnReplaceScpClassConfig
    {
        public int RoleID;

        public int SpawnChance;

        public int MaxSpawn;

        public int MinRequiredScpPlayers;

        public int MaxRequiredScpPlayers;

        public int MinRequiredPlayersInGame;

        public int MaxRequiredPlayersInGame;
    }
}
