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
        public bool Handcuffed { get; set; }
        public RoleID NewRole { get; set; }
        public bool StartWarHead { get; set; }
        public string EscapeMessage { get; set; }

        public SerializedEscapeConfig(EscapeEnum escape, RoleID role, TeamID team, bool handcuffed, RoleID newRole, bool startWarHead = false, string escapeMessage = null)
        {
            Escape = escape;
            Role = role;
            Team = team;
            Handcuffed = handcuffed;
            NewRole = newRole;
            StartWarHead = startWarHead;
            EscapeMessage = escapeMessage;
        }
        public SerializedEscapeConfig()
        {

        }
    }
}
