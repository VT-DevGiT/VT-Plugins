using HarmonyLib;

namespace VTCustomClass
{
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AntiCheatKillPlayer))]
    internal static class killAntiCheatPatch
    {
        [HarmonyPrefix]
        private static bool PatchKillPlayer(PlayerMovementSync __instance, string message, string code)
        {
            if (Plugin.ConfigCustomClass.killAntiCheatPatch)
                return false;
            
            return true;
        }
    }
}
