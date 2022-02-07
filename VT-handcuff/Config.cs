using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Api.Core.Enum;

namespace VThandcuff
{
    public class Config : AbstractConfigSection
    {

        [Description("Can Cuff ally")]
        public bool CuffAlly = true;

        [Description("Can Cuff role (ID)")]
        public List<int> CuffId = new List<int>()
        {
            (int)RoleID.Scp035,
            (int)RoleID.Scp049,
            (int)RoleID.Tutorial,
            (int)RoleID.SerpentsHand,
        };

        [Description("Cant Cuff UTR")]
        public bool NCuffUTR = true;

        [Description("Can Cuff Tutoral RoleType")]
        public bool CuffTuto = true;

        [Description("like real handcuffs")]
        public bool CuffLock = false;
        public int Angle = 0; //20 is good
    }
}
