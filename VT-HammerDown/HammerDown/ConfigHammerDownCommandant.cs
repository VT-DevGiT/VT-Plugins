using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_HammerDown
{
    public class ConfigHammerDownCommandant : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 130;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem>
            {
                new SerializedPlayerItem((int)ItemType.KeycardNTFCommander,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunLogicer,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunUSP,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Painkillers,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeFrag,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio,75,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 125;
        public int MaxShield = 150;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 50;

        [Description("The name of the class")]
        public string RoleName = "Hammer-Down Commandant";
    }
}
