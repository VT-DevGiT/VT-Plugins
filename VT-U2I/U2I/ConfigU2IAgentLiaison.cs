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
    public class ConfigU2IAgentLiaison : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(80, 0, 24),
            Items = new List<SerializedPlayerItem>()
            {
                new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Adrenaline,0,0,0,0,Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunE11SR,40,0,0,0,Vector3.one, 100, true),
            }
        };

        [Description("Shield of the class")]
        public int Shield = 110;
        public int MaxShield = 110;

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("The name of the class")]
        public string RoleName = "U2I Liaison-Agent";

    }
}
