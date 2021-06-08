using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using VT_Referance.NpcScript;

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

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-PNJ")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            new EventHandlers();
        }

    }

}
