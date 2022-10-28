using HarmonyLib;
using Synapse;
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
    Version = "v2.4.0"
    )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers>
    {
        public static SerializedMapPoint DoorPosition { get; internal set; }
        public static Quaternion DoorRotation { get; internal set; }

        public override bool AutoRegister => true;

        public override void Load()
        {
            PatchAll();
            //Synapse.Api.Teams.TeamManager.Get.RegisterTeam<TestTeam>();

            //AudioApi.AudioApi.Enable();
            base.Load();
        }

        private void PatchAll()
        {
            var instance = new Harmony("VTTest");
            instance.PatchAll();
            Server.Get.Logger.Info("Harmony Patch done!");
        }
    }
}
