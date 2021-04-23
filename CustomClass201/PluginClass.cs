using CustomClass.Config;
using CustomClass.PlayerScript;
using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using Synapse.Translation;
using System.Collections.Generic;
using VT_Referance.Variable;
using CustomClass.CustomTeam;

namespace CustomClass
{
    [PluginInformation(
        Author = "VT",
        Description = "A plugin for add new class",
        LoadPriority = 1,
        Name = "CustomClass",
        SynapseMajor = 2,
        SynapseMinor = 4,
        SynapsePatch = 1,
        Version = "v.1.1.3")]
    public class PluginClass : AbstractPlugin
    {
        public static PluginClass Plugin;
        //la config générale
        [Synapse.Api.Plugin.Config(section = "CustomClass-General")]
        public static ConfigCustomClass ConfigCustomClass;

        //les config des rôles custome


        //Config de la Classe 201 obsolete car désactivée.
        //[Synapse.Api.Plugin.Config(section = "CustomClass-CustomClass201")]
        //public static Config201 Config201;

        [Synapse.Api.Plugin.Config(section = "ConfigConcierge")]
        public static ConfigConcierge ConfigConcierge;

        [Synapse.Api.Plugin.Config(section = "ConfigAndersonUTRheavy")]
        public static ConfigAndersonUTRheavy ConfigAndersonUTRheavy;

        [Synapse.Api.Plugin.Config(section = "ConfigConcierge")]
        public static ConfigAndersonUTRlight ConfigAndersonUTRlight;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigDirecteurSite")]
        public static ConfigDirecteurSite ConfigDirecteurSite;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigScientifiqueSuperviseur")]
        public static ConfigScientifiqueSuperviseur ConfigScientifiqueSuperviseur;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigGardeSuperviseur")]
        public static ConfigGardeSuperviseur ConfigGardeSuperviseur;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFSergent")]
        public static ConfigNTFSergent ConfigNTFSergent;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFCapitaine")]
        public static ConfigNTFCapitaine ConfigNTFCapitaine;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFLieutenantColonel")]
        public static ConfigNTFLieutenantColonel ConfigNTFLieutenantColonel;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFExpertPyrotechnie")]
        public static ConfigNTFExpertPyrotechnie ConfigNTFExpertPyrotechnie;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFExpertReconfinement")]
        public static ConfigNTFExpertReconfinement ConfigNTFExpertReconfinement;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFInfirmier")]
        public static ConfigNTFInfirmier ConfigNTFInfirmier;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIInfirmier")]
        public static ConfigCHIInfirmier ConfigCHIInfirmier;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigNTFVirologue")]
        public static ConfigNTFVirologue ConfigNTFVirologue;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIExpertPyrotechnie")]
        public static ConfigCHIExpertPyrotechnie ConfigCHIExpertPyrotechnie;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIHacker")]
        public static ConfigCHIHacker ConfigCHIHacker;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIIntrus")]
        public static ConfigCHIIntrus ConfigCHIntrus;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIKamikaze")]
        public static ConfigCHIKamikaze ConfigCHIKamikaze;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHILeader")]
        public static ConfigCHILeader ConfigCHILeader;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHIMastondonte")]
        public static ConfigCHIMastodonte ConfigCHIMastondonte;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigCHISPY")]
        public static ConfigCHISpy ConfigCHISPY;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigFoundationUTR")]
        public static ConfigFoundationUTR ConfigFoundationUTR;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP008")]
        public static ConfigSCP008 ConfigSCP008;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP166")]
        public static ConfigSCP166 ConfigSCP166Az;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP1048")]
        public static ConfigSCP1048 ConfigSCP1048;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP507")]
        public static ConfigSCP507 ConfigSCP507;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP682")]
        public static ConfigSCP682 ConfigSCP682;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP966")]
        public static ConfigSCP966 ConfigSCP966;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP953")]
        public static ConfigSCP953 ConfigSCP953;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigSCP999")]
        public static ConfigSCP999 ConfigSCP999;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigTechnicien")]
        public static ConfigTechnicien ConfigTechnicien;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigStaff")]
        public static ConfigStaff ConfigStaff;

        [Synapse.Api.Plugin.Config(section = "CustomClass-ConfigTestClass")]
        public static ConfigTestClass ConfigTestClass;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation;

        public Dictionary<RoleID, int> RespawnedPlayer = new Dictionary<RoleID, int>();

        //On lance les patch pour pouvoir kill certaint entiCheat si actif
        private void PatchAll()
        {
            var instance = new Harmony("CustomClass");
            instance.PatchAll();
            Server.Get.Logger.Info("Custom class Harmony Patch done!");
        }
        
        public override void Load()
        {
            Plugin = this;
            RegisterCustomTeam();
            RegisterCustomRole();
            PluginTranslation.AddTranslation(new CustomClass.PluginTranslation());
            PluginTranslation.AddTranslation(new CustomClass.PluginTranslation{
            SpawnMessage = "<color=blue><b>Tu es à présent</b></color> <color=red><b>%RoleName%</b></color>",
            VentMessage = "Vous pouvez rester encore %Time% secondes dans la ventilation",
            NoTimeVentMessage = "Vous vous trouvez dans les ventilation",
            PowerCooldown = "vous pouvez utiliser ce pouvoir dans %Time% secondes"
            }, "FRENCH");
            PatchAll();
            new EventHandlers();
            
        }

        //On registre les team pour les certainne classe
        public void RegisterCustomTeam()
        {
            Server.Get.TeamManager.RegisterTeam<NetralSCPTeam>();
            Server.Get.TeamManager.RegisterTeam<BerserkSCPTeam>();
            Server.Get.TeamManager.RegisterTeam<VIPTeam>();
            Server.Get.TeamManager.RegisterTeam<StaffTeam>();
        }

        //On registre les Scripte
        public void RegisterCustomRole()
        {
            //Role 201 dans le but de teste si les Scriptes ont bien été crée Obselette.
            //Server.Get.RoleManager.RegisterCustomRole<Scripte201>();

            string curAssemblyFolder = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;

            if (System.IO.File.Exists($"{curAssemblyFolder}\\VT-AndersonRobotic.dll") || ConfigCustomClass.Anderson)
            {
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRlightScript>();
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRheavyScript>();
            }
            Server.Get.RoleManager.RegisterCustomRole<ConciergeScript>();
            Server.Get.RoleManager.RegisterCustomRole<DirecteurSiteScript>();
            Server.Get.RoleManager.RegisterCustomRole<ScientifiqueSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIHackerScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIInfirmierScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIIntrusScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIKamikazeScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHILeaderScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIMastodonteScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHISPYScript>();
            Server.Get.RoleManager.RegisterCustomRole<GardeSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFSergentScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFCapitaine>();
            Server.Get.RoleManager.RegisterCustomRole<NTFLieutenantColonel>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertReconfinementScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFInfirmierScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFVirologueScript>();
            Server.Get.RoleManager.RegisterCustomRole<FoundationUTRScript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP166AzScript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP008Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP1048cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP507Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP682Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP953Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP966cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP999Script>();
            Server.Get.RoleManager.RegisterCustomRole<StaffClassScript>();
            Server.Get.RoleManager.RegisterCustomRole<TechnicienScript>();
            Server.Get.RoleManager.RegisterCustomRole<TestClassScript>();
        }

    }
}
