using HarmonyLib;
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

                LockerManager singleton = LockerManager.singleton;

                if (lockerId >= singleton.lockers.Length)
                    return false;

                if (!__instance.ChckDis(singleton.lockers[lockerId].gameObject.position) ||
                    !singleton.lockers[lockerId].supportsStandarizedAnimation)
                    return false;

                if (chamberNumber >= singleton.lockers[lockerId].chambers.Length)
                    return false;

                if (singleton.lockers[lockerId].chambers[chamberNumber].doorAnimator == null)
                    return false;

                if (!singleton.lockers[lockerId].chambers[chamberNumber].CooldownAtZero())
                    return false;

                singleton.lockers[lockerId].chambers[chamberNumber].SetCooldown();

                string accessToken = singleton.lockers[lockerId].chambers[chamberNumber].accessToken;
                var itemById = __instance._inv.GetItemByID(__instance._inv.curItem);

                bool flag = true;
                if ((__instance._hc.CufferId > 0 && !PlayerInteract.CanDisarmedInteract) || string.IsNullOrEmpty(accessToken) || (itemById != null && itemById.permissions.Contains(accessToken)) || __instance._sr.BypassMode)
                    flag = false;

                VTController.Server.Events.Map.InvokeLockerIneractEvent(__instance.GetPlayer(), singleton.lockers[lockerId], ref flag);

                if (flag)
                {
                    bool _flag = (singleton.openLockers[lockerId] & 1 << chamberNumber) != 1 << chamberNumber;
                    singleton.ModifyOpen(lockerId, chamberNumber, _flag);
                    singleton.RpcDoSound(lockerId, chamberNumber, _flag);
                    bool anyOpen = true;
                    for (int i = 0; i < singleton.lockers[lockerId].chambers.Length; i++)
                    {
                        if ((singleton.openLockers[lockerId] & 1 << i) == 1 << i)
                        {
                            anyOpen = false;
                            break;
                        }
                    }

                    singleton.lockers[lockerId].LockPickups(!_flag, chamberNumber, anyOpen);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        singleton.RpcChangeMaterial(lockerId, chamberNumber, false);
                    }
                }
                else
                {
                    singleton.RpcChangeMaterial(lockerId, chamberNumber, true);
                }

                __instance.OnInteract();

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
