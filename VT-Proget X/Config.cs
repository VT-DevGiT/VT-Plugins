using Synapse.Config;
using System.ComponentModel;

namespace VTProget_X
{
    public class Config : AbstractConfigSection
    {
        [Description("Intercom information")]
        public bool IntercomInformation = true;

        [Description("time for one information cycle")]
        public int IntercomInfomationtime = 30;

        [Description("time for one BlackOut")]
        public int BlackOutTime = 60;

        [Description("The tablets disable tesla Gate")]
        public bool TeslaTablets = true;

        [Description("Must have a keycard (intercom level) in your hands to be able to speak")]
        public bool KeycardSpeak = true;
    }
}
