using HarmonyLib;
using InventorySystem.Items.Radio;
using VTCustomClass.PlayerScript;

namespace VTCustomClass
{
    [HarmonyPriority(Priority.High)]
    [HarmonyPatch(typeof(RadioItem), nameof(RadioItem.Update))]
    internal static class InfiniteRadio
    {
        static bool Prefix(RadioItem __instance)
        {
            var player = __instance?.OwnerInventory?.GetPlayer();
            if (player?.CustomRole is BaseUTRScript)
            {
                __instance.SendStatusMessage();
                return false;
            }
            return true;
        }
    }
}
