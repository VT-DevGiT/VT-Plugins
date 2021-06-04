using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace VT_Item
{
    public class Config : AbstractConfigSection
    {
        [Description("List of white list IP")]
        public List<string> WhitListIP = new List<string>() { "127.0.0.1" };

        [Description("List of white list Country")]
        public List<string> WhitListCountry = new List<string>();

    }
}
