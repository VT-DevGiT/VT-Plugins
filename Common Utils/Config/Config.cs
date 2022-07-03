using Synapse.Api.Enum;
using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;

namespace Common_Utiles.Config
{

    public class Config : AbstractConfigSection
    {
        [Description("The Config of the class")]
        public Dictionary<int, SerializedPlayerRole> configClasses = new Dictionary<int, SerializedPlayerRole>()
        {
            {(int)RoleID.ClassD, new SerializedPlayerRole()
            {
                Inventory = new SerializedPlayerInventory()
                {
                    Ammo = new SerializedAmmo(0, 0, 0, 0, 0),
                    Items = new List<SerializedPlayerItem>()
                    {
                        new SerializedPlayerItem ((int)ItemType.Coin, 1, 0, Vector3.one, 50, true),
                        new SerializedPlayerItem ((int)ItemType.Flashlight, 1, 0, Vector3.one, 50, true),
                    }
                },
            }},
            {(int)RoleID.SerpentsHand, new SerializedPlayerRole()
            {
                Health = 110,
                MaxHealth = 110,
            }},
        };

        /* Si sa fonctionne pas sa dégage
        [Description("If true it will remove the old recipes of 914. Curently not working.")]
        public bool RemouvRecipes = false;
        */

        [Description("A list of recipes to add for SCP 914. Curently not working.")]
        public List<Serialized914Recipe> Recipes = new List<Serialized914Recipe>()
        {
            new Serialized914Recipe((int)ItemType.KeycardFacilityManager, 
                new List<int>() { (int)ItemType.Coin },
                new List<int>() { (int)ItemType.ArmorHeavy, 55 },
                new List<int>() { (int)ItemType.Radio, (int)ItemType.Medkit },
                new List<int>() { (int)ItemType.ArmorHeavy },
                new List<int>() { (int)ItemType.Painkillers }),
            
            new Serialized914Recipe((int)ItemType.GunAK, 
                new List<int>() { (int)ItemType.None },
                new List<int>() { (int)ItemType.Coin },
                new List<int>() { 50 },
                new List<int>() { (int)ItemType.GunFSP9 },
                new List<int>() { 52, 51 }),
        };


        [Description("Makes the player change roles if he has the chance and the good role, Note if role is -1 is for all roles")]
        public List<Serialized914Role> Rnd914Roles = new List<Serialized914Role>()
        {
            new Serialized914Role()
            {
                RoleID = -1,
                Chance = 100,
                RoughRoleID = (int)RoleID.Spectator
            },

            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scientist,
                Chance = 100,
                CorseRoleID = 2
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfCaptain,
                Chance = 100,
                CorseRoleID = (int)RoleID.NtfSergeant
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfSergeant,
                Chance = 100,
                CorseRoleID = (int)RoleID.NtfPrivate
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfSpecialist,
                Chance = 100,
                CorseRoleID = (int)RoleID.NtfPrivate
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp049,
                Chance = 100,
                CorseRoleID = (int)RoleID.Scp0492
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp096,
                Chance = 100,
                CorseRoleID = (int)RoleID.Scp0492
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp173,
                Chance = 100,
                CorseRoleID = (int)RoleID.Scp0492
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp106,
                Chance = 100,
                CorseRoleID = (int)RoleID.Scp0492
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.SCP008,
                Chance = 100,
                CorseRoleID = (int)RoleID.Scp0492
            },



            new Serialized914Role()
            {
                RoleID = (int)RoleID.ClassD,
                Chance = 50,
                OneToOneRoleID = (int)RoleID.Janitor
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scientist,
                Chance = 50,
                OneToOneRoleID = (int)RoleID.ZoneManager
            }
        };

        [Description("Makes players passing through 914 have a random size")]
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
        public List<Serialized914Effect> list914Effect = new List<Serialized914Effect>()
        {
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 100,
                CorseEffect = Effect.Amnesia
            },
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 50,
                RoughEffect = Effect.Asphyxiated
            },
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 25,
                FineEffect = Effect.Scp207
            },
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 50,
                CorseEffect = Effect.Visuals939
            },
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 50,
                OneToOneEffect = Effect.Vitality
            },
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 50,
                OneToOneEffect = Effect.Bleeding
            },
        };

    }
}
