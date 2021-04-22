using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VT_AdminHelp
{
    public class Config : AbstractConfigSection
    {
        [Description("Alert message when a player creates a ticket")]
        public string AlertTicketMessage = "%Ticket% \\n le joueur qui a fait la demande est en jail";

        [Description("Alert message when a player kill many ally")]
        public string AlertKillAllyMessage = "Attention! \\n vous avez tué plus de %MaxKill% alliés en moins de %DeltaTime% minutes";

        [Description("Max FF kill (Time in minute)")]
        public int FFMax = 3;
        public int DeltaTime = 2;

        [Description("what happens when it exceeds the kill ally / time limit")]
        public bool Jail = false;
        public bool AutoReport = true;
        public string AutoReportMessage = "FF repérer, Team %TeamKiller% Team de la dernier Victime %TeamVictime% \\n il à tué plus de %MaxKill% d'une team alliés en moins de %DeltaTime% minutes \\n Un Staff est demandée sur place si possible pour annalisé et confirmée";
        public bool CallStaff = false;
        public string CallStaffMessage = "Le joueur : %Player% à tuée trop d'ally en trop peux de temps";
    }
}
