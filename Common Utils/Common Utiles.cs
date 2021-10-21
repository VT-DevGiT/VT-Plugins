using Synapse.Api.Plugin;

namespace Common_Utiles
{

    [PluginInformation(
Name = "Common Utiles",
Author = "Oka",
Description = "add new functionality and config",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.3.1"
)]
    public class CommonUtiles : AbstractPlugin
    {
        public static CommonUtiles Instance { get; private set; }
        public bool RespawnAllow { get; internal set; }

        [Synapse.Api.Plugin.Config(section = "Common Utiles")]
        public static Config Config;

        public static Serialized914Recipe[] Recipes;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}