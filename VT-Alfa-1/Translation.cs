using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Alpha
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = "You are a %RoleName%\\nYour Goal is kill everone\\n<b>Press Esc to close</b>";    }

}
