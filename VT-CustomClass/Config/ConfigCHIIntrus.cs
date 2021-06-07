using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigCHIIntrus : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_079", 10.1f, -2.4f, 0.09f);

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(30, 30, 30),
            Items = new List<SerializedPlayerItem>() 
            { 
                new SerializedPlayerItem((int)ItemType.GunUSP, 18, 2, 1, 1, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 25;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 3;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Intrus";
    }
}