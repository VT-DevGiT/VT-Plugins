using Synapse.Api.Plugin;

namespace VT079
{
    [PluginInformation(
Name = "DevTool",
Author = "VT",
Description = "DevTool",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.0"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
        }

    }

}
