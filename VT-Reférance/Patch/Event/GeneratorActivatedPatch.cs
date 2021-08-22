using HarmonyLib;
using MapGeneration.Distributors;
using Mirror;
using Synapse;
using System;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(Scp079Generator), nameof(Scp079Generator.ServerUpdate))]
    class GeneratorActivated
    {
        private static bool Prefix(Scp079Generator __instance)
        {
            try
            { 
                if (!NetworkServer.active)
                    return false;
                bool flag = __instance._currentTime >= (double)__instance._totalActivationTime;
                if (!flag)
                {
                    int num = Mathf.FloorToInt(__instance._totalActivationTime - __instance._currentTime);
                    if (num != __instance._syncTime)
                        __instance.Network_syncTime = (short)num;
                }
                if (__instance.ActivationReady)
                {
                    if (flag && !__instance.Engaged)
                    {
                        VTController.Server.Events.Map.InvokeGeneratorActivatedEvent(__instance.GetGenerator());

                        __instance.Engaged = true;
                        __instance.Activating = false;

                        return false;
                    }
                    __instance._currentTime += Time.deltaTime;
                }
                else
                {
                    if (__instance._currentTime == 0.0 | flag)
                        return false;
                    __instance._currentTime -= __instance.DropdownSpeed * Time.deltaTime;
                }
                __instance._currentTime = Mathf.Clamp(__instance._currentTime, 0.0f, __instance._totalActivationTime);
                return false;
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Vt-Event: GeneratorActivatedEvent failed!!\n{e}\nStackTrace:\n{e.StackTrace}");

                return true;
            }
        }
    }
}
