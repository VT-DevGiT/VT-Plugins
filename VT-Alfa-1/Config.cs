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

        [Description("The chance that a Apha-1 Squad spawns when WarHead Start")]
        public float SpawnChance = 50f;

        [Description("Max Respawn of Alpha-1")]
        public float MaxRepsawn = 1;

        [Description("The Cassie message when Alpha-1 spawn")]
        public string CassieSpawn = "";

        [Description("The Name of a Alpha-1 unit")]
        public string UnitName = "%RandomName%";
    }
}
