using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;

namespace VTEscape
{

    [PluginInformation(
Author = "VT",
Description = "add a new Escape and config",
LoadPriority = 20,
Name = "VT-Escape",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.5.3"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-Escape")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
