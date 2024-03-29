﻿using Synapse.Api.Plugin;

namespace VTRadio
{
    [PluginInformation(
Name = "VT-Radio",
Author = "VT",
Description = "Better Radio",
LoadPriority = 5,
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
            new EventHandlers();
        }
    }
}
