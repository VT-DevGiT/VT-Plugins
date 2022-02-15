using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Item.Configs
{
    public class Translation : IPluginTranslation
    {
        public string NameBulletproofPlate = "BulletproofPlate";
        public string NameMiniGun = "MiniGun";
        public string MessageGetItem = "You pickup a %Name% (is a custom item)";
        public string MessageHandItem = "You ave in you hand a %Name% (is a custom item)";
    }
}
