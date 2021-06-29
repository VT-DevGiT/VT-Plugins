using HarmonyLib;
using Synapse;
using System;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.CallCmdSwitchAWButton))]
    class ActivatingWarheadPanelPatch
    {
        private static bool Prefix(PlayerInteract __instance)
        {
            try
            {
                if (!__instance._playerInteractRateLimit.CanExecute() || (__instance._hc.CufferId > 0 && !PlayerInteract.CanDisarmedInteract))
                    return false;
                GameObject gameObject = GameObject.Find("OutsitePanelScript");
                if (!__instance.ChckDis(gameObject.transform.position))
                    return false;

                bool flag = true;

                Item item = __instance._inv.GetItemByID(__instance._inv.curItem);
                if (!__instance._sr.BypassMode && (item == null || !item.permissions.Contains("CONT_LVL_3")))
                    flag = false;

                VTController.Server.Events.Map.InvokeActivatWarheadPanelEvent(__instance.GetPlayer(), __instance.GetPlayer().ItemInHand, ref flag);

                if (flag)
                {
                    gameObject.GetComponentInParent<AlphaWarheadOutsitePanel>().NetworkkeycardEntered = true;
                    __instance.OnInteract();
                }
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
