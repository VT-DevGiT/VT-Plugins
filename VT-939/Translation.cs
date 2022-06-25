using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT939
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = 
            "<size=20><color=#00FFFF>You've spawned as an upgraded version of <color=#FF0000>SCP-939</color>!" +
            "\nYou're faster than humans, your <color=#FF0000>anger</color> will increase after taking damage from them." +
            "\nMore anger means more damage inflicted to humans." +
            "\nAfter <color=#FF0000>hurting</color> someone, you'll get slowed down for <color=#FF0000>{0}</color> seconds</color></size>";

    }
}
