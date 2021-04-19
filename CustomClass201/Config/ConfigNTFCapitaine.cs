using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigNTFCapitaine : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 115;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() {
            new SerializedItem((int)ItemType.KeycardNTFCommander, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.GunE11SR, 40, 4, 1, 2, Vector3.one),
            new SerializedItem(50, 1, 1, 0, 1, Vector3.one),
            new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int) ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one)};

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 100;
        public int MaxArtificialHealth = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 50;

        [Description("Max alive at the same time")]
        public int MaxAlive = 2;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 5;

        [Description("The name of the class")]
        public string RoleName = "Nine-Tailed Fox Capitaine";

        [Description("The cooldown of the class Power")]
        public int CoolDown = 30;

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 100;
        public uint Ammo7 = 100;
        public uint Ammo9 = 100;
    }
}