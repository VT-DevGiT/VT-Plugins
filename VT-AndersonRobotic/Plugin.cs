using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;


namespace VT_AndersonRobotic
{
    [PluginInformation(
Name = "VT-AndersonRobotic",
Author = "VT",
Description = "Adds the team AndersonRobotic",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.3"
       )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-AndersonRobotic")]
        public static Config Config;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Instance = this;
            Server.Get.TeamManager.RegisterTeam<AndersonRoboticTeam>();
            Server.Get.RoleManager.RegisterCustomRole<AndersonRoboticUnite>();
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
