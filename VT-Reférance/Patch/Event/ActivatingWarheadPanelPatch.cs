using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;
using Synapse;
using System;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.UserCode_CmdSwitchAWButton))]
    class ActivatingWarheadPanelPatch
    {
        [HarmonyPrefix]
        private static bool NukeLeverPatch(PlayerInteract __instance)
        {
            try
            {
                if (!__instance.CanInteract)
                    return false;
                GameObject gameObject = GameObject.Find("OutsitePanelScript");
                if (!__instance.ChckDis(gameObject.transform.position))
                    return false;

                bool flag = true;

                if (!__instance.ChckDis(gameObject.transform.position) || !__instance._sr.BypassMode && (!(__instance._inv.CurInstance is KeycardItem curInstance) || !curInstance.Permissions.HasFlag((Enum) KeycardPermissions.AlphaWarhead)))
                    flag = false;

                VTController.Server.Events.Map.InvokeActivatWarheadPanelEvent(__instance.GetPlayer(), ref flag);

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
