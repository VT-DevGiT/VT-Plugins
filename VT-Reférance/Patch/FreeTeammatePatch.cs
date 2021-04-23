using HarmonyLib;
using System;
using UnityEngine;
using VT_Referance.Event;
using GameCore;


namespace VT_Referance.Patch
{
    [HarmonyPatch(typeof(Handcuffs), nameof(Handcuffs.CallCmdFreeTeammate))]
    internal static class FreeTeammatePatch
    {
        private static bool Prefix(Handcuffs __instance, GameObject target)
        {
            try
            {
                if (!__instance._interactRateLimit.CanExecute() || (UnityEngine.Object)target == (UnityEngine.Object)null || ((double)Vector3.Distance(target.transform.position, __instance.transform.position) > (double)__instance.raycastDistance * 1.10000002384186))
                    return false;
                Synapse.Api.Player cuffer = __instance.GetPlayer();
                Synapse.Api.Player target2 = target.GetPlayer();
                Synapse.Api.Items.SynapseItem item = cuffer.ItemInHand;

                bool flag = __instance.MyReferenceHub.characterClassManager.CurRole.team == Team.SCP;
                
                Events.PlayerSingleton.Instance.InvokePlayerFreeTeammateEvent(target2, cuffer, item, ref flag);

                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: PlayerFreeTeammate failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
        
    }
}
