using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigSCP507 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_914 (14)", -7.898621f, 1.329997f, -0.6168289f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.KeycardZoneManager, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.GunCOM15, 15, 1, 0, 0, Vector3.one)
        };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 25;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = " SCP507";

        [Description("Time for Random teleports")]
        public float PowerTime = 120;

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 30;

        [Description("The list of rooms where SCP 507 can teleport")]
        public List<SerializedMapPoint> ListRoom = new List<SerializedMapPoint>() {
            new SerializedMapPoint("Root_*&*Outside Cams", 174.0778f, -21.96997f, 95.57212f),//ok
            new SerializedMapPoint("HCZ_049", -3.892807f, 265.3323f, -1.798096f),//ok
            new SerializedMapPoint("HCZ_049", -13f, 266f, 0f),//ok
            new SerializedMapPoint("HCZ_EZ_Checkpoint", -1.803504f, 1.330017f, -9.199753f),//ok 
            new SerializedMapPoint("HCZ_106", 8f, 6f, -15f),// ok
            new SerializedMapPoint("HCZ_079", -6.624541f, -5.875854f, -9.300067f),//ok
            new SerializedMapPoint("EZ_Shelter", -1.331055f, 1.330017f, 8.138712f),//ok
            new SerializedMapPoint("Root_*&*Outside Cams", -23.45f, 4.0469f, -69.2814f), // Barrier Pont
            new SerializedMapPoint("Root_*&*Outside Cams", -22.28745f, 19.89001f, -44.4634f), // Tour
            new SerializedMapPoint("Root_*&*Outside Cams", 191f, -4f, -73f), // MTF
            new SerializedMapPoint("Root_*&*Outside Cams", -13.98f, 1.330017f, 2.1f), // cotée assenceur)
            new SerializedMapPoint("EZ_GateA", -0.2867508f, 1.329956f, 7.699394f), //ok
            new SerializedMapPoint("EZ_Intercom", -3.978503f, -6.669983f, -3.094589f),//Pas ok 
            new SerializedMapPoint("EZ_PCs", -1.406681f, 1.330017f, 3.080208f)//ok

        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
    }
}