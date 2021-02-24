using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClass
{
    
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AnticheatIsIntersecting))]
    internal static class AntiCheatPatchSync
    {
        private static bool Prefix(PlayerMovementSync __instance, ref bool __result)
        {
            __result = false;
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AntiCheatKillPlayer))]
    internal static class AntiCheatPatchKill
    {
        private static bool Prefix()
        {
            return false;
        }
    }
     [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AnticheatRaycast))]
       internal static class AntiCheatPatchRayCast
       {
           private static bool Prefix(ref bool __result)
           {
               __result = true;
               return false;
           }
       } 
       [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.CheckAnticheatSafe))]
       internal static class AntiCheatPatchSafe
       {
           private static bool Prefix(ref bool __result)
           {
               __result = true;
               return false;
           }
       }
}
