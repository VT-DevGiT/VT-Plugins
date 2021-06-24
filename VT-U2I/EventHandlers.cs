using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Referance.Variable;

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
                && (ev.Players.Where(p => Plugin.Config.SpawnNeedRank.Contains(p.RankName)).Count() > 0 || !Plugin.Config.SpawnNeedRank.Any())
                && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
                ev.TeamID = (int)TeamID.U2I;
        }
    }
}