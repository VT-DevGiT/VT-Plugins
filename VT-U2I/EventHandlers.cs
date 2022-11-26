using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Api.Core.Enum;

namespace VT_U2I
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
        }
        
        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox 
                && (ev.Players.Where(p => Plugin.Instance.Config.SpawnNeedRank.Contains(p.RankName)).Count() > 0 || !Plugin.Instance.Config.SpawnNeedRank.Any())
                && UnityEngine.Random.Range(0, 100) <= Plugin.Instance.Config.SpawnChance)
                ev.TeamID = (int)TeamID.U2I;
        }
    }
}