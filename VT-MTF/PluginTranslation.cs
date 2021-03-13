using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_MTF
{
    public class PluginTranslation : IPluginTranslation
    {
        public string SpawnMessageCadet { get; set; } = "<i>You are a <color=blue>HammerDown</color></i>\\n<i>Your Goal is it to stop all intruders and kill SCP</i>\\n<b>Press Esc to close</b>";
        public string SpawnMessageLieutenant { get; set; } = "<i>You are a <color=blue>HammerDown</color></i>\\n<i>Your Goal is it to stop all intruders and kill SCP</i>\\n<b>Press Esc to close</b>";
        public string SpawnMessageCommandant { get; set; } = "<i>You are a <color=blue>HammerDown</color></i>\\n<i>Your Goal is it to stop all intruders and kill SCP</i>\\n<b>Press Esc to close</b>";
    }
}
