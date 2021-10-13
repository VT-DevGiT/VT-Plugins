using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(ReferenceHub), nameof(ReferenceHub.OnDestroy))]
    class DestroyHubPatch
    {
        [HarmonyPrefix]
        private static void HubDestroyPatch(ReferenceHub __instance)
        {
            try
            {
                var player = __instance.GetPlayer();

                if (player == null)
                    return;

                VTController.Server.Events.Player.InvokePlayerDestroyEvent(player);
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: DestroyHub failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
            }
        }
        
    }
}
