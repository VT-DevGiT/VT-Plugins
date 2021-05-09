using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_U2I
{
    public class ConfigU2IAgent : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(0, 70, 24),
            Items = new List<SerializedPlayerItem>()
            {
                new SerializedPlayerItem((int)ItemType.KeycardGuard,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunMP7,35,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunCOM15,12,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Flashlight,75,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 50;
        public int MaxArtificialHealth = 100;

        [Description("The name of the class")]
        public string RoleName = "U2I Agent";

    }
}
