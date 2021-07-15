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
    public class ConfigHammerDownLieutenant : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem>
            {
                new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunLogicer,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Adrenaline,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GrenadeFrag,75,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio,75,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 100;
        public int MaxShield = 125;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 7;

        [Description("The name of the class")]
        public string RoleName = "Hammer-Down Lieutenant";
    }
}
