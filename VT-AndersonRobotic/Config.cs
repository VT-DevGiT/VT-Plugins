using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VT_AndersonRobotic
{
    public class Config : AbstractConfigSection
    {

        [Description("The chance that a Anderson Robotic Squad spawns instead of a Chaos")]
        public float SpawnChance = 50f;

        [Description("The max of Anderson Spawn")]
        public float SpawnMax = 1;

        [Description("The Spawnpoint where Anderson Robotic spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f);

        [Description("The maximal amount of players that can spawn as Anderson Robotic Unite in one squad")]
        public int SpawnSizeAnderson = 7;

        [Description("The maximal amount of players that can spawn as Asimov Unite in one squad")]
        public int SpawnSizeAsimov = 20;
    }
}