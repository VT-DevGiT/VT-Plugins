using HarmonyLib;
using Synapse;
using System;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Intercom), nameof(Intercom.CmdSetTransmit))]

    class IntercomSpeakPatch
    {
        [HarmonyPrefix]
        private static bool IntercomStartTransmit(Intercom __instance, bool player)
        {
            try
            {
                if (!__instance._interactRateLimit.CanExecute() || Intercom.AdminSpeaking)
                    return false;
                if (player)
                {
                    if (!__instance.ServerAllowToSpeak())
                        return false;

                    bool flag = true;
                    Synapse.Api.Player Player = __instance.GetPlayer();
                    VTController.Server.Events.Player.InvokePlayerSpeakIntercomEvent(Player, ref flag);
                    if (flag) Intercom.host.RequestTransmission(__instance.gameObject);
                }
                else
                {
                    if (!(Intercom.host.Networkspeaker == __instance.gameObject))
                        return false;
                    Intercom.host.RequestTransmission(null);
                }
                return false;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Vt-Event: IntercomSpeakEvent failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
