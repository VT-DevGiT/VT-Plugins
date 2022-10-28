using CustomPlayerEffects;
using HarmonyLib;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTDevHelp.paths
{
    class EffectName
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Scp207), nameof(Scp207.GetSpectatorText))]
        private static bool GetSpectatorText(Scp207 __instance, ref bool __result, out string s)
        {
            Logger.Get.Info("GetSpectatorText");
            __result = true;
            s = "SUS";
            return false;
        }
    }
}
