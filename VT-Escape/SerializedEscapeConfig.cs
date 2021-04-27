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

        public SerializedEscapeConfig(EscapeEnum escape, RoleID role, TeamID team, TeamID cufferTeam, RoleID newRole, bool startWarHead = false, string escapeMessage = null)
        {
            Escape = escape;
            Role = role;
            Team = team;
            CufferTeam = cufferTeam;
            NewRole = newRole;
            StartWarHead = startWarHead;
            EscapeMessage = escapeMessage;
        }
        public SerializedEscapeConfig()
        {

        }
    }
}
