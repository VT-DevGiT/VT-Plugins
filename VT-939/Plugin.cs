using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VT939
{
    [PluginInformation(
Name = "VT-939",
Author = "VT",
Description = "Better939 for SynapseSl",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.3.0"
)]

    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config>
    {
        public override bool AutoRegister => false;
    }
}
