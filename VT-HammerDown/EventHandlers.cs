using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;
using System.Collections.Generic;

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
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox && (Server.Get.Players.Where(p => p.Is939() || Team.Contains(p.TeamID) ).Count() > 0) && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
                ev.TeamID = (int)TeamID.CDM;
        }
    }
}