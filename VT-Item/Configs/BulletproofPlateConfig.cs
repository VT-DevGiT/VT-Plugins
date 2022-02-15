using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synapse.Config;

namespace VT_Item.Configs
{
    public class BulletproofPlateConfig : AbstractConfigSection
    {
        [Description("The Amout of Sheld when a Bulletproof Pate is use")]
        public int AmoutSheld = 35;
    }
}
