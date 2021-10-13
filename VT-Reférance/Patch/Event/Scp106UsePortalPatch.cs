using HarmonyLib;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Scp106PlayerScript), nameof(Scp106PlayerScript.CmdUsePortal))]
    class Scp106UsePortalPatch
    {
        [HarmonyPrefix]
        private static bool Scp106HabilityPatch(Scp106PlayerScript __instance)
        {
            try
            {
                if (!__instance._interactRateLimit.CanExecute())
                    return false;
                if (!__instance._hub.playerMovementSync.Grounded || (!__instance.iAm106 || !(__instance.portalPosition != Vector3.zero)) || __instance.goingViaThePortal)
                    return false;
                bool flag = true;
                VTController.Server.Events.Scp.Scp106.InvokePortalUseEvent(__instance.GetPlayer(), ref flag);
                if (flag)
                    Timing.RunCoroutine(__instance._DoTeleportAnimation(), Segment.Update);
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: UsePortal failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
        
    }
}
