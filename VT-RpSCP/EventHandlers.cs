using Assets._Scripts.Dissonance;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Extension;

namespace VTRpSCP
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundCheckEvent += OnCheck;
            Server.Get.Events.Scp.ScpAttackEvent += OnScpAttack;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnScp096Target;
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClass;
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            foreach(var Scp in Server.Get.Players.Where(p => p.Scp096Controller.EnrageTimeLeft > 1))
            {
                if (!Scp.Scp096Controller.Targets.Contains(ev.Victim) && Scp.Scp096Controller.Targets.Count >= 1)
                    Scp.Scp096Controller.EnrageTimeLeft = 0.1f;
            }
        }

        private void OnPlayerSetClass(PlayerSetClassEventArgs ev)
        {
            Timing.CallDelayed(60, () =>
            {
                if (!ev.Player.Is939())
                    ev.Player.GetComponent<DissonanceUserSetup>().DisableSpeaking(TriggerType.Role, Assets._Scripts.Dissonance.RoleType.SCP);
                if (ev.Player.RoleType == RoleType.Scp049)
                    ev.Player.GetComponent<DissonanceUserSetup>().EnableSpeaking(TriggerType.Proximity | TriggerType.Intercom);
            });
        }

        private void OnScp096Target(Scp096AddTargetEventArgument ev)
        {
            if (ev.Scp096.IsCuffed)
                ev.Allow = false;
            else
            {
                ev.Scp096.Scp096Controller.EnrageTimeLeft = 600;
            }
        }

        private void OnScpAttack(ScpAttackEventArgs ev)
        {
            if (ev.Scp.RoleID != (int)RoleType.Scp096 && ev.Scp.IsCuffed)
                ev.Allow = false;
        }

        private void OnCheck(RoundCheckEventArgs ev)
        {
            int[] NoEndTeam = new int[] { (int)TeamID.SCP, (int)TeamID.AND, (int)TeamID.CDP, (int)TeamID.CHI, (int)TeamID.BerserkSCP, (int)TeamID.NetralSCP, (int)TeamID.RSC, (int)TeamID.SHA, (int)TeamID.VIP };
            if (Server.Get.Players.Where(p => NoEndTeam.Contains(p.TeamID)).Any() && Server.Get.Players.Where(p => p.IsDead == true).Any())
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