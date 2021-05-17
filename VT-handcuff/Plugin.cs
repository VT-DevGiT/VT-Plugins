using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace VThandcuff
{
    [PluginInformation(
Author = "VT",
Description = "Better handcuff",
LoadPriority = 0,
Name = "VT-handcuff",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.1"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-handcuff")]
        public static Config Config;

        private List<Player> _CuffedPlayer = new List<Player>();

        public List<Player> CuffedPlayer
        {
            get { return CuffedPlayer; }
            set { CuffedPlayer = value; }
        }


        private void PatchAll()
        {
            var instance = new Harmony("VTProget_X");
            instance.PatchAll();
            Server.Get.Logger.Info("VT-handcuff Harmony Patch done!");
        }

        public override void Load()
        {
            Instance = this;
            PatchAll();
            base.Load();
            new EventHandlers();
        }
    }
}