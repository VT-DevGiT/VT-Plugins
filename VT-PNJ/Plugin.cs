using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VT_PNJ
{
    [PluginInformation(
Name = "VT-PNJ",
Author = "VT",
Description = "Add new NPC script",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.0.0.1"
)]
    public class Plugin : VtAbstractPlugin<EventHandlers, Config>
    {
        public override bool AutoRegister => false;
    }
}
