using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigZoneMageur : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_EZ_Checkpoint", 5.943375f, 1.329956f, -5.99823f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(30, 70, 30),
            Items = new List<SerializedPlayerItem> ()
            { 
            new SerializedPlayerItem((int)ItemType.KeycardZoneManager, 1, 3, 2, 1, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.Flashlight, 50, 0, 0, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one, 100, true), 
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 0;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 7;

        [Description("The name of the class")]
        public string RoleName = "Zone-Manager";
    }
}