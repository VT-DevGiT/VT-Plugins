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

        [Description("A list of recipes to add for SCP 914.")]
        public List<Serialized914Recipe> Recipes = new List<Serialized914Recipe>()
        {
            new Serialized914Recipe(50, 
                new List<int>() { (int)ItemType.ArmorLight},
                new List<int>() { (int)ItemType.ArmorLight },
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
                RoughRoleID = (int)RoleID.Spectator,
                CorseRoleID = -1,
                OneToOneRoleID = -1,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp173,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.Scp0492,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ClassD,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 2,
                OneToOneRoleID = -3,
                FineRoleID = -1,
                VeryFineRoleID = -1

            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp106,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfSpecialist,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.NtfPrivate,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp049,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.Scp0492,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scientist,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 2,
                OneToOneRoleID = -6,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosConscript,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.ChaosRifleman,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp096,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.Scp0492,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp0492,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.Spectator,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },

            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfSergeant,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.NtfPrivate,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1

            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfCaptain,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.NtfPrivate,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfPrivate,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = -2,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.FacilityGuard,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = -2,
                OneToOneRoleID = -5,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp93953,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp93989,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosRifleman,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 2,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosRepressor,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.ChaosRifleman,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosMarauder,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.ChaosRifleman,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.SerpentsHand,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp035,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Scp056,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Janitor,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 1,
                OneToOneRoleID = -3,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.GardeSuperviseur,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 15,
                OneToOneRoleID = -5,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ScientifiqueSuperviseur,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 6,
                OneToOneRoleID = -6,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.DirecteurSite,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 6,
                OneToOneRoleID = -6,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.Technicien,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 15,
                OneToOneRoleID = -5,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfLieutenant,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.NtfPrivate,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfCommander,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfLieutenantColonel,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfExpertReconfinement,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfExpertPyrotechnie,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1

            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfInfirmier,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.NtfVirologue,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.FoundationUTR,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 15,
                OneToOneRoleID = -5,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosMastodonte,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = (int)RoleID.ChaosRifleman,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
             new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosExpertPyrotechnie,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
              new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosLeader,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
               new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosHacker,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosKamikaze,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                 new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosIntrus,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                  new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosSpy,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                   new Serialized914Role()
            {
                RoleID = (int)RoleID.ChaosInfirmier,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 18,
                OneToOneRoleID = -7,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },                    
            new Serialized914Role()
            {
                RoleID = (int)RoleID.SCP008,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
             new Serialized914Role()
            {
                RoleID = (int)RoleID.SCP966,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
              new Serialized914Role()
            {
                RoleID = (int)RoleID.SCP999,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 10,
                OneToOneRoleID = -2,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
               new Serialized914Role()
            {
                RoleID = (int)RoleID.CdmCadet,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                new Serialized914Role()
            {
                RoleID = (int)RoleID.CdmLieutenant,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },
                 new Serialized914Role()
            {
                RoleID = (int)RoleID.CdmCommander,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },              
            new Serialized914Role()
            {
                RoleID = (int)RoleID.GardePrison,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 15,
                OneToOneRoleID = -5,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },              
            new Serialized914Role()
            {
                RoleID = (int)RoleID.ZoneManager,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 6,
                OneToOneRoleID = -6,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },              
            new Serialized914Role()
            {
                RoleID = (int)RoleID.MTFUTR,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },              
            new Serialized914Role()
            {
                RoleID = (int)RoleID.U2IAgent,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },              
            new Serialized914Role()
            {
                RoleID = (int)RoleID.U2IAgentLiaison,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },    
            new Serialized914Role()
            {
                RoleID = (int)RoleID.AlphaOneAgent,
                Chance = 100,
                RoughRoleID = -1,
                CorseRoleID = 13,
                OneToOneRoleID = -4,
                FineRoleID = -1,
                VeryFineRoleID = -1
            },

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


        // -1 = rien/ -2 = effect positif/ -3 Super effect w maluce
        [Description("makes players passing through 914 have a random effect")]
        public List<Serialized914Effect> list914Effect = new List<Serialized914Effect>()
        {
            new Serialized914Effect()
            {
                RoleID = -1,
                Chance = 100,
                RoughEffect = -1,
                CorseEffect = -1,
                OneToOneEffect = -1,
                FineEffect = -2,
                VeryFineEffect = -3
            },
        };

    }
}
