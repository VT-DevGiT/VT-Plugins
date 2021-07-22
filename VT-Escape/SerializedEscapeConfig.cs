using MEC;
using Respawning;
using Synapse;
using Synapse.Api;
using System;
using VT_Referance.Variable;

namespace VTEscape
{
    [Serializable]
    public class SerializedEscapeConfig
    {
        public EscapeEnum Escape { get; set; }
        public RoleID Role { get; set; }
        public TeamID Team { get; set; }
        public TeamID CufferTeam { get; set; }
        public RoleID NewRole { get; set; }
        public bool StartWarHead { get; set; }
        public string EscapeMessage { get; set; }
        public SpawnableTeamType TicketsTem { get; set; }
        public int TicketsAmont { get; set; }

        public SerializedEscapeConfig(EscapeEnum escape, RoleID role, TeamID team, TeamID cufferTeam, RoleID newRole, EscapeEnum ticketsTem = EscapeEnum.NONE, int ticketsAmont = 0, bool startWarHead = false, string escapeMessage = null)
        {
            Escape = escape;
            Role = role;
            Team = team;
            CufferTeam = cufferTeam;
            NewRole = newRole;
            StartWarHead = startWarHead;
            EscapeMessage = escapeMessage;
            TicketsTem = (SpawnableTeamType)ticketsTem;
            TicketsAmont = ticketsAmont;
        }

        public void Use(Player player)
        {
            if (StartWarHead == true && !Server.Get.Map.Nuke.Detonated)
                Timing.RunCoroutine(new Method().WarHeadEscape(3));
            if (EscapeMessage != null && !Server.Get.Map.Nuke.Detonated)
                Server.Get.Map.Cassie(EscapeMessage, false);
            if (TicketsTem != SpawnableTeamType.None && TicketsAmont != 0)
                    RespawnTickets.Singleton.GrantTickets(TicketsTem, TicketsAmont);
            Method.ChangeRole(player, (int)NewRole);
        }

        public SerializedEscapeConfig()
        {

        }
    }
}
