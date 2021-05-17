using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigCHILeader : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(25, 125, 20),
            Items = new List<SerializedPlayerItem>() 
            { 
                new SerializedPlayerItem((int)ItemType.KeycardNTFCommander, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem(50, 1, 0, 1, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 100;

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
    }
}