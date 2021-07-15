
using HarmonyLib;
using Synapse.Api.Plugin;

namespace VTRpSCP
{
    [PluginInformation(
    Name = "VT-RpSCP",
    Author = "VT",
    Description = "for Rp SCP sl",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.0.0"
    )]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
            new EventHandlers();
        }

    }
}
