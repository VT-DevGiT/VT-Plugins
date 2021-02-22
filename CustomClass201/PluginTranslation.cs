using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClass
{
    public class PluginTranslation : IPluginTranslation
    {
        public string SpawnMessage = "<color=blue><b>You are now</b></color> <color=red><b>%RoleName%</b></color>";
        public string VentMessage = "you can stay another %Time% seconds in the ventilation";
        public string NoTimeVentMessage = "you are in the ventilation";
        public string PowerCooldown = "you can use this power in %Time% seconds";
    }
}
