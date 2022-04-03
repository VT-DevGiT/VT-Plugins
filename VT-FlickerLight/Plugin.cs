using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Core.Plugin;

namespace VT_FlickerLight
{
    [PluginInformation(
        Name = "VT-FlickerLight",
        Author = "Antoniofo",
        Description = "Flicker the light at the start of the game",
        LoadPriority = 0,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.0.1"
        )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandler, Config>
    {
        public override bool AutoRegister => false;

        public override void Load()
        {
            base.Load();
        }
    }
}
