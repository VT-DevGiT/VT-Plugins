using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_Alpha
{
    public class ConfigAlphaOne : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 150;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(0, 70, 24),
            Items = new List<SerializedPlayerItem>()
            {
                new SerializedPlayerItem((int)ItemType.KeycardO5,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunProject90,50,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunUSP,12,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeFrag,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeFrag,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet,12,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 100;
        public int MaxArtificialHealth = 100;

        [Description("The name of the class")]
        public string RoleName = "AlphaOne Agent";

    }
}
