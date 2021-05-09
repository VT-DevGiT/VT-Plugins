using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Alpha
{
    public class PluginTranslation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = "You are a <color=red>%RoleName%</color>\\nYour Goal is it to stop the Alpha WarHead and kill\\n<b>Press Esc to close</b>";    }

}
