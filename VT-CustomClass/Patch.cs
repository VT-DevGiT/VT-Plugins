using HarmonyLib;

namespace VTCustomClass
{
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.AntiCheatKillPlayer))]
    internal static class killAntiCheatPatch
    {
        private static bool Prefix()
        {
            if (Plugin.ConfigCustomClass.killAntiCheatPatch)
                return false;
            
            return true;
        }
    }
}
