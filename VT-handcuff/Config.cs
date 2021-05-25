using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;


namespace VThandcuff
{
    public class Config : AbstractConfigSection
    {

        [Description("Can Cuff ally")]
        public bool CuffAlly = true;

        [Description("Can Cuff 049")]
        public bool Cuff049 = true;

        [Description("Cant Cuff UTR")]
        public bool NCuffUTR = true;

        [Description("Can Cuff Tutoral RoleType")]
        public bool CuffTuto = true;

        [Description("like real handcuffs")]
        public bool CuffLock = false;
        public int Angle = 20;
    }
}
