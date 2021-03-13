using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_MTF
{
    public class Config
    {
        [Description("The Health of the Cadet")]
        public int HealthCadet = 120;

        [Description("The Health of the Lieutenant")]
        public int HealthLieutenant = 150;

        [Description("The Health of the Commander")]
        public int HealthCommander = 180;
        
        [Description("The displayed name of the Cadet")]
        public string CustomRoleNameCadet = "HamerDownCadet (%Squad%)";

        [Description("The displayed name of the Lieutenant")]
        public string CustomRoleNameLieutenant = "HamerDownLieutenant (%Squad%)";

        [Description("The displayed name of the Commander")]
        public string CustomRoleNameCommander = "HamerDownCommander (%Squad%)";

        [Description("The chance that a HamerDown Squad spawns instead of a MTF")]
        public float SpawnChance = 50f;

        [Description("The amount of ammo for the Cadet")]
        public Ammo AmmoCadet = new Ammo();

        [Description("The amount of ammo for the Lieutenant")]
        public Ammo AmmoLieutenant = new Ammo();

        [Description("The amount of ammo for the Commandant")]
        public Ammo AmmoCommandant = new Ammo();

        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 7;

        [Description("The maximal amount of Lieutenant that can spawn as HamerDown in one squad")]
        public int MaxLieutenant = 7;

        [Description("The items that HamerDown Cadet spawn with")]
        public List<SerializedItem> ItemsCadet = new List<SerializedItem>
        {
            new SerializedItem((int)ItemType.KeycardSeniorGuard,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Medkit,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.GunE11SR,75,0,0,0,Vector3.one),
        };

        [Description("The items that HamerDown Lieutenant spawn with")]
        public List<SerializedItem> ItemsLieutenant = new List<SerializedItem>
        {
            new SerializedItem((int)ItemType.KeycardNTFLieutenant,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Medkit,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Adrenaline,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.GunLogicer,75,0,0,0,Vector3.one),
        };

        [Description("The items that HamerDown Commander spawn with")]
        public List<SerializedItem> ItemsCommandant = new List<SerializedItem>
        {
            new SerializedItem((int)ItemType.KeycardNTFCommander,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Medkit,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Painkillers,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.GunLogicer,75,0,0,0,Vector3.one),
        };

    }
    public class Ammo
    {
        public uint Ammo5 = 50;

        public uint Ammo7 = 50;

        public uint Ammo9 = 50;
    }
}
