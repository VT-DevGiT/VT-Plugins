using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.CancelDetonation), new Type[] { typeof(GameObject) })]
    internal static class Stopping
    {
        private static bool Prefix(AlphaWarheadController __instance, GameObject disabler)
        {
            try
            {
                if (!__instance.inProgress)
                    return false;
                if (disabler != null)
                { 
                    bool flag = true;
                    if (__instance.timeToDetonation <= 10.0 || __instance._isLocked)
                        flag = false;

                    VTController.Server.Events.Map.InvokeWarheadStopEvent(disabler.GetPlayer(), ref flag);
                    if (!flag)
                        return false;
                    if (__instance.timeToDetonation <= 15.0)
                        __instance.GetComponent<PlayerStats>().TargetAchieve(disabler.GetComponent<NetworkIdentity>().connectionToClient, "thatwasclose");
                }
                for (sbyte index = 0; index < __instance.scenarios_resume.Length; ++index)
                {
                    if ((double)__instance.scenarios_resume[index].SumTime() > __instance.timeToDetonation && __instance.scenarios_resume[(int)index].SumTime() < (double)__instance.scenarios_start[AlphaWarheadController._startScenario].SumTime())
                        __instance.NetworksyncResumeScenario = index;
                }
                __instance.NetworktimeToDetonation = (AlphaWarheadController._resumeScenario < 0 ? __instance.scenarios_start[AlphaWarheadController._startScenario].SumTime() : __instance.scenarios_resume[AlphaWarheadController._resumeScenario].SumTime()) + __instance.cooldown;
                __instance.NetworkinProgress = false;
                DoorEventOpenerExtension.TriggerAction(DoorEventOpenerExtension.OpenerEventType.WarheadCancel);
                if (!NetworkServer.active)
                    return false;
                __instance._autoDetonate = false;
                return false;
            }
            catch(Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: CancelDetonation failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
