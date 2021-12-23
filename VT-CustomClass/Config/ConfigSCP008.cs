using Synapse.Config;
using System.ComponentModel;

namespace VTCustomClass.Config
{
    public class ConfigSCP008 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_012 (12)", 12.0223f, 1.333964f, -0.1024685f);

        [Description("The Amount of Health the class have")]
        public int Health = 750;
        public int MaxHealth = 2200;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 25;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = " SCP-008";

        [Description("the number of HP the class gains when it is next to a human par second")]
        public int HealHp = 10;

        [Description("the number of health the human loses when he is next to the class per second")]
        public int DomageHp = 5;

        [Description("the distance that the class must have with the human")]
        public int Distance = 2;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
    }
}
