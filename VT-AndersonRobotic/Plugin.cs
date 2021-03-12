using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;


namespace VT_AndersonRobotic
{
    [PluginInformation(
Name = "VT-AndersonRobotic",
Author = "VT",
Description = "Adds functionality such as intercom information",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.1"
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
            Server.Get.TeamManager.RegisterTeam<AndersonRoboticTeam>();
            Server.Get.RoleManager.RegisterCustomRole<AndersonRoboticUnite>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation()
            {
                SpawnMessage = "<i>tu es un membre de <color=yellow>AndersonRobotic</color></i>\\n<i>Ton objectif et de volée toute les donnée des serveurs!</i>\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            Instance = this;
            new EventHandlers();
            base.Load();
        }
    }
}
