using Synapse.Api.Plugin;
using Synapse.Config;
using Synapse.Api;
using UnityEngine;
using Synapse;

namespace VTDevHelp
{
    [PluginInformation(
Name = "VT-DevTool",
Author = "VT/Oka",
Description = "Dev Tool for help Dev (or fun for mod)",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v1.2.3"
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
            ItemLoader();
            new EventHandlers();
        }

        public void ItemLoader()
        {
            new DestructeurDeGameObject();
            new DésincronisateurD_Object();
            new TerminatorDeGameObject();
        }
    }
}
