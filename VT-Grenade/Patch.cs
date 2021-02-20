using Grenades;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using Synapse;

namespace VTGrenad
{

    [HarmonyPatch(typeof(Grenade), nameof(Grenade.OnCollisionEnter))]
    internal static class FlashGrenadePatch
    {
        private static void Prefix(Grenade __instance)
        {
            foreach(var player in Server.Get.Players)
            {
                player.SendBroadcast(2, "FlashGrenadePatch");
            }
            Synapse.Api.Logger.Get.Info("FlashGrenadePatch");
            if (!Plugin.Config.FlashbangFuseWithCollision) return;
            if (__instance is FlashGrenade)
                __instance.NetworkfuseTime = 0;
        }
    }

    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ChangeIntoGrenade))]
    internal static class FragGrenadeChainPatch
    {
        private static bool Prepare()
        {
            Synapse.Api.Logger.Get.Info("FragGrenadeChainPatch");
            return true;
        }
        private static bool Prefix(FragGrenade __instance, Pickup item, ref bool __result)
        {
            foreach (var player in Server.Get.Players)
            {
                player.SendBroadcast(2, "FragGrenadeChainPatch");
            }
            Synapse.Api.Logger.Get.Info("FragGrenadeChainPatch");
            if (!Plugin.Config.ChaineFuseFragGrenad) return true;
            Plugin.SpawnGrenade(item.position);
            item.Delete();
            __result = true;
            return false;
        }
    }
    

}
