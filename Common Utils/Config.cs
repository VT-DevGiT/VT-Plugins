using Synapse.Api.Enum;
using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Referance.Variable;

namespace Common_Utiles
{

    public class Config : AbstractConfigSection
    {
        [Description("The Config of the class")]
        public List<SerializedConfigClass> configClasses = new List<SerializedConfigClass>()
        {
            new SerializedConfigClass()
            {
                ClassID = (int)RoleID.ClassD,
                Health = null,
                MaxHealh = null,
                Shiled = null,
                MaxShiled = null,
                Inventory = new SerializedPlayerInventory()
                {
                    Ammo = new SerializedAmmo(0, 0, 0, 0, 0),
                    Items = new List<SerializedPlayerItem>()
                    {
                        new SerializedPlayerItem ((int)ItemType.Coin, 1, 0, Vector3.one, 50, true),
                        new SerializedPlayerItem ((int)ItemType.Flashlight, 1, 0, Vector3.one, 50, true),
                    }
                }
            },
            new SerializedConfigClass()
            {
                ClassID = (int)RoleID.SerpentsHand,
                Health = null,
                MaxHealh = null,
                Shiled = 40,
                MaxShiled = null,
                Inventory = new SerializedPlayerInventory()
            }
        };

        [Description("If true it will remove the old recipes of 914.")]
        public bool RemouvRecipes = true;

        [Description("A list of recipes for SCP 914.")]
        public List<Serialized914Recipe> Recipes = new List<Serialized914Recipe>()
        {
            new Serialized914Recipe((int)ItemType.KeycardFacilityManager, new List<int>() {(int)ItemType.Coin},
                new List<int>() {(int)ItemType.ArmorHeavy, 55 },new List<int>() {(int)ItemType.Radio, (int)ItemType.Medkit },
                new List<int>() { (int)ItemType.ArmorHeavy},new List<int>() {(int)ItemType.Painkillers}),
            new Serialized914Recipe((int)ItemType.GunAK, new List<int>() {(int)ItemType.None},
                new List<int>() {(int)ItemType.Coin},new List<int>() {50 },
                new List<int>() { (int)ItemType.GunFSP9},new List<int>() {52 , 51 }),
        };

        [Description("makes players passing through 914 have a random size")]
        public bool Rnd914Size = false;

        [Description("Max and Min Scale in X")]
        public float Max914SizeX = 1.2f;
        public float Min914SizeX = 0.5f;

        [Description("Max and Min Scale in Y")]
        public float Max914SizeY = 1.2f;
        public float Min914SizeY = 0.5f;

        [Description("Max and Min Scale in Z")]
        public float Max914SizeZ = 1.2f;
        public float Min914SizeZ = 0.5f;

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
        public float Max914Life = 200;
        public float Min914Life = 10;

        [Description("makes players passing through 914 have a random artificial life")]
        public bool Rnd914ArtificialLife = false;

        [Description("Max and Min artificial life")]
        public ushort Max914ArtificialLife = 200;
        public ushort Min914ArtificialLife = 10;

        [Description("The percent chance of dying for players passing through 914")]
        public float Rnd914ChanceDie = 50;
    }
}
