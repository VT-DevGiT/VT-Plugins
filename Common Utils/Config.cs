using Synapse.Api.Enum;
using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Common_Utiles
{

    public class Config : AbstractConfigSection
    {
        [Description("The list of objects that each class will have when it spawn")]

        public SerializedPlayerInventory ClassDInventory = new SerializedPlayerInventory()
        { 
            Ammo = new SerializedAmmo(0,0,0),
            Items = new List<SerializedPlayerItem> ()
            {
                new SerializedPlayerItem ((int)ItemType.Coin, 1, 0, 0, 0, Vector3.one, 50, true),
                new SerializedPlayerItem ((int)ItemType.Flashlight, 1, 0, 0, 0, Vector3.one, 50, true),
            }
        };

        public SerializedPlayerInventory ChaosInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory ScientistInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory GuardInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory CadetInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory LieutenantInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory NtfSciInventory = new SerializedPlayerInventory();

        public SerializedPlayerInventory CommanderInventory = new SerializedPlayerInventory();

        [Description("A list of roles and what their default starting health should be.")]
        public int ClassDHealth = -1;
        public int ChaosHealth = -1;
        public int ScientistHealth = -1;
        public int GuardHealth = -1;
        public int CadetHealth = -1;
        public int LieutenantHealth = -1;
        public int NtfSciHealth = -1;
        public int CommanderHealth = -1;
        public int InsurgéHealth = -1;
        public int Scp049 = -1;
        public int Scp0492 = -1;
        public int Scp096 = -1;
        public int Scp106 = -1;
        public int Scp173 = -1;
        public int Scp93953 = -1;
        public int Scp93989 = -1;

        [Description("A list of recipes for SCP 914. Be careful if the config is not empty it will remove the old recipes. ")]
        public List<Serialized914Recipe> Recipes = new List<Serialized914Recipe>()
        {
            new Serialized914Recipe((int)ItemType.KeycardFacilityManager, new List<int>() {(int)ItemType.Coin},
                new List<int>() {(int)ItemType.WeaponManagerTablet, 55 },new List<int>() {(int)ItemType.Radio, (int)ItemType.Medkit },
                new List<int>() { (int)ItemType.WeaponManagerTablet},new List<int>() {(int)ItemType.Painkillers}),
            new Serialized914Recipe((int)ItemType.GunMP7, new List<int>() {(int)ItemType.None},
                new List<int>() {(int)ItemType.Coin},new List<int>() {50 },
                new List<int>() { (int)ItemType.GunProject90},new List<int>() {52 , 51 }),
        };

        [Description("makes players passing through 914 have a random size")]
        public bool Rnd914Size = false;

        [Description("Max and Min Scale in X")]
        public float Max914SizeX = 1;
        public float Min914SizeX = 2;

        [Description("Max and Min Scale in Y")]
        public float Max914SizeY = 1;
        public float Min914SizeY = 2;

        [Description("Max and Min Scale in Z")]
        public float Max914SizeZ = 1;
        public float Min914SizeZ = 2;

        [Description("makes players passing through 914 have a random effect")]
        public List<Effect> list914Effect = new List<Effect>()
        {
            Effect.Amnesia,
            Effect.Asphyxiated,
            Effect.Scp207,
            Effect.Visuals939,
            Effect.ArtificialRegen,
            Effect.Blinded,
        };
        [Description("makes players passing through 914 have a random life")]
        public bool Rnd914Life = false;

        [Description("Max and Min life")]
        public float Max914Life = 1;
        public float Min914Life = 2;

        [Description("makes players passing through 914 have a random artificial life")]
        public bool Rnd914ArtificialLife = false;

        [Description("Max and Min artificial life")]
        public float Max914ArtificialLife = 1;
        public float Min914ArtificialLife = 2;

        [Description("The percent chance of dying for players passing through 914")]
        public float Rnd914ChanceDie = 50;
    }
}
