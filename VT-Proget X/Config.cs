using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Api.Core.Enum;

namespace VTProget_X
{
    public class Config : AbstractConfigSection
    {
        [Description("Intercom information")]
        public bool IntercomInformation = true;

        [Description("second for one information cycle")]
        public int IntercomInfomationtime = 30;

        [Description("second for one BlackOut")]
        public int BlackOutTime = 60;

        [Description("The radio disable tesla Gate")]
        public bool TeslaRadio = true;

        [Description("Must have a keycard (intercom level) in your hands to be able to speak")]
        public bool KeycardSpeak = true;
        public List<int> KeycardSpeakIgnorRole = new List<int>()
        {
            (int)RoleID.FoundationUTR,
            (int)RoleID.MTFUTR,
            (int)RoleID.AndersonUTRheavy,
            (int)RoleID.AndersonUTRlight,
        };
    }
}
