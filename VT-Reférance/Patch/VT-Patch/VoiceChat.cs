using Dissonance;
using Dissonance.Audio.Capture;
using Dissonance.Integrations.MirrorIgnorance;
using Dissonance.Networking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_Referance.Patch.VT_Patch
{
    [HarmonyPatch(typeof(AudioSettings), nameof(AudioSettings.outputSampleRate), MethodType.Getter)]
    internal static class AudioSettingsOutputSamplerateFix
    {
        private static bool Prefix(ref int __result)
        {
            __result = 48000;
            return false;
        }
    }
    [HarmonyPatch(typeof(CapturePipelineManager), nameof(CapturePipelineManager.RestartTransmissionPipeline))]
    internal static class RestartTransmissionPipelineFix
    {
        [HarmonyPrefix]
        private static bool Patch(string reason) => reason != "Detected a frame skip, forcing capture pipeline reset";
    }
    [HarmonyPatch(typeof(BaseCommsNetwork<MirrorIgnoranceServer, MirrorIgnoranceClient, MirrorConn, Unit, Unit>), nameof(BaseCommsNetwork<MirrorIgnoranceServer, MirrorIgnoranceClient, MirrorConn, Unit, Unit>.RunAsDedicatedServer))]
    internal static class RunAsHostFix
    {
        [HarmonyPrefix]
        private static bool Patch(BaseCommsNetwork<MirrorIgnoranceServer, MirrorIgnoranceClient, MirrorConn, Unit, Unit> __instance)
        {
            __instance.RunAsHost(Unit.None, Unit.None);
            return false;
        }
    }
}
