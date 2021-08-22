using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace VT_Item.Config
{
    public class PluginConfig : AbstractConfigSection
    {
        [Description("The ID of item if you have them in your hands you are slow")]
        public List<int> IdOfSlowItem = new List<int>()
        {
            20,
            24,
            16
        };

        [Description("if the item is pulled over it moves")]
        public bool ShootMouve = true;

        [Description("if a grenade and shoot it and it is active it explodes")]
        public bool ShootInstantFuse = false;
    }
}
