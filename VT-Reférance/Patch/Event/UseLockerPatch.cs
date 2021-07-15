using HarmonyLib;
using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.CallCmdUseLocker))]

    class UseLockerPatch
    {
        private static bool Prefix(PlayerInteract __instance, byte lockerId, byte chamberNumber)
        {
            try
            {
                if (!__instance._playerInteractRateLimit.CanExecute(true))
                    return false;
                if (__instance._hc.CufferId > 0 && !PlayerInteract.CanDisarmedInteract)
                    return false;

                LockerManager sing = LockerManager.singleton;

                if (lockerId >= sing.lockers.Length)
                    return false;

                if (!__instance.ChckDis(sing.lockers[lockerId].gameObject.position) ||
                    !sing.lockers[lockerId].supportsStandarizedAnimation)
                    return false;

                if (chamberNumber >= sing.lockers[lockerId].chambers.Length)
                    return false;

                if (sing.lockers[lockerId].chambers[chamberNumber].doorAnimator == null)
                    return false;

                if (!sing.lockers[lockerId].chambers[chamberNumber].CooldownAtZero())
                    return false;

                sing.lockers[lockerId].chambers[chamberNumber].SetCooldown();

                string accessToken = sing.lockers[lockerId].chambers[chamberNumber].accessToken;
                var itemById = __instance._inv.GetItemByID(__instance._inv.curItem);

                bool flag = string.IsNullOrEmpty(accessToken) || (itemById != null && itemById.permissions.Contains(accessToken)) || __instance._sr.BypassMode;
                VTController.Server.Events.Map.InvokeLockerIneractEvent(__instance.GetPlayer(), sing.lockers[lockerId], ref flag);
                if (flag)
                {
                    bool _flag = (sing.openLockers[lockerId] & 1 << chamberNumber) != 1 << chamberNumber;
                    sing.ModifyOpen(lockerId, chamberNumber, _flag);
                    sing.RpcDoSound(lockerId, chamberNumber, _flag);
                    bool anyOpen = true;
                    for (int i = 0; i < sing.lockers[lockerId].chambers.Length; i++)
                    {
                        if ((sing.openLockers[lockerId] & 1 << i) == 1 << i)
                        {
                            anyOpen = false;
                            break;
                        }
                    }
                    sing.lockers[lockerId].LockPickups(!_flag, chamberNumber, anyOpen);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        sing.RpcChangeMaterial(lockerId, chamberNumber, false);
                    }
                    __instance.OnInteract();
                }
                else
                {
                    sing.RpcChangeMaterial(lockerId, chamberNumber, true);
                }
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
