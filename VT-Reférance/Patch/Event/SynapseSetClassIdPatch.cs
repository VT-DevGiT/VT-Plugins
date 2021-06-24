using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Roles;
using System;
using System.Reflection;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{

    [HarmonyPatch(typeof(Player), "set_RoleID")]
    class SynapseSetClassIdPatch
    {
        private static bool Prefix(Player __instance, int value)
        {
            try
            {
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
