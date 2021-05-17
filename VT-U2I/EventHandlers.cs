using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;

namespace VT_U2I
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox 
                && (ev.Players.Where(p => p.RankName == Plugin.Config.SpawnNeedRank).Count() > 0 || string.IsNullOrWhiteSpace(Plugin.Config.SpawnNeedRank))
                && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
                ev.TeamID = (int)TeamID.U2I;
        }
    }
}