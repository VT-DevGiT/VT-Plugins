using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigAndersonUTRheavy : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f);

        [Description("The Amount of Health the class have")]
        public int Health = 160;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.GunLogicer, 75, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Flashlight, 1, 0, 0, 0, Vector3.one) };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 250;
        public int MaxArtificialHealth = 250;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 5;

        [Description("The name of the class")]
        public string RoleName = "<color=yellow>heavy Anderson U.T.R.</color>";
        
        [Description("the number of ammo to the class")]
        public uint Ammo5 = 100;
        public uint Ammo7 = 100;
        public uint Ammo9 = 100;
    }
}