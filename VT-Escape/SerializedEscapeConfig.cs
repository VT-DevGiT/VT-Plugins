using System;

namespace VTEscape
{
    [Serializable]
    public class SerializedEscapeConfig
    {
        public EscapeEnum Escape { get; set; }
        public RoleEnum Role { get; set; }
        public TeamEnum Team { get; set; }
        public bool Handcuffed { get; set; }
        public RoleEnum NewRole { get; set; }
        public bool StartWarHead { get; set; }
        public string EscapeMessage { get; set; }

        public SerializedEscapeConfig(EscapeEnum escape, RoleEnum role, TeamEnum team, bool handcuffed, RoleEnum newRole, bool startWarHead = false, string escapeMessage = null)
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
