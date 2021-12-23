using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigNTFVirologue : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 150;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
            Items = new List<SerializedPlayerItem>() { 
            new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.SCP500, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 40;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 10;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Nine-Tailed Fox Virologue";
    }
}