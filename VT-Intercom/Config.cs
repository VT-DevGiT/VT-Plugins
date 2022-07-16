using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Api.Core.Enum;

namespace VTIntercom
{
    public class Config : AbstractConfigSection
    {
        [Description("Intercom information")]
        public bool IntercomInformation { get; set; } = true;

        [Description("second for one information cycle")]
        public int IntercomInfomationtime { get; set; } = 30;

        [Description("second for one BlackOut")]
        public int BlackOutTime { get; set; } = 60;

        [Description("The radio disable tesla Gate")]
        public bool TeslaRadio { get; set; } = true;

        [Description("Must have a keycard (intercom level) in your hands to be able to speak")]
        public bool KeycardSpeak { get; set; } = false;
        public List<int> KeycardSpeakIgnorRole { get; set; } = new List<int>()
        {
            (int)RoleID.FoundationUTR,
            (int)RoleID.MTFUTR,
            (int)RoleID.AndersonUTRheavy,
            (int)RoleID.AndersonUTRlight,
        };

        [Description("If enabled the decontamination start after the 3rd generater")]
        public bool Decont { get; set; } = true;
    }
}
