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

        [Synapse.Api.Plugin.Config(section = "VT-AlphaOne")]
        public static Config Config;

        [Synapse.Api.Plugin.Config(section = "VT-AlphaOneAgent")]
        public static ConfigAlphaOne ConfigAlphaOneAgent;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Server.Get.TeamManager.RegisterTeam<AlphaOneTeam>();
            Server.Get.RoleManager.RegisterCustomRole<U2IAgent>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation()
            {
                SpawnMessage = "tu es un membre de <color=blue>U2I %RoleName%</color>\\nTon objectif est de stoper toute les intrus et tuée les SCP\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
