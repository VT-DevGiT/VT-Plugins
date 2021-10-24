using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using System;
using VT_Referance.Behaviour;
using VT_Referance.Method;

namespace VT_Referance.Patch.VT_Patch
{

    [HarmonyPatch(typeof(Player), "CustomRole", MethodType.Setter)]
    static class SynapseSetClassIdPatch1
    {

        [HarmonyPrefix]
        private static bool CustomRolePatch(Player __instance, IRole value)
        {
            try
            {
                ShieldControler shield = __instance.GetOrAddComponent<ShieldControler>();
                shield.ShieldLock = false;
                shield.MaxShield = 100;
                shield.Shield = 0;
                return true;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Patch: CustomRole failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(Player), "RoleType", MethodType.Setter)]
    static class SynapseSetClassIdPatch2
    {

        [HarmonyPrefix]
        private static bool RoleTypePatch(Player __instance, RoleType value)
        {
            try
            {
                if (__instance.CustomRole == null)
                {
                    ShieldControler shield = __instance.GetOrAddComponent<ShieldControler>();
                    shield.ShieldLock = false;
                    shield.MaxShield = 100;
                    shield.Shield = 0;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Patch : RoleType failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
