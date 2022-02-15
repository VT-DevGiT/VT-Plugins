using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Api.Config;

namespace VT_HammerDown
{
    public class Config : AbstractConfigSection
    {
        [Description("The chance that a HamerDown Squad spawns instead of a MTF")]
        public float SpawnChance = 25f;

        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 7;

        [Description("The Cassie message when HamerDown spawn")]
        public string CassieSpawn = "";

        [Description("The Name of a HamerDown unit")]
        public string UnitName = "%RandomName%";

        [Description("The Config of The CustomRole HammerDown Cadet")]
        public string CadName = "Hammer-Down Cadet";
        public SerializedPlayerRole CadConfig = new SerializedPlayerRole()
        {
            Health = 100,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFOfficer,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunE11SR,25,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Painkillers,75,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio,75,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Flashlight,75,0,Vector3.one, 100, true),
                }
            }
        };

        [Description("The Config of The CustomRole HammerDown Lieutenant")]
        public string LtnName = "Hammer-Down Lieutenant";
        public int LtnMaxPerRespawn = 7;
        public SerializedPlayerRole LtnConfig = new SerializedPlayerRole()
        {
            Health = 110,

            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunLogicer,100,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE,75,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio,75,0,Vector3.one, 100, true),
                }
            }
        };
                
        [Description("The Config of The CustomRole HammerDown Commander")]
        public string CmdName = "Hammer-Down Commandant";
        public int CmdMaxPerRespawn = 2;
        public SerializedPlayerRole CmdConfig = new SerializedPlayerRole()
        {
            Health = 130,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFCommander,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunLogicer,100,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunCOM18,75,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Painkillers,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE,75,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio,75,0,Vector3.one, 100, true),
                }
            }
        };
    }
}
