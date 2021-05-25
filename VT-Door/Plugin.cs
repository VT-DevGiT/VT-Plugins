using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;

namespace VTDoor
{

    [PluginInformation(
Author = "VT",
Description = "add a new Door",
LoadPriority = 5,
Name = "VT-Door",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.0"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-Door")]
        public static Config Config;
        // 
        public override void Load()
        {
            Instance = this;
            PatchAll();
            base.Load();
            new EventHandlers();
        }

        private void PatchAll()
        {
            var instance = new Harmony("VTDoor");
            instance.PatchAll();
            Server.Get.Logger.Info("VT-Door Harmony Patch done!");
        }

    }
}
