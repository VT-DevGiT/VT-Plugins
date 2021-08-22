using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigSCP166 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_Plants", -0.0884471f, 2.171398f, -4.554413f);

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
            Items = new List<SerializedPlayerItem>() 
            {
            new SerializedPlayerItem((int)ItemType.KeycardScientist, 1, 0, Vector3.one, 100, true),
            new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, true),
            }
        };

        [Description("The Amount of Health the class have")]
        public int Health = 200;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 150;

        [Description("Shield of the class")]
        public int Shield = 0;
        public int MaxShield = 0;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 35;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 3;

        [Description("The name of the class")]
        public string RoleName = "SCP-166-Az";

        [Description("The distance that the class must have with other class for the power")]
        public int Distance = 6;
    }
}