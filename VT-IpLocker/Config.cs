using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace VT_IpLocker
{
    public class Config : AbstractConfigSection
    {
        [Description("List of white list IP")]
        public List<IPAddress> WhitListIP = new List<IPAddress>() { new IPAddress(new byte[]{127,0,0,1}) };

        [Description("List of white list Country")]
        public List<string> WhitListCountry = new List<string>();

    }
}
