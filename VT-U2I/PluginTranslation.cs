using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_U2I
{
    public class PluginTranslation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = "You are a <color=blue>UIU %RoleName%</color>\\nYour Goal is it to stop all intruders and kill SCP\\n<b>Press Esc to close</b>";    }
}
