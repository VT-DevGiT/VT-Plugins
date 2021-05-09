using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Variable;

namespace VT_AndersonRobotic
{
    internal class EventHandlers
    {
        int AndersonSapwn = 0;
        public EventHandlers()
        {
            Server.Get.Events.Round.TeamRespawnEvent += Respawn;
            Server.Get.Events.Round.RoundRestartEvent += RoundResart;
        }

        private void RoundResart()
        {
            AndersonSapwn = 0;
        }

        private void Respawn(TeamRespawnEventArgs ev)
        {
            if (AndersonSapwn > Plugin.Config.SpawnMax && ev.Team == Respawning.SpawnableTeamType.ChaosInsurgency && UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
            {
                ev.TeamID = (int)TeamID.AND;
                AndersonSapwn++;
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