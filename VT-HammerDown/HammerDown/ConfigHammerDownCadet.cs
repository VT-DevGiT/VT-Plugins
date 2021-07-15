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
    public class ConfigHammerDownCadet : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem>
            {
                new SerializedPlayerItem((int)ItemType.KeycardSeniorGuard,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunE11SR,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Painkillers,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Flashlight,75,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 75;
        public int MaxShield = 125;

        [Description("The name of the class")]
        public string RoleName = "Hammer-Down Cadet";
    }
}
