using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;


namespace VT_AndersonRobotic
{
    [PluginInformation(
Name = "VT-AndersonRobotic",
Author = "VT",
Description = "Adds the team AndersonRobotic",
LoadPriority = 20,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.3"
       )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-AndersonRoboticPlugin")]
        public static Config Config;

        [Synapse.Api.Plugin.Config(section = "VT-AndersonRoboticIngenier")]
        public static ConfigAndersonEngineer ConfigAndersonEngineer;

        [Synapse.Api.Plugin.Config(section = "VT-AndersonRoboticLeader")]
        public static ConfigAndersonLeader ConfigAndersonLeader;

        [Synapse.Api.Plugin.Config(section = "VT-AsimovGardien")]
        public static ConfigGardienGammaOne ConfigGardienGammaOne;

        [Synapse.Api.Plugin.Config(section = "VT-AsimovGeneral")]
        public static ConfigGeneralGammaOne ConfigGeneralGammaOne;

        internal int AndersonSapwn = 0;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Instance = this;
            Server.Get.TeamManager.RegisterTeam<AndersonRoboticTeam>();
            Server.Get.RoleManager.RegisterCustomRole<AndersonRoboticLeaderScript>();
            Server.Get.RoleManager.RegisterCustomRole<AndersonRoboticEngineerScript>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation()
            {
                SpawnMessage = "tu es un membre de <color=yellow>AndersonRobotic</color>\\nTon objectif est de volée toute les donnée des serveurs!\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            new EventHandlers();
            base.Load();
        }
    }
}
