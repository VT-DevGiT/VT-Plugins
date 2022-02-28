using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;

namespace VTCustomClass
{
    public class Config : AbstractConfigSection
    {
        [Description("Sizes that are too small are bugged due to anti-cheat. But if you kill him, you won't have anything to verify that a player isn't stealing or cheating. if you choose true is becomes your responsibility")]
        public bool killAntiCheatPatch = false;

        [Description("The config of the spawn of customroles (odre import the top is priority)")]
        public List<SpawnClassConfig> SpawnClassConfigs = new List<SpawnClassConfig>()
        {
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.Janitor,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayers = 1,
                MaxSpawn = 1,
                SpawnChance = 100,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.GardeSuperviseur,
                ReplaceRoleID = (int)RoleID.FacilityGuard,
                MinRequiredPlayers = 1,
                MaxSpawn = 1,
                SpawnChance = 100,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.ScientifiqueSuperviseur,
                ReplaceRoleID = (int)RoleID.Scientist,
                MinRequiredPlayers = 1,
                MaxSpawn = 1,
                SpawnChance = 50,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.DirecteurSite,
                ReplaceRoleID = (int)RoleID.Scientist,
                MinRequiredPlayers = 1,
                MaxSpawn = 1,
                SpawnChance = 13,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.Technicien,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayers = 3,
                MaxSpawn = 1,
                SpawnChance = 10,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.SCP966,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayersInGame = 20,
                MaxSpawn = 2,
                SpawnChance = 25,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.SCP008,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayersInGame = 20,
                MaxSpawn = 1,
                SpawnChance = 25,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.SCP999,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayers = 0,
                MaxSpawn = 1,
                SpawnChance = 0,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.ZoneManager,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayers = 6,
                MaxSpawn = 1,
                SpawnChance = 20,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.GardePrison,
                ReplaceRoleID = (int)RoleID.ClassD,
                MinRequiredPlayers = 6,
                MaxSpawn = 1,
                SpawnChance = 20,
            },
            new SpawnClassConfig()
            {
                RoleID = (int)RoleID.FoundationUTR,
                ReplaceRoleID = (int)RoleID.FacilityGuard,
                MinRequiredPlayers = 3,
                MaxSpawn = 1,
                SpawnChance = 20,
            }
        };

        public List<RespawnClassConfig> RespawnClassConfig = new List<RespawnClassConfig>()
        {
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosLeader,
                ReplaceRoleID = (int)RoleID.ChaosRifleman,
                MaxAlive = 2,
                MaxPerRespawn = 1,
                SpawnChance = 15,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfLieutenant,
                ReplaceRoleID = (int)RoleID.NtfPrivate,
                MinRequiredPlayers = 5,
                MaxAlive = 2,
                MaxPerRespawn = 1,
                SpawnChance = 50,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.CdmCommander,
                ReplaceRoleID = (int)RoleID.NtfPrivate,
                MinRequiredPlayers = 5,
                MaxAlive = 2,
                MaxPerRespawn = 1,
                SpawnChance = 50,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfExpertReconfinement,
                ReplaceRoleID = (int)RoleID.NtfSergeant,
                MaxAlive = 2,
                MaxPerRespawn = 1,
                SpawnChance = 15,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfExpertPyrotechnie,
                ReplaceRoleID = (int)RoleID.NtfSergeant,
                MaxPerRespawn = 1,
                SpawnChance = 16,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfInfirmier,
                ReplaceRoleID = (int)RoleID.NtfSergeant,
                MaxPerRespawn = 1,
                SpawnChance = 16,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfLieutenantColonel,
                ReplaceRoleID = (int)RoleID.NtfPrivate,
                MinRequiredPlayers = 7,
                MaxPerRound = 2,
                MaxPerRespawn = 1,
                SpawnChance = 25,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.NtfVirologue,
                ReplaceRoleID = (int)RoleID.NtfSergeant,
                MaxAlive = 1,
                MaxPerRespawn = 1,
                SpawnChance = 10,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosMastodonte,
                ReplaceRoleID = (int)RoleID.ChaosRepressor,
                MaxAlive = 1,
                MaxPerRespawn = 1,
                SpawnChance = 100,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosKamikaze,
                ReplaceRoleID = (int)RoleID.ChaosMarauder,
                SpawnChance = 20,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosHacker,
                ReplaceRoleID = (int)RoleID.ChaosRifleman,
                MaxPerRespawn = 1,
                SpawnChance = 16,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosInfirmier,
                ReplaceRoleID = (int)RoleID.ChaosRifleman,
                MaxPerRespawn = 2,
                SpawnChance = 16,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosExpertPyrotechnie,
                ReplaceRoleID = (int)RoleID.ChaosRifleman,
                MaxPerRespawn = 1,
                SpawnChance = 16,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.ChaosSpy,
                ReplaceRoleID = (int)RoleID.NtfPrivate,
                MaxAlive = 1,
                MaxPerRound = 2,
                MaxPerRespawn = 1,
                SpawnChance = 10,
            },
            new RespawnClassConfig()
            {
                RoleID = (int)RoleID.MTFUTR,
                ReplaceRoleID = (int)RoleID.NtfCaptain,
                MaxAlive = 1,
                MaxPerRespawn = 1,
                SpawnChance = 25,
            }
        };

        public List<SpawnReplaceScpClassConfig> SpawnReplaceScpClassConfig = new List<SpawnReplaceScpClassConfig>()
        {
            new SpawnReplaceScpClassConfig()
            {
                RoleID = (int)RoleID.SCP008,
                MaxRequiredPlayersInGame = 19,
                MaxSpawn = 1,
                SpawnChance = 10,
            },
            new SpawnReplaceScpClassConfig()
            {
                RoleID = (int)RoleID.SCP966,
                MaxRequiredPlayersInGame = 19,
                MaxSpawn = 2,
                SpawnChance = 10,
            }
        };

        [Description("Config of the role Janirtor (ID = 100)")]
        public string JanirtorName = "Concierge";
        public SerializedPlayerRole JanirtorConfig = new SerializedPlayerRole()
        {
            Health = 110,
            SpawnPoints = new List<SerializedMapPoint>() 
            { 
                new SerializedMapPoint("LCZ_Toilets", -5.6f, 2.15f, -9.3f) 
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(0, 0, 0, 0, 0),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardJanitor, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Facility Guard Supervisor (ID = 101)")]
        public string GuarSupervisorName = "Garde Superviseur";
        public SerializedPlayerRole GuarSupervisorConfig = new SerializedPlayerRole()
        {
            Health = 110,
            SpawnPoints = new List<SerializedMapPoint>() 
            { 
                new SerializedMapPoint("HCZ_EZ_Checkpoint", 7.9f, 2.143616f, 0.02f) 
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(30, 70, 30, 30, 30),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFOfficer, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem(50, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem(55, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Scientist Supervisor (ID = 102)")]
        public string SciSupervisorName = "Scientifique Superviseur";
        public SerializedPlayerRole SciSupervisorConfig = new SerializedPlayerRole()
        {
            Health = 110,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(10, 10, 10, 10, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardResearchCoordinator, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 18, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Scientist Supervisor (ID = 103)")]
        public string DirectorName = "Directeur du Site";
        public SerializedPlayerRole DirectorConfig = new SerializedPlayerRole()
        {
            Health = 125,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("LCZ_Cafe (15)", 8.7f, 2.145415f, 3.7f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardFacilityManager, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 18, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Technicien (ID = 104)")]
        public int TechnicieCoolDown = 90;
        public int TechniciePowerTime = 20;
        public string TechnicienName = "Technicien";
        public SerializedPlayerRole TechnicienConfig = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)//TODO Change to serveur 
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(30, 30, 30, 30, 30),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardZoneManager, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 18, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit, 35, 0, Vector3.one, 100, true)
                }
            }
        };

        [Description("Config of the role NTF Lieutenant (ID = 105)")]
        public string LieutenantName = "Nine-Tailed Fox Lieutenant";
        public SerializedPlayerRole LieutenantConfig = new SerializedPlayerRole()
        {
            Health = 110,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunShotgun, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                }
            }
        };
        [Description("Config of the role NTF Commander (ID = 106)")]
        public string CommanderName = "Nine-Tailed Fox Commandant";
        public SerializedPlayerRole PCommanderConfig = new SerializedPlayerRole()
        {
            Health = 120,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFCommander, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 40, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem(50, 0, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int) ItemType.Adrenaline, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role NTF Lieutenant-Colonel (ID = 107)")]
        public string LtnConlonelName = "Nine-Tailed Fox Lieutenant-Colonel";
        public SerializedPlayerRole LtnConlonelConfig = new SerializedPlayerRole()
        {
            Health = 130,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardO5, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 40,  0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 18, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role NTF Reconfinement Expert (ID = 108)")]
        public string ReconfinementExpName = "Nine-Tailed Fox Expert en reconfinement";
        public SerializedPlayerRole ReconfinementExpConfig = new SerializedPlayerRole()
        {
            Health = 175,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardContainmentEngineer, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role NTF Pyrotechnie Expert (ID = 109)")]
        public string NtfPyrotechnieExpName = "Nine-Tailed Fox Expert en explosif";
        public SerializedPlayerRole NtfPyrotechnieExpConfig = new SerializedPlayerRole()
        {
            Health = 150,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 40, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role NTF Infirmier (ID = 110)")]
        public int NtfInfirmierCooldown = 105;
        public string NtfInfirmierName = "Nine-Tailed Fox Infirmier";
        public SerializedPlayerRole NtfInfirmierConfig = new SerializedPlayerRole()
        {
            Health = 105,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunE11SR, 17, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem(55, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem(55, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role NTF Virologue (ID = 111)")]
        public string VirologueName = "Nine-Tailed Fox Virologue";
        public SerializedPlayerRole VirologueConfig = new SerializedPlayerRole()
        {
            Health = 150,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>() 
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.SCP500, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Fondation UTR (ID = 112)")]
        public string FUTRName = "U.T.R.";
        public SerializedPlayerRole FUTRConfig = new SerializedPlayerRole()
        {
            Health = 180,
            ArtificialHealth = 150,
            MaxArtificialHealth = 150,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(300, 300, 300, 300, 300),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunShotgun, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Mastondonte (ID = 113)")]
        public string MastondonteName = "Mastondonte";
        public SerializedPlayerRole MastondonteConfig = new SerializedPlayerRole()
        {
            ArtificialHealth = 230,
            MaxArtificialHealth = 100,
            Health = 150,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(50, 520, 520, 520, 520),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem(200, 0, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 26, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Pyrotechnie Expert (ID = 114)")]
        public string ChiPyrotechnieExpName = "Expert en explosif";
        public SerializedPlayerRole ChiPyrotechnieExpConfig = new SerializedPlayerRole()
        {
            Health = 120,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(0, 100, 0, 100, 80),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Leader (ID = 115)")]
        public string LeaderName = "Leader";
        public int LeaderCooldown = 120;
        public SerializedPlayerRole LeaderConfig = new SerializedPlayerRole()
        {
            Health = 120,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(25, 125, 20, 125, 125),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardNTFCommander, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75,0, Vector3.one, 100, true),
                    new SerializedPlayerItem(50, 0, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false)
                }
            }
        };


        [Description("Config of the role CHI Hacker (ID = 116)")]
        public string HackerName = "Hacker";
        public int HackerCoolDownDoor = 30;
        public int HackerCoolDownLight = 60;
        public int HackerCoolDownMessage = 120;
        public SerializedPlayerRole HackerConfig = new SerializedPlayerRole()
        {
            Health = 110,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1,0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Kamikaze (ID = 117)")]
        public string KamikazeName = "Kamikaze";
        public float KamikazeGrenadeTimeDeath = 0.1f;
        public SerializedPlayerRole KamikazeConfig = new SerializedPlayerRole()
        {
            Health = 120,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(0, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100,0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Intruder (ID = 118)")]
        public string IntruderName = "Intrus";
        public SerializedPlayerRole IntruderConfig = new SerializedPlayerRole()
        {
            Health = 110,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_079", 10.1f, -2.4f, 0.09f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(50, 50, 50, 50, 50),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 18, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Spy (ID = 119)")]
        public string SpyName = "Infiltré";
        public SerializedPlayerRole SpyConfig = new SerializedPlayerRole()
        {
            Health = 90,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.KeycardNTFOfficer, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role CHI Infirmier (ID = 120)")]
        public string ChiInfirmierName = "Infirmier";
        public int ChiInfirmierCooldown = 105;
        public SerializedPlayerRole ChiInfirmierConfig = new SerializedPlayerRole()
        {
            Health = 110,
            Inventory = new SerializedPlayerInventory()
            {
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardChaosInsurgency, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCrossvec, 17, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem(55, 2, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem(55, 2, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem(55, 2, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Adrenaline, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Scp008 (ID = 122)")]
        public string Scp008Name = "SCP-008";
        public int Scp008AuraDomage = 5;
        public int Scp008AuraHeal = 10;
        public int Scp008AuraDistance = 2;
        public SerializedPlayerRole Scp008Config = new SerializedPlayerRole()
        {
            Health = 750,
            MaxHealth = 2200,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f) //TODO Change to 330
            },
        };

        [Description("Config of the role Scp966 (ID = 123)")]
        public string Scp966Name = "SCP-966";
        public SerializedPlayerRole Scp966Config = new SerializedPlayerRole()
        {
            Health = 500,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)
            },
        };

        [Description("Config of the role Scp682 (ID = 125) not finsh")]
        public string Scp682Name = "SCP-682";
        public SerializedPlayerRole Scp682Config = new SerializedPlayerRole()
        {
            Health = 3500,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)
            },
        };

        [Description("Config of the role Scp999 (ID = 124)")]
        public int Scp999HealHp = 10;
        public int Scp999DistanceForHeal = 2;
        public string Scp999Name = "SCP-999";
        public SerializedPlayerRole Scp999Config = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_EZ_Checkpoint", 5.943375f, 1.329956f, -5.99823f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.Coin, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 40,  0, Vector3.one, 100, false),
                }
            }
        };

        [Description("Config of the role Scp1048 (ID = 126) not finsh")]
        public int Scp1048CoolDown = 90;
        public string Scp1048Name = "SCP-1048";
        public SerializedPlayerRole Scp1048Config = new SerializedPlayerRole()
        {
            Health = 1400,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)
            },
        };

        [Description("Config of the role Anderson UTR heavy (ID = 139)")]
        public string AndUTRHeavyName = "<color=yellow>heavy Anderson U.T.R.</color>";
        public SerializedPlayerRole AndUTRHeavyConfig = new SerializedPlayerRole()
        {
            Health = 175,
            ArtificialHealth = 300,
            MaxArtificialHealth = 300,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Outside", 86.47166f, -10.64563f, -69.14687f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1,0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Anderson UTR light (ID = 140)")]
        public string AndUTRLightName = "<color=yellow>Light Anderson U.T.R.</color>";
        public SerializedPlayerRole AndUTRLightConfig = new SerializedPlayerRole()
        {
            Health = 120,
            ArtificialHealth = 150,
            MaxArtificialHealth = 150,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem> 
                {
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0,  Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Scp953 (ID = 131) not finsh")]
        public string Scp953Name = "SCP-953";
        public SerializedPlayerRole Scp953Config = new SerializedPlayerRole()
        {
            Health = 1400,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)
            },
        };

        [Description("Config of the role Guardian (ID = 146)")]
        public string GuardianName = "Gardien";
        public SerializedPlayerRole GuardianConfig = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                 new SerializedMapPoint("LCZ_ClassDSpawn (1)", 13f, 1.33f, 1f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(30, 70, 30, 30, 30),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 50, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem(50, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
                }
            }
        };

        [Description("Config of the role Zone Manager (ID = 147)")]
        public string ZoneManagerName = "Zone-Manager";
        public SerializedPlayerRole ZoneManagerConfig = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_EZ_Checkpoint", 5.943375f, 1.329956f, -5.99823f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(30, 70, 30, 30, 30),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.KeycardZoneManager, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GunCOM18, 1, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 50, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                }
            }
        };

        [Description("Config of the role MTF UTR (ID = 148)")]
        public string NtfUTRName = "Nine-Tailed Fox U.T.R.";
        public SerializedPlayerRole NtfUTRConfig = new SerializedPlayerRole()
        {
            Health = 250,
            ArtificialHealth = 200,
            MaxArtificialHealth = 200,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(300, 300, 300, 300, 300),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.GunLogicer, 75, 0, Vector3.one, 100, true),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.ArmorHeavy, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false)
                }
            }
        };

        [Description("Config of the role Staff (ID = 199)")]
        public string StaffName = "<color=red>Staff</color>";
        public SerializedPlayerRole StaffConfig = new SerializedPlayerRole()
        {
            Health = 1234567890,
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(100, 100, 100, 100, 100),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.Coin, 1, 0, Vector3.one, 100, false),
                    new SerializedPlayerItem((int)ItemType.Flashlight, 40,  0, Vector3.one, 100, false),
                }
            }
        };

        [Description("Config of the role 201 (ID = 201)")]
        public string Role201Name = " 201";
        public SerializedPlayerRole Role201Config = new SerializedPlayerRole()
        {
            Health = 100,
            SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f)
            },
            Inventory = new SerializedPlayerInventory()
            {
                Ammo = new SerializedAmmo(0, 0, 0, 0, 0),
                Items = new List<SerializedPlayerItem>()
                {
                    new SerializedPlayerItem((int)ItemType.Medkit, 35, 0, Vector3.one, 100, true)
                }
            },
        };

        [Description("UTR ignor SCP 173")]
        public bool UTRIngor173 = true;

        [Description("UTR ignor SCP 096")]
        public bool UTRIngor096 = true;

        [Description("List of the SCPs can damage UTR")]
        public List<int> UTRListScpDamge = new List<int>()
        {
            (int)RoleID.Scp0492, (int)RoleID.Scp096, (int)RoleID.Scp106,
            (int)RoleID.Scp93953, (int)RoleID.Scp93989, (int)RoleID.SCP008,
            (int)RoleID.SCP966
        };

        public int UTRScpDamage = 20;

        [Description("List of the SCPs cannot damge UTR")]
        public List<int> UTRListScpNoDamge = new List<int>()
        {
            (int)RoleID.Scp173, (int)RoleID.Scp049
        };
    }
}
