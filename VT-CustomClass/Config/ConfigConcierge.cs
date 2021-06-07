using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigConcierge : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_Toilets", -5.6f, 2.15f, -9.3f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo (0, 0, 0),
            Items = new List<SerializedPlayerItem> ()
            { 
                new SerializedPlayerItem((int)ItemType.KeycardJanitor, 1, 0, 0, 0, Vector3.one, 100, true), 
                new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int MaxArtificialHealth = 100;
        public int ArtificialHealth = 0;

        [Description("Shield of the class")]
        public int Shield = 0;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Concierge";
    }
}