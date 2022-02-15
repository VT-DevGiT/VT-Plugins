using MEC;
using Respawning;
using Synapse;
using Synapse.Api;
using System;
using VT_Api.Core.Enum;

namespace VTEscape
{
    [Serializable]
    public class SerializedEscapeConfig
    {
        public EscapeType Escape { get; set; }
        public int RoleID { get; set; }
        public int TeamID { get; set; }
        public int CufferTeamID { get; set; }
        public int NewRoleID { get; set; }
        public bool StartWarHead { get; set; }
        public string EscapeMessage { get; set; }
        public SpawnableTeamType TicketsTem { get; set; }
        public int TicketsAmont { get; set; }

        public SerializedEscapeConfig(EscapeType escape, int roleID, int teamID, int cufferTeamID, int newRoleID, EscapeType ticketsTem = EscapeType.NONE, int ticketsAmont = 0, bool startWarHead = false, string escapeMessage = null)
        {
            Escape = escape;
            RoleID = roleID;
            TeamID = teamID;
            CufferTeamID = cufferTeamID;
            NewRoleID = newRoleID;
            StartWarHead = startWarHead;
            EscapeMessage = escapeMessage;
            TicketsTem = (SpawnableTeamType)ticketsTem;
            TicketsAmont = ticketsAmont;
        }

        public void Use(Player player)
        {
            if (StartWarHead == true && !Server.Get.Map.Nuke.Detonated)
                Timing.RunCoroutine(Method.WarHeadEscape(3));
            if (EscapeMessage != null && !Server.Get.Map.Nuke.Detonated)
                Server.Get.Map.Cassie(EscapeMessage, false);
            if (TicketsTem != SpawnableTeamType.None && TicketsAmont != 0)
                    RespawnTickets.Singleton.GrantTickets(TicketsTem, TicketsAmont);
            Method.ChangeRole(player, (int)NewRoleID);
        }

        public SerializedEscapeConfig()
        {

        }
    }
}
