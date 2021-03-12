using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigSCP166 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_Plants", -0.0884471f, 2.171398f, -4.554413f);

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.KeycardJanitor, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one) };

        [Description("The Amount of Health the class have")]
        public int Health = 200;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 35;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 3;

        [Description("The name of the class")]
        public string RoleName = " SCP-166";

        [Description("The distance that the class must have with other class for the power")]
        public int Distance = 2;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
    }
}