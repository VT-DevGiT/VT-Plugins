using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigNTFInfirmier : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 115;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem> () 
            { 
                new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.GunE11SR, 17, 1, 3, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem(55, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem(55, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 50;
        public int ArtificialHealth = 50;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 16;

        [Description("Max alive at the same time")]
        public int MaxAlive = 100;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 100;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Nine-Tailed Fox Infirmier";

        [Description("The cooldown of the class Power")]
        public int Cooldown = 150;
    }
}