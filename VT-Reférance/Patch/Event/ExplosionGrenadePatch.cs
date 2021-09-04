using HarmonyLib;
using Interactables;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using NorthwoodLib.Pools;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(ExplosionGrenade), nameof(ExplosionGrenade.PlayExplosionEffects))]
    class FragExplosionGrenadePatch
    {
        internal static TimeGrenade grenade;

        [HarmonyPrefix]
        private static bool GrenadeExplosion(ExplosionGrenade __instance)
        {
            try
            {
                bool falg = true;
                GrenadeType Type1;
                if (__instance.GetType() == typeof(Scp018Projectile))
                    Type1 = GrenadeType.Scp018;
                else if (__instance.GetType() == typeof(ExplosionGrenade))
                    Type1 = GrenadeType.Grenade;
                else
                    Type1 = (GrenadeType)4;

                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(__instance, Type1, ref falg);
                if (falg) grenade = __instance;
                return falg;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFragExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(FlashbangGrenade), nameof(FlashbangGrenade.PlayExplosionEffects))]
    class FlashExplosionGrenadePatch
    {
        [HarmonyPrefix]
        private static bool FlashExplosion(FlashbangGrenade __instance)
        {
            try
            {
                if (!NetworkServer.active)
                    return false;
                GrenadeType Type;
                if (__instance.GetType() == typeof(FlashbangGrenade))
                    Type = GrenadeType.Flashbang;
                else
                    Type = (GrenadeType)4;
                bool flag = true;
                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(__instance, Type, ref flag);
                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFlashExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
    
}
