using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigCHILeader : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 170;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() {
            new SerializedItem((int)ItemType.KeycardNTFCommander, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.GunLogicer, 75, 0, 0, 0, Vector3.one),
            new SerializedItem(50, 1, 0, 1, 0, Vector3.one),
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 170;
        public int ArtificialHealth = 170;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Leader";
        
        [Description("The cooldown of the class Power")]
        public int Cooldown = 150;

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 25;
        public uint Ammo7 = 125;
        public uint Ammo9 = 25;
    }
}