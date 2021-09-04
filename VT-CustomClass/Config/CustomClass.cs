using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass.Config
{
    public class ConfigCustomClass : AbstractConfigSection
    {
        [Description("Sizes that are too small are bugged due to anti-cheat. But if you kill him, you won't have anything to verify that a player isn't stealing or cheating. if you choose true is becomes your responsibility")]
        public bool killAntiCheatPatch = false;
    }
}
