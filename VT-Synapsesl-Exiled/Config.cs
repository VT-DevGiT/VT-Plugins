using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Synapsesl_Exiled
{
    public class Config : AbstractConfigSection
    {
        [Description("If enabled your Server is marked as VT-Loder Server")]
        public bool NameTracking = true;

        [Description("Disable Warning it deactivates itself after the first start")]
        public bool Warning = true;
    }
}
