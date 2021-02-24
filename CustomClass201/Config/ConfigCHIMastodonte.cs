using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigCHIMastodonte : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.KeycardChaosInsurgency, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GunLogicer, 75, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GrenadeFlash, 1, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 15;

        [Description("Max alive at the same time")]
        public int MaxAlive = 2;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 100;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Mastondonte";

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 215;
        public int MaxArtificialHealth = 0;
    }
}