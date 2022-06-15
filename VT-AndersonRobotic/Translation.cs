using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_AndersonRobotic
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessageAnderson { get; set; } = "You are a %RoleName% in the <color=yellow>Anderson-Robotic</color> Team\\nYour Goal is it to steal all data in the serveur\\n<b>Press Esc to close</b>";
    
        public string SpawnMessageAsimov { get; set; } = "You are a %RoleName% in the <color=yellow>Anderson-Robotic</color> Team\\nYour Goal is it to steal all data in the serveur\\n<b>Press Esc to close</b>";

    }
}
