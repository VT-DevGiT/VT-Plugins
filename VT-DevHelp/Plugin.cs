using Synapse.Api.Plugin;
using Synapse.Config;
using UnityEngine;
using VT_Api.Core.Plugin;

namespace VTDevHelp
{
    [PluginInformation(
    Name = "VT-DevTool",
    Author = "VT/Oka",
    Description = "Dev Tool for help Dev (or just a funny plugin for moderator)",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v2.3.5"
    )]
    public class Plugin : VtAbstractPlugin<EventHandlers>
    {
        public override bool AutoRegister => true;

        public static SerializedMapPoint DoorPosition;
        public static Quaternion DoorRotation;
    }
}
