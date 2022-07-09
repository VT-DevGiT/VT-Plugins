using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using Mirror;
using Subtitles;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static LightContainmentZoneDecontamination.DecontaminationController;

namespace VTIntercom
{
/*
    [HarmonyPatch(typeof(DecontaminationController), nameof(DecontaminationController.Update))]
    public static class DecontPatch
    {
        public static DecontaminationController.DecontaminationPhase[] DecontaminationPhases = new DecontaminationController.DecontaminationPhase[] { };
        public static bool Prefix(DecontaminationController __instance)
        {
            if (__instance._stopUpdating || __instance.disableDecontamination)
                return false;

            __instance.CheckBroadcaster();
            if (__instance.RoundStartTime <= 0.0)
            {
                if (__instance.RoundStartTime == -1.0)
                {
                    __instance._stopUpdating = true;
                }

                return false;
            }

            if (__instance._justJoinedCooldown < 10f)
            {
                __instance._justJoinedCooldown += Time.deltaTime;
            }

            if (!((float)GetServerTime > DecontaminationPhases[__instance._nextPhase].TimeTrigger))
            {
                return false;
            }

            if (DecontaminationPhases[__instance._nextPhase].Function == DecontaminationPhase.PhaseFunction.Final)
            {
                __instance.FinishDecontamination();
            }

            if (NetworkServer.active && DecontaminationPhases[__instance._nextPhase].Function == DecontaminationPhase.PhaseFunction.OpenCheckpoints)
            {
                DoorEventOpenerExtension.TriggerAction(DoorEventOpenerExtension.OpenerEventType.DeconEvac);
            }

            if (__instance._nextPhase == DecontaminationPhases.Length - 1)
            {
                __instance._stopUpdating = true;
            }
            else
            {
                __instance._nextPhase++;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(DecontaminationController), nameof(DecontaminationController.ServersideSetup))]
    internal static class ServersideSetupKill
    {
        public static bool Prefix(DecontaminationController __instance)
        {
            return false;
        }
    }*/
}
