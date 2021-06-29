using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.Event.EventArguments;
using VT_Referance;

namespace VT_Alpha
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            VTController.Server.Events.Map.WarHeadStartEvent += OnWarHeadStart;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;
        }

        private void OnRestart() => Plugin.Instance.AphaOne = 0;

        private void OnWarHeadStart(WarHeadInteracteEventArgs ev)
        {
            if (Plugin.Instance.AphaOne > Plugin.Config.MaxRepsawn && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
            { 
                Round.Get.NextRespawn += 50f;
                Server.Get.TeamManager.SpawnTeam((int)TeamID.AL1, Server.Get.Players.Where(p => p.RoleID == (int)RoleID.Spectator).ToList());
                Plugin.Instance.AphaOne++;
            }
        }
    }
}