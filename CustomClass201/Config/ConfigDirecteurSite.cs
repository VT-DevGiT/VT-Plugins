using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigDirecteurSite : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_Cafe (15)", 8.7f, 2.145415f, 3.7f);

        [Description("The Amount of Health the class have")]
        public int Health = 125;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.KeycardFacilityManager, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.GunUSP, 18, 0, 0, 0, Vector3.one),
            new SerializedItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 50;
        public int ArtificialHealth = 35;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 13;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Directeur du site";

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 30;
        public uint Ammo7 = 30;
        public uint Ammo9 = 30;
    }
}