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
            Ammo = new SerializedAmmo(0, 0, 0, 70, 24),
            Items = new List<SerializedPlayerItem>()
            {
                new SerializedPlayerItem((int)ItemType.KeycardO5,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunCrossvec,50,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunCOM18,12,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeHE,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeHE,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.ArmorLight,12,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 150;
        public int MaxShield = 150;

        [Description("The name of the class")]
        public string RoleName = "AlphaOne Agent";

    }
}
