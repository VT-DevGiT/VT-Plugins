using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Api.Config;

namespace VT_U2I
{
    public class Config : AbstractConfigSection
    {
        [Description("The chance that a U2I Squad spawns instead of a MTF")]
        public float SpawnChance = 25f;

        [Description("Rank for spawn on \"chef\" of the U2I (this is for RP server)")]
        public List<string> SpawnNeedRank = new List<string>() { };

        [Description("The Cassie message when U2I spawn")]
        public string CassieSpawn = "";

        [Description("The Name of a U2I unit")]
        public string UnitName = "%RandomName%";

        [Description("The maximal amount of players that can spawn as HamerDown in one squad")]
        public int SpawnSize = 11;

        [Description("U2I Agent Config")]
        public string U2IAgentName = "UIU-Agent";
        public SerializedPlayerRole U2IRoleConfig = new SerializedPlayerRole()
        {
            Health = 110,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(70, 70, 70, 70, 70),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardGuard,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunFSP9,35,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunCOM15,12,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Flashlight,75,0,Vector3.one, 100, true),
                }
            },
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Root_*&*Outside Cams", 187.5507f, -8.07251f, -6.48763f),
                new SerializedMapPoint("Root_*&*Outside Cams", 185.9299f, -8.444763f, -1.784706f),
                new SerializedMapPoint("Root_*&*Outside Cams", 183.4525f, -8.931152f, -1.332999f),
                new SerializedMapPoint("Root_*&*Outside Cams", 180.3424f, -9.680847f, -1.697027f),
                new SerializedMapPoint("Root_*&*Outside Cams", 177.6069f, -10.34033f, -1.783141f),
                new SerializedMapPoint("Root_*&*Outside Cams", 175.2793f, -10.65308f, -1.589358f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
                new SerializedMapPoint("Root_*&*Outside Cams", 172.6244f, -12.77948f, 9.865286f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.8324f, -12.73206f, 7.958255f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.9952f, -12.05273f, 5.345505f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.7792f, -11.40039f, 2.836424f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
            }
        };

        [Description("U2I Agent Liaison Config")]
        public string U2ILiaisonName = "U2I Liaison-Agent";
        public SerializedPlayerRole U2ILiaisonRoleConfig = new SerializedPlayerRole()
        {
            Health = 120,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(80, 80, 80, 80, 80),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline,0,0,Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunE11SR,40,0,Vector3.one, 100, true),
                }
            },
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("Root_*&*Outside Cams", 187.6646f, -5.909363f, -28.50043f)
            }
        };
       
    }
}
