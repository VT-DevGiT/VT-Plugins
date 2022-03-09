using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VTMap
{

    [PluginInformation(
    Author = "VT",
    Description = "Add new component one the Map !",
    LoadPriority = 5,
    Name = "VT-Map",
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.0.3"
    )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config>
    {
        public override bool AutoRegister => false;

    }
}
