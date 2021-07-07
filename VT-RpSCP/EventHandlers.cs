using Assets._Scripts.Dissonance;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTRpSCP
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundCheckEvent += OnCheck;
        }

        private void OnCheck(RoundCheckEventArgs ev)
        {
            int[] NoEndTeam = new int[] { (int)TeamID.SCP, (int)TeamID.AND, (int)TeamID.CDP, (int)TeamID.CHI, (int)TeamID.BerserkSCP, (int)TeamID.NetralSCP, (int)TeamID.RSC, (int)TeamID.SHA, (int)TeamID.VIP };
            if (Server.Get.Players.Where(p => NoEndTeam.Contains(p.TeamID)).Any())
            { 
                ev.EndRound = false;

            }
            else
            {
                ev.Team = RoundSummary.LeadingTeam.FacilityForces;
                ev.EndRound = true;
            }
        }
    }
}