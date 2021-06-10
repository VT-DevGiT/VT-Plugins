using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Item.Config
{
    public class MiniGunConfig : AbstractConfigSection
    {
        [Description("List of ID of the immunised class for movement with")]
        public List<int> IdClassImu = new List<int>()
        {
            (int)VT_Referance.Variable.RoleID.ChiMastodonte
        };
    }
}
