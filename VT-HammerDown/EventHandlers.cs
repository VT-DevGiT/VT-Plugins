using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VT_HammerDown
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox && Scp939PresentOrEnemy() && UnityEngine.Random.Range(1f, 100f) <= Plugin.Instance.Config.SpawnChance)
            {
                ev.TeamID = (int)TeamID.CDM;
                TeamManager.Get.RemoveOrFillWithSpectator(ev.Players, Plugin.Instance.Config.CmdMaxPerRespawn);
            }
        }

        public bool Scp939PresentOrEnemy()
            => Server.Get.Players.Any(p => p.RoleType.Is939() || p.TeamID == (int)TeamID.AND || p.TeamID == (int)TeamID.CHI || p.TeamID == (int)TeamID.SHA);
       
    }
}