using Synapse.Config;
using System.ComponentModel;

namespace VT_MultieLoder
{
    public class Config : AbstractConfigSection
    {
        [Description("If enabled your Server is marked as VT-Loder Server")]
        public bool NameTracking = false;

        [Description("Disable Warning it deactivates itself after the first start")]
        public bool Warning = true;

        [Description("Enable support for other plugin loader")]
        public bool EnabledExiled = true;
        public bool EnabledQurre = false;
    }
}
