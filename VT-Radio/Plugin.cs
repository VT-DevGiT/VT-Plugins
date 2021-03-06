using Synapse.Api.Plugin;

namespace VTRadio
{
    [PluginInformation(
Name = "VT-Radio",
Author = "VT",
Description = "Better Radio",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.0"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-079")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            new EventHandlers();
        }

    }

}
