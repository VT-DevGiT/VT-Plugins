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
    }
}
