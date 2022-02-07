using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Api.Config;

namespace VT_Alpha
{
    public class Config : AbstractConfigSection
    {
        [Description("The chance that a Apha-1 Squad spawns when WarHead Start")]
        public float SpawnChance = 50f;

        [Description("Max and Min player respawn")]
        public int MaxPlayer = -1;
        public int MinPlayer = -1;

        [Description("Max Respawn of Alpha-1 in one round")]
        public int MaxRepsawn = -1;

        [Description("The Cassie message when Alpha-1 spawn")]
        public string CassieSpawn = "Attention . The MtfUnit Alpha One Designate %UnitName% have entered the facility";

        [Description("The Name of a Alpha-1 unit")]
        public string UnitName = "%RandomName%";

        [Description("The name of the class")]
        public string RoleName = "AlphaOne Agent";

        [Description("The Config a Alpha-a unit")]
        public SerializedPlayerRole AlphaOneUnit = new SerializedPlayerRole()
        {

            Health = 150,
            Inventory = new SerializedPlayerInventory()
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
            }
        };
    }
}
