using HarmonyLib;
using Synapse;
using System;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Intercom), nameof(Intercom.CallCmdSetTransmit))]

    class IntercomSpeakPatch
    {
        private static bool Prefix(Intercom __instance, bool player)
        {
            try
            {
                bool flag = true;
                if (!__instance._interactRateLimit.CanExecute(true) || Intercom.AdminSpeaking ||
                    (player && !__instance.ServerAllowToSpeak()) || Intercom.host.Networkspeaker != __instance.gameObject)
                    return false;

                Synapse.Api.Player Player = player ? __instance.GetPlayer() : null;
                VTController.Server.Events.Player.InvokePlayerSpeakIntercomEvent(Player, ref flag);

                if (flag) Intercom.host.RequestTransmission(__instance.gameObject);
                return false;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Vt-Event: ActivatingWarheadPanel failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
