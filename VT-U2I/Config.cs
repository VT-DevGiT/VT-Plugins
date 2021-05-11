using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_U2I
{
    public class Config : AbstractConfigSection
    {
        [Description("The chance that a U2I Squad spawns instead of a MTF")]
        public float SpawnChance = 25f;

        [Description("Role for spawn (this is for RP server)")]
        public string SpawnNeedRank = "";

        [Description("The Cassie message when U2I spawn")]
        public string CassieSpawn = "";

        [Description("The Name of a U2I unit")]
        public string UnitName = "%RandomName%";

        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 11;
    }
}
