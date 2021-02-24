using Grenades;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Mirror;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using UnityEngine;

namespace VTGrenad
{

    [HarmonyPatch(typeof(Grenade), nameof(Grenade.OnCollisionEnter))]
    internal static class FlashGrenadePatch
    {
        private static void Prefix(Grenade __instance)
        {
            if (!Plugin.Config.FlashbangFuseWithCollision) return;
            if (__instance is FlashGrenade)
                __instance.NetworkfuseTime = 0;
        }
    }

    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ChangeIntoGrenade))]
    internal static class FragGrenadeChainPatch
    {
        private static bool Prefix(FragGrenade __instance, Pickup item, ref bool __result)
        {
            if (Plugin.Config.ChaineFuseFragGrenad)
            {
                SynapseItem LeTruc = item.GetSynapseItem();
                if (LeTruc.ItemType != ItemType.GrenadeFrag)
                {
                    __result = false;
                    return false;
                }
                Plugin.SpawnGrenade(item.position);
                item.Delete();
                __result = true;
                return false;
            }
            else return true;
        }
    }
    
    [HarmonyPatch(typeof(FlashGrenade), nameof(FlashGrenade.ServersideExplosion))]
    internal static class FlashEffect
    {
        private static void Postfix(FlashGrenade __instance)
        {
            foreach (var joueur in Server.Get.Players)
            {
                GameObject player = joueur.gameObject;
                Vector3 position = __instance.transform.position;
                ReferenceHub hub = ReferenceHub.GetHub(player);

                if (__instance._friendlyFlash)
                {
                    float num = __instance.powerOverDistance.Evaluate(Vector3.Distance(player.transform.position, position) / ((double)position.y > 900.0 ? __instance.distanceMultiplierSurface : __instance.distanceMultiplierFacility)) * __instance.powerOverDot.Evaluate(Vector3.Dot(hub.PlayerCameraReference.forward, (hub.PlayerCameraReference.position - position).normalized));
                    byte intensity = (byte)Mathf.Clamp(Mathf.RoundToInt(num * 10f * __instance.maximumDuration), 1, (int)byte.MaxValue);
                    if ((double)num > 0.0)
                    {
                        joueur.GiveEffect(Effect.Deafened, 1, 7);
                        joueur.GiveEffect(Effect.Exhausted, 1, 7);
                        Timing.CallDelayed(7f, () =>
                        {
                            joueur.GiveEffect(Effect.Deafened, 1, 0);
                            joueur.GiveEffect(Effect.Exhausted, 1, 0);
                        });
                    }
                }
            }
        }
    }
    
}
