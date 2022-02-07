using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Api.Config;

namespace VT_AndersonRobotic
{
    public class Config : AbstractConfigSection
    {

        [Description("The chance that a Anderson Robotic Squad spawns instead of a Chaos")]
        public float SpawnChance = 50f;

        [Description("The max of Anderson Spawn")]
        public float SpawnMax = 1;

        [Description("The Spawnpoint where Anderson Robotic spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f);

        [Description("The maximal amount of players that can spawn as Anderson Robotic Unite in one squad")]
        public int SpawnSizeAnderson = 7;

        [Description("The maximal amount of players that can spawn as Asimov Unite in one squad")]
        public int SpawnSizeAsimov = 20;

        [Description("The Config of the Leader and the Engineer Anderson")]
        public string LeaderName = "<color=yellow>Anderson Leader</color>";
        public SerializedPlayerRole LeaderConfig = new SerializedPlayerRole()
        {
            Health = 120,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFCommander, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, true),
                }
            }
        };

        public string EngineerName = "<color=yellow>Anderson Engineer</color>";
        public SerializedPlayerRole EngineerConfig = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, true)
                }
            }
        };

        [Description("The Config of the General and Solder of the Asimov")]
        public string GeneralName = "General Gamma-1";
        public SerializedPlayerRole GeneralAsimov = new SerializedPlayerRole()
        {
            Health = 120,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFCommander, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, true),
                }
            }
        };

        public string GuardianName = "Guardian Gamma-1";
        public SerializedPlayerRole GuardianAsimov = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, true)
                }
            }
        };
    }
}