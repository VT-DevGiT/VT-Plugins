using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Teams;

namespace VT_Alpha
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            VtController.Get.Events.Map.WarHeadStartEvent += OnWarHeadStart;
            Server.Get.Events.Round.RoundRestartEvent += RoundResart;
        }

        internal int AphaOne = 0;

        private void RoundResart() => AphaOne = 0;

        private void OnWarHeadStart(WarHeadInteracteEventArgs ev)
        {
            if ((AphaOne == -1 || AphaOne > Plugin.Instance.Config.MaxRepsawn) && UnityEngine.Random.Range(1f, 100f) <= Plugin.Instance.Config.SpawnChance)
            { 
                Round.Get.NextRespawn += 120f;
                
                var players = new List<Player>();
                var amount = UnityEngine.Random.Range(Plugin.Instance.Config.MinPlayer, Plugin.Instance.Config.MaxPlayer);
                
                TeamManager.Get.RemoveOrFillWithSpectator(players, amount);
                Server.Get.TeamManager.SpawnTeam((int)TeamID.AL1, players);
                
                AphaOne++;
            }
        }
    }
}