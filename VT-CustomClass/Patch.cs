using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass
{

    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AnticheatIsIntersecting))]
    internal static class killAntiCheatPatchSync
    {
        private static bool Prefix(PlayerMovementSync __instance, ref bool __result)
        {
            if (PluginClass.ConfigCustomClass.killAntiCheatPatchSync)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AntiCheatKillPlayer))]
    internal static class killAntiCheatPatch
    {
        private static bool Prefix()
        {
            if (PluginClass.ConfigCustomClass.killAntiCheatPatch)
            {
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AnticheatRaycast))]
    internal static class killAntiCheatPatchRayCast
    {
        private static bool Prefix(ref bool __result)
        {
            if (PluginClass.ConfigCustomClass.killAntiCheatPatchRayCast)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.CheckAnticheatSafe))]
    internal static class killAntiCheatPatchSafe
    {
        private static bool Prefix(ref bool __result)
        {
            if (PluginClass.ConfigCustomClass.killAntiCheatPatchSafe)
            {
                __result = true;
                return false;
            }
            return true;
        }        
    }
}
