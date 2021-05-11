using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace VT_Alpha
{

    [PluginInformation(
Author = "VT",
Description = "Add the Alpha team",
LoadPriority = 20,
Name = "VT-Alpha",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.0"
)]
    public class Plugin : AbstractPlugin
    {

        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-Alpha-1")]
        public static Config Config;

        [Synapse.Api.Plugin.Config(section = "VT-Alpha-1 Agent")]
        public static ConfigAlphaOne ConfigAlphaOneAgent;

        internal int AphaOne;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Server.Get.TeamManager.RegisterTeam<AlphaOneTeam>();
            Server.Get.RoleManager.RegisterCustomRole<AlphaOneAgent>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation()
            {
                SpawnMessage = "tu es un %RoleName%\\nTon objectif est de rétablire l'odre au nom des O5\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
