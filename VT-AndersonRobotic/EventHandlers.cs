using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;

namespace VT_AndersonRobotic
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += Respawn;
            Server.Get.Events.Round.RoundRestartEvent += RoundResart;
        }

        int andersonSapwn = 0;

        private void RoundResart() => andersonSapwn = 0;

        private void Respawn(TeamRespawnEventArgs ev)
        {
            if (andersonSapwn > Plugin.Instance.Config.SpawnMax && ev.Team == Respawning.SpawnableTeamType.ChaosInsurgency && 
                UnityEngine.Random.Range(1f, 100f) <= Plugin.Instance.Config.SpawnChance)
            {
                ev.TeamID = (int)TeamID.AND;
                andersonSapwn++;
                Timing.CallDelayed(35f, () =>
                {
                    List<Player> spawnPlayer = new List<Player>();
                    spawnPlayer.AddRange(Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Spectator && !p.OverWatch));
                    Server.Get.TeamManager.SpawnTeam((int)TeamID.ASI, spawnPlayer);
                    Round.Get.NextRespawn += 50f;
                });
            }
        }
    }
}