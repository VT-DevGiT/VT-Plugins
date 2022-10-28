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

        [Description("If true it will remove the old recipes of 914.")]
        public bool RemouvRecipes = false;
        

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
                ID = new TeamOrRole(-1),
                Chance = 100,
                RoughID = new TeamOrRole((int)RoleID.Spectator),
                CorseID = new TeamOrRole(-1),
                OneToOneID = new TeamOrRole(-1),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole((int)RoleID.Scp173),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole((int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole((int)RoleID.ClassD),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Spectator),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CDP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)

            },
            new Serialized914Role()
            {
                ID = new TeamOrRole((int)RoleID.Scp106),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfSpecialist),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole((int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp049),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scientist),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Spectator),
                OneToOneID = new TeamOrRole(false, (int)TeamID.RSC),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosConscript),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true,(int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp096),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp0492),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Spectator),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },

            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfSergeant),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)

            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfCaptain),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(false, (int)TeamID.SCP),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.FacilityGuard),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(false, (int)TeamID.SCP),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp93953),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp93989),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Spectator),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosRepressor),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosMarauder),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.SerpentsHand),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp035),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Scp056),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Janitor),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ClassD),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CDP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.GardeSuperviseur),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.FacilityGuard),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ScientifiqueSuperviseur),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scientist),
                OneToOneID = new TeamOrRole(false, (int)TeamID.RSC),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.DirecteurSite),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scientist),
                OneToOneID = new TeamOrRole(false, (int)TeamID.RSC),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.Technicien),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ClassD),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfLieutenant),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfCommander),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfLieutenantColonel),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfExpertReconfinement),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfExpertPyrotechnie),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)

            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfInfirmier),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.NtfVirologue),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.FoundationUTR),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.FacilityGuard),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosMastodonte),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosExpertPyrotechnie),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosLeader),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosHacker),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosKamikaze),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosIntrus),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosSpy),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ChaosInfirmier),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.ChaosRifleman),
                OneToOneID = new TeamOrRole(false, (int)TeamID.CHI),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.SCP008),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
             new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.SCP966),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.SCP999),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scp0492),
                OneToOneID = new TeamOrRole(false, (int)TeamID.SCP),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
             new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.CdmCadet),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.CdmLieutenant),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.CdmCommander),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },              
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.GardePrison),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.FacilityGuard),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },              
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.ZoneManager),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.Scientist),
                OneToOneID = new TeamOrRole(false, (int)TeamID.RSC),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },              
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.MTFUTR),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },              
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.U2IAgent),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },              
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.U2IAgentLiaison),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
            },    
            new Serialized914Role()
            {
                ID = new TeamOrRole(true, (int)RoleID.AlphaOneAgent),
                Chance = 100,
                RoughID = new TeamOrRole(-1),
                CorseID = new TeamOrRole(true, (int)RoleID.NtfPrivate),
                OneToOneID = new TeamOrRole(false, (int)TeamID.NTF),
                FineID = new TeamOrRole(-1),
                VeryFineID = new TeamOrRole(-1)
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


    }
}
