using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;

namespace VT_HammerDown
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            List<int> Team = new List<int>() { (int)TeamID.AND, (int)TeamID.CHI, (int)TeamID.SHA };
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox && (Server.Get.Players.Where(p => p.RoleType.Is939() || Team.Contains(p.TeamID) ).Count() > 0) && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
                ev.TeamID = (int)TeamID.CDM;
        }
    }
}