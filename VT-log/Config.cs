using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synapse.Config;

namespace VTLog
{
    public class Config : AbstractConfigSection
    {
        [Description("Dir log position")]
        public string LogDir { get; set; } = "\\home\\scpserver\\logs";
    }
}
