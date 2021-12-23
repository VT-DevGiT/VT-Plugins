using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using System;
using VT_Referance.Behaviour;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{

    [HarmonyPatch(typeof(Player), "CustomRole", MethodType.Setter)]
    static class SynapseSetClassIdPatch1
    {

        [HarmonyPrefix]
        private static bool CustomRolePatch(Player __instance, IRole value)
        {
            try
            {
                if (__instance == null) Server.Get.Logger.Info("Vt-Patch : CustomRole\n__instance == null");
                if (value != null)
                    VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, value.GetRoleID());
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
                if (__instance == null) Server.Get.Logger.Error("Vt-Patch : RoleType\n__instance == null");
                if (__instance.CustomRole == null)
                    VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, (int)value);

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
