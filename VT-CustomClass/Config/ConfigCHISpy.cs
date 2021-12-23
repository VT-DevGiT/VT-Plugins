using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigCHISpy : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(0, 0, 100, 100, 100),
            Items = new List<SerializedPlayerItem>() 
            { 
                new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.KeycardNTFOfficer, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunLogicer, 1, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false)
             }
        };
        
        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 20;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 7;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 12;

        [Description("The name of the class")]
        public string RoleName = "Infiltré";
    }
}