using Grenades;
using HarmonyLib;
using Synapse;

namespace VTGrenad
{

    [HarmonyPatch(typeof(Grenade), nameof(Grenade.OnCollisionEnter))]
    public static class FlashGrenadePatch
    {
        public static void Prefix(Grenade __instance)
        {
            Server.Get.Logger.Info("FlashGrenadePatch");
            if (!Plugin.Config.FlashbangFuseWithCollision) return;
            if (__instance is FlashGrenade)
                __instance.NetworkfuseTime -= __instance.fuseDuration;
        }
    }

    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ChangeIntoGrenade))]
    public static class FragGrenadeChainPatch
    {
        public static bool Prefix(FragGrenade __instance, Pickup item, ref bool __result)
        {
            Server.Get.Logger.Info("FragGrenadeChainPatch");
            if (!Plugin.Config.ChaineFuseFragGrenad) return true;
            Plugin.SpawnGrenade(item.position);
            item.Delete();
            __result = true;
            return false;
        }
    }

}
