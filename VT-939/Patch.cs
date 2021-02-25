using Assets._Scripts.Dissonance;
using HarmonyLib;
using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT939
{
    
    /*
    [HarmonyPatch(typeof(DissonanceUserSetup), nameof(DissonanceUserSetup.CallCmdAltIsActive))]
    internal static class SCP939SeeVoice1
    {
        private static void Postfix(DissonanceUserSetup __instance)
        {
            var player = __instance.GetPlayer();
            if (__instance.IsTransmittingOnAny())
                Server.Get.Logger.Info($"IsTransmittingOnAny {player}"); 
            if (__instance.NetworkaltIsActive)
                Server.Get.Logger.Info($"NetworkaltIsActive {player}");
            if (__instance.altIsActive)
                Server.Get.Logger.Info($"altIsActive {player}");
        }
    }
    [HarmonyPatch(typeof(MyMicrophoneIndicator), nameof(MyMicrophoneIndicator.Update))]
    internal static class SCP939SeeVoice3
    {
        private static List<string> PatchedPlayer = new List<string>();
        private static void Prefix(MyMicrophoneIndicator __instance)
        {
            Server.Get.Logger.Info($"OnStartedSpeaking Patched");
            var comms = __instance.dissonanceSetup;
            foreach(var player in  comms.Players)
            {
                if (!PatchedPlayer.Contains( player.Name))
                {
                    PatchedPlayer.Add(player.Name);
                    player.OnStartedSpeaking += pl =>
                    {
                        Server.Get.Logger.Info($"OnStartedSpeaking {pl.Name}");
                    };
                }
            }
        }
    }
    
    [HarmonyPatch(typeof(DissonanceUserSetup), nameof(DissonanceUserSetup.EnableSpeaking))]
    internal static class SCP939SeeVoice2
    {
        private static void Postfix(DissonanceUserSetup __instance)
        {
            var player = __instance.GetPlayer();
            Server.Get.Logger.Info($"EnableSpeaking {player}");
        }
    }
    */


}
