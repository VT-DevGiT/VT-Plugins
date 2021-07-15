using HarmonyLib;
using Synapse;
using Synapse.Api;
using System;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Player), "RoleID", MethodType.Setter)]
    class SynapseSetClassIdPatch
    {
        private static bool Prefix(Player __instance, int value)
        {
            try
            {
                Server.Get.Logger.Info("Log RoleID set");
                bool flag = true;
                VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, ref value, ref flag);
                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: PlayerDamagePost failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
