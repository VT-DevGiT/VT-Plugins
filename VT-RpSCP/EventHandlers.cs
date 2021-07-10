using Assets._Scripts.Dissonance;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Referance;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTRpSCP
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundCheckEvent += OnCheck;
            Server.Get.Events.Scp.Scp173.Scp173BlinkEvent += OnBlink;
            VTController.Server.Events.Map.ActivatingWarheadPanelEvent += OnWarPanel;
            VTController.Server.Events.Map.LockerInteractEvent += OnLocker;
            VTController.Server.Events.Map.Scp914ActivateEvent += On914Active;
            VTController.Server.Events.Map.Scp914changeSettingEvent += On914Change;
        }

        private void OnBlink(Scp173BlinkEventArgs ev)
        {
            ev.Scp173.GetComponent<Scp173PlayerScript>().AllowMove = false;
        }

        private void On914Change(Change914KnobSettingEventArgs ev)
        {
            Server.Get.Logger.Send($"{ev.Allow}", ConsoleColor.Yellow);
        }

        private void On914Active(VT_Referance.Event.EventArguments.Scp914ActivateEventArgs ev)
        {
            Server.Get.Logger.Send($"{ev.Allow}", ConsoleColor.Yellow);
        }

        private void OnLocker(LockerInteractEventArgs ev)
        {
            Server.Get.Logger.Send($"{ev.Allow}", ConsoleColor.Yellow);
        }

        private void OnWarPanel(WarHeadInteracteEventArgs ev)
        {
            Server.Get.Logger.Send($"{ev.Allow}", ConsoleColor.Yellow);
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