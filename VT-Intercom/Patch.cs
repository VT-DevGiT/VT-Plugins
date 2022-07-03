using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using Mirror;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VTIntercom
{

    [HarmonyPatch(typeof(DecontaminationController), "Update")]
    internal static class Starting
    {
        public static int phase = 0;
        public static DecontaminationController.DecontaminationPhase[] DecontaminationPhases;
        public static bool Prefix(DecontaminationController __instance)
        {
            // remètre le song (attenction lors des teste à tuée un SCP)
            if (((Plugin)Plugin.Instance).DecontInProgress)
            { 
                //Server.Get.Logger.Info($"Test {DecontaminationController.GetServerTime} {phase} {DecontaminationPhases[phase].TimeTrigger}");
                if (DecontaminationController.GetServerTime <= DecontaminationPhases[phase].TimeTrigger)
                    return false;
                if (DecontaminationPhases[phase].AnnouncementLine !=null)
                {
                    Server.Get.Logger.Info($"Sound {phase}");
                   /* var _curFunction = DecontaminationPhases[phase].Function;
                    float b = 0.0f;
                    if (_curFunction == DecontaminationController.DecontaminationPhase.PhaseFunction.Final || _curFunction == DecontaminationController.DecontaminationPhase.PhaseFunction.GloballyAudible)
                        b = 1f;
                    else if (Mathf.Abs(SpectatorCamera.Singleton.cam.transform.position.y) < 200.0)
                        b = 1f;
                    Server.Get.Logger.Info($"b {b}");*/
                    __instance.AnnouncementAudioSource.volume = 1f;// Mathf.Lerp(__instance.AnnouncementAudioSource.volume, b, 1f);
                    Server.Get.Logger.Info($"Vol {__instance.AnnouncementAudioSource.volume}");
                    __instance.AnnouncementAudioSource.PlayOneShot(__instance.DecontaminationPhases[__instance.DecontaminationPhases.Length - 1].AnnouncementLine);
                }
                if (DecontaminationPhases[phase].Function == DecontaminationController.DecontaminationPhase.PhaseFunction.Final)
                {
                    Map.Get.Decontamination.InstantStart();
                    //Map.Get.GlitchedCassie($"Light Containment Zone is locked down and ready for decontamination .");
                }
                if (NetworkServer.active && DecontaminationPhases[phase].Function == DecontaminationController.DecontaminationPhase.PhaseFunction.OpenCheckpoints)
                {
                    //Map.Get.GlitchedCassie($"danger . LightContainmentZone decontamination start in 2 minutes .");
                    Server.Get.Logger.Info("Overture Check");
                    DoorEventOpenerExtension.TriggerAction(DoorEventOpenerExtension.OpenerEventType.DeconEvac);
                }
                if (phase == DecontaminationPhases.Length - 1)
                    ((Plugin)Plugin.Instance).DecontInProgress = false;
                else
                    ++phase;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(DecontaminationController), "ServersideSetup")]
    internal static class ServersideSetupKill
    {
        public static bool Prefix(DecontaminationController __instance)
        {
            return false;
        }
    }
}
