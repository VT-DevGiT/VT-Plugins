using HarmonyLib;
using MapGeneration.Distributors;
using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Locker), nameof(Locker.ServerInteract))]

    class UseLockerPatch
    {
        [HarmonyPrefix]
        private static bool UseLocker(Locker __instance, ReferenceHub ply, byte colliderId)
        {
            try
            {
                if (colliderId >= __instance.Chambers.Length || !__instance.Chambers[colliderId].CanInteract)
                    return false;
                bool flag = !__instance.CheckPerms(__instance.Chambers[colliderId].RequiredPermissions, ply) && !ply.serverRoles.BypassMode;
                VTController.Server.Events.Map.InvokeLockerIneractEvent(__instance.GetPlayer(), __instance, ref flag);

                if (flag)
                {
                    __instance.Chambers[colliderId].SetDoor(!__instance.Chambers[colliderId].IsOpen, __instance._grantedBeep);
                    __instance.RefreshOpenedSyncvar();
                }
                else  __instance.RpcPlayDenied(colliderId);
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: UseLocker failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
