using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigFoundationUTR : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 180;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(300, 300, 300, 300, 300),
            Items = new List<SerializedPlayerItem> ()
            { 
            new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
            new SerializedPlayerItem((int)ItemType.GunShotgun, 1, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
            new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, true),
            new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, true) 
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 150;
        public int MaxShield = 150;
        
        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 25;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 3;

        [Description("The name of the class")]
        public string RoleName = " U.T.R.";
    }
}