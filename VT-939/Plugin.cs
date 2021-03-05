using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;

namespace VT939
{
    [PluginInformation(
Name = "VT-939",
Author = "VT",
Description = "Better939 for SynapseSl",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.0"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public float Scp939SeeingAhpAmount { get; internal set; }

        [Synapse.Api.Plugin.Config(section = "VT-939")]
        public static Config Config;
        
        /*
        private void PatchAll()
        {
            var instance = new Harmony("VT939");
            instance.PatchAll();
            Server.Get.Logger.Info("Custom class Harmony Patch done!");
        }
        */

        public override void Load()
        {
            Instance = this;
            base.Load();
            //PatchAll();
            new EventHandlers();
        }
    }
}
