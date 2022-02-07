using HarmonyLib;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Synapse.Api.Enum;
using System;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(CollisionDetectionPickup), nameof(CollisionDetectionPickup.ProcessCollision))]
    class ColisionGrenadePatch
    {
        [HarmonyPrefix]
        private static bool CollisionPatch(CollisionDetectionPickup __instance, Collision collision)
        {
            try
            {
                if (!(__instance is TimeGrenade))
                    return true;

                GrenadeType Type;
                if (__instance.GetType() == typeof(ExplosionGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(FlashbangGrenade))
                    Type = GrenadeType.Flashbang;
                else if (__instance.GetType() == typeof(Scp018Projectile))
                    Type = GrenadeType.Scp018;
                else
                    Type = (GrenadeType)4;

                VTController.Server.Events.Grenade.InvokeCollisionGrenadeEvent((TimeGrenade)__instance, Type);
                return true;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeCollisionEnter failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
