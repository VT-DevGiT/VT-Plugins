using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigCHIHacker : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem> ()
            { 
                new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 3, 2, 1, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunProject90, 50, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 20;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 11;

        [Description("Max alive at the same time")]
        public int MaxAlive = 100;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Hacker";

        [Description("The cooldown of the class Power")]
        public int CoolDownDoor = 30;
        public int CoolDownLight = 60;
        public int CoolDownMessage = 120;
    }
}