using CustomClass.Config;
using CustomClass.PlayerScript;
using Synapse;
using Synapse.Api.Plugin;

namespace CustomClass
{
    [PluginInformation(
        Author = "VT",
        Description = "A test plugi to check custom script",
        LoadPriority = 1,
        Name = "CustomClass201",
        SynapseMajor = 2,
        SynapseMinor = 4,
        SynapsePatch = 1,
        Version = "v.1.0.0")]
    public class PluginClass : AbstractPlugin
    {
        public static PluginClass Plugin;

        [Synapse.Api.Plugin.Config(section = "CustomClass201")]
        public static Config201 Config201;

        [Synapse.Api.Plugin.Config(section = "ConfigConcierge")]
        public static ConfigConcierge ConfigConcierge;

        [Synapse.Api.Plugin.Config(section = "ConfigDirecteurSite")]
        public static ConfigDirecteurSite ConfigDirecteurSite;

        [Synapse.Api.Plugin.Config(section = "ConfigScientifiqueSuperviseur")]
        public static ConfigScientifiqueSuperviseur ConfigScientifiqueSuperviseur;

        [Synapse.Api.Plugin.Config(section = "ConfigGardeSuperviseur")]
        public static ConfigGardeSuperviseur ConfigGardeSuperviseur;

        [Synapse.Api.Plugin.Config(section = "ConfigNTFExpertPyrotechnie")]
        public static ConfigNTFExpertPyrotechnie ConfigNTFExpertPyrotechnie;

        [Synapse.Api.Plugin.Config(section = "ConfigNTFExpertReconfinement")]
        public static ConfigNTFExpertReconfinement ConfigNTFExpertReconfinement;

        [Synapse.Api.Plugin.Config(section = "ConfigNTFInfirmier")]
        public static ConfigNTFInfirmier ConfigNTFInfirmier;

        [Synapse.Api.Plugin.Config(section = "ConfigCHIExpertPyrotechnie")]
        public static ConfigCHIExpertPyrotechnie ConfigCHIExpertPyrotechnie;

        [Synapse.Api.Plugin.Config(section = "ConfigCHIHacker")]
        public static ConfigCHIHacker ConfigCHIHacker;

        [Synapse.Api.Plugin.Config(section = "ConfigCHIIntrus")]
        public static ConfigCHIIntrus ConfigCHIntrus;

        [Synapse.Api.Plugin.Config(section = "ConfigCHIKamikaze")]
        public static ConfigCHIKamikaze ConfigCHIKamikaze;

        [Synapse.Api.Plugin.Config(section = "ConfigCHILeader")]
        public static ConfigCHILeader ConfigCHILeader;

        [Synapse.Api.Plugin.Config(section = "ConfigCHIMastondonte")]
        public static ConfigCHIMastodonte ConfigCHIMastondonte;

        [Synapse.Api.Plugin.Config(section = "ConfigCHISPY")]
        public static ConfigCHISpy ConfigCHISPY;

        [Synapse.Api.Plugin.Config(section = "ConfigNTFVirologue")]
        public static ConfigNTFVirologue ConfigNTFVirologue;

        [Synapse.Api.Plugin.Config(section = "ConfigUniteTactiqueRobotique")]
        public static ConfigRoboticTacticalUnity ConfigRoboticTaticalUnity;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP008")]
        public static ConfigSCP008 ConfigSCP008;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP1048")]
        public static ConfigSCP1048 ConfigSCP1048;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP507")]
        public static ConfigSCP507 ConfigSCP507;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP682")]
        public static ConfigSCP682 ConfigSCP682;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP966")]
        public static ConfigSCP966 ConfigSCP966;

        [Synapse.Api.Plugin.Config(section = "ConfigSCP999")]
        public static ConfigSCP999 ConfigSCP999;

        [Synapse.Api.Plugin.Config(section = "ConfigTechnicien")]
        public static ConfigTechnicien ConfigTechnicien;

        [Synapse.Api.Plugin.Config(section = "ConfigStaff")]
        public static ConfigStaff ConfigStaff;

        [Synapse.Api.Plugin.Config(section = "ConfigTestClass")]
        public static ConfigTestClass ConfigTestClass;

        public override void Load()
        {
            Plugin = this;
            RegisterCustomRole();

            new EventHandlers();
        }

        public void RegisterCustomRole()
        {
            Server.Get.RoleManager.RegisterCustomRole<ConciergeScript>();
            Server.Get.RoleManager.RegisterCustomRole<DirecteurSiteScript>();
            Server.Get.RoleManager.RegisterCustomRole<ScientifiqueSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICHackerScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICIntrusScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICKamikazeScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICLeaderScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICMastodonteScript>();
            Server.Get.RoleManager.RegisterCustomRole<ICSPYScript>();
            Server.Get.RoleManager.RegisterCustomRole<GardeSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertReconfinementScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFInfirmierScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFVirologueScript>();
            Server.Get.RoleManager.RegisterCustomRole<RoboticTacticalUnityScript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP008Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP1048cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP507Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP682Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP966cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP999Script>();
            Server.Get.RoleManager.RegisterCustomRole<StaffClassScript>();
            Server.Get.RoleManager.RegisterCustomRole<TechnicienScript>();
            Server.Get.RoleManager.RegisterCustomRole<TestClassScript>();
        }

    }
}
