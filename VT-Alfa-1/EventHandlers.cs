using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;

namespace VT_Alpha
{
    internal class EventHandlers
    {
        public EventHandlers() => VT_Referance.Event.VT_MapEventsWarheadStartEvent += OnWarHead;

        private void OnWarHead() =>  Server.Get.TeamManager.SpawnTeam((int)TeamID.AL1, Server.Get.Players.Where(p => p.RoleID == (int)RoleID.Spectator).ToList());
    }
}