using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigNTFVirologue : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 170;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.KeycardNTFLieutenant, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.GunProject90, 50, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.SCP500, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 100;
        public int MaxArtificialHealth = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 10;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Virologue";

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 100;
        public uint Ammo7 = 100;
        public uint Ammo9 = 100;
    }
}