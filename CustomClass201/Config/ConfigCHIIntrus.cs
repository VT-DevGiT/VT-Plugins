using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigCHIIntrus : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.GunUSP, 18, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.KeycardChaosInsurgency, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 3;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Intrus";

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 100;
        public uint Ammo7 = 100;
        public uint Ammo9 = 100;
    }
}