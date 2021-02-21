using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigSCP507 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.Medkit, 35, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 25;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = " SCP507";

        [Description("Minimum and maximum time time before SCP 507 teleports")]
        public int MinTPower = 130;
        public int MaxTPower = 230;

        [Description("The list of rooms where SCP 507 can teleport")]
        public List<SerializedMapPoint> ListRoom = new List<SerializedMapPoint> () { 
            new SerializedMapPoint("Root_*&*Outside Cams", -97.45316f, 1979.313f, -167.1043f),
            new SerializedMapPoint("HCZ_049", -19.8f, 264.1f, 5.8f),
        };
        //Root_*&*Outside Cams

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;
        public bool LoseArtificialHealth = false;
    }
}
