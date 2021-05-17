using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_HammerDown
{
    public class Config : AbstractConfigSection
    {
        [Description("The chance that a HamerDown Squad spawns instead of a MTF")]
        public float SpawnChance = 25f;

        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 7;

        [Description("The Cassie message when HamerDown spawn")]
        public string CassieSpawn = "";

        [Description("The Name of a HamerDown unit")]
        public string UnitName = "%RandomName%";
    }
}
