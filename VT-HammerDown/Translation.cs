using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_HammerDown
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = "You are a %RoleName% in the team of <color=blue>HammerDown</color>\\nYour Goal is it to stop all intruders and kills SCP 939\\n<b>Press Esc to close</b>";
    }
}
