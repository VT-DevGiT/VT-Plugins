using Synapse;
using Synapse.Api.Plugin;
using Synapse.Config;
using System;
using UnityEngine;

namespace VTDevHelp
{
    [PluginInformation(
Name = "VT-DevTool",
Author = "VT",
Description = "Dev Tool for help Dev",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v0.1.12"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        public static SerializedMapPoint DoorPosition;
        public static Quaternion DoorRotation;
        public override void Load()
        {
            Instance = this;
            base.Load();
        }
    }
}
