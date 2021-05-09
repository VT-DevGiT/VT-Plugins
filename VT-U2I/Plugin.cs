using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace VT_U2I
{

    [PluginInformation(
Author = "VT",
Description = "Add the U2I team",
LoadPriority = 20,
Name = "VT-U2I",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.0"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-U2I")]
        public static Config Config;

        [Synapse.Api.Plugin.Config(section = "VT-U2ILiaisonAgent")]
        public static ConfigU2IAgentLiaison ConfigU2IAgentLiaison;

        [Synapse.Api.Plugin.Config(section = "VT-U2IAgent")]
        public static ConfigU2IAgent ConfigU2IAgent;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Server.Get.TeamManager.RegisterTeam<U2ITeam>();
            Server.Get.RoleManager.RegisterCustomRole<U2IAgent>();
            Server.Get.RoleManager.RegisterCustomRole<U2IAgentLiaison>();
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
