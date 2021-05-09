using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_Alpha
{
    public class Config : AbstractConfigSection
    {
        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 20;
    }
}
