using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessage = "<color=blue><b>You are now</b></color> <color=red><b>%RoleName%</b></color>\n<b>Press Esc to close</b>";
        public string VentMessage = "you can stay another %Time% seconds in the ventilation";
        public string NoTimeVentMessage = "you are in the ventilation";
        public string PowerCooldown = "you can use this power in %Time% seconds";
        public string KilledMessage = "<color=blue><b>You are killed by</b></color> <color=red><b>%RoleName%</b></color>";
    }
}
