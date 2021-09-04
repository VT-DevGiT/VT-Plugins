using HarmonyLib;
using Scp914;
using Synapse;
using System;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.UserCode_CmdUseElevator))]
    class UseElevatorPatch
    {
        [HarmonyPrefix]
        private static bool UseElevator(PlayerInteract __instance, GameObject elevator)
        {
            try
            {
                if (!__instance.CanInteract || elevator == null)
                    return false;
                Lift component = elevator.GetComponent<Lift>();
                if (component == null)
                    return false;
                foreach (Lift.Elevator _elevator in component.elevators)
                {
                    if (__instance.ChckDis(_elevator.door.transform.position))
                    {
                        bool flag = true;
                        VTController.Server.Events.Map.InvokeElevatorIneractEvent(__instance.GetPlayer(), component.GetElevator(), ref flag);
                        if (flag)
                        {
                            component.UseLift();
                            __instance.OnInteract();
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: UseElevator failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
