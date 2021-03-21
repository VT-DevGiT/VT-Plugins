using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_HammerDown
{
    public class PluginTranslation : IPluginTranslation
    {
        public string SpawnMessageCadet { get; set; } = "You are a <color=blue>HammerDown</color>\\nYour Goal is it to stop all intruders and kill SCP\\n<b>Press Esc to close</b>";
        public string SpawnMessageLieutenant { get; set; } = "You are a <color=blue>HammerDown</color>\\nYour Goal is it to stop all intruders and kill SCP\\n<b>Press Esc to close</b>";
        public string SpawnMessageCommandant { get; set; } = "You are a <color=blue>HammerDown</color>\\nYour Goal is it to stop all intruders and kill SCP\\n<b>Press Esc to close</b>";
    }
}
