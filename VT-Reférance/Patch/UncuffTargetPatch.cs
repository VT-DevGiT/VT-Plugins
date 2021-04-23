using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Event;

namespace VT_Referance.Patch
{
    [HarmonyPatch(typeof(Handcuffs), nameof(Handcuffs.CallCmdCuffTarget))]
    internal static class UncuffTargetPatch
    {
        private static bool Prefix(Handcuffs __instance)
        {
            try
            {
                if (!__instance._interactRateLimit.CanExecute()) return false;
                Synapse.Api.Player cuffer = __instance.GetPlayer();
                Synapse.Api.Player target = null;
                Synapse.Api.Items.SynapseItem item = cuffer.ItemInHand;

                foreach (GameObject player in PlayerManager.players)
                {
                    Handcuffs handcuffs = ReferenceHub.GetHub(player).handcuffs;
                    if (handcuffs.CufferId == __instance.MyReferenceHub.queryProcessor.PlayerId)
                    {
                        target = player.GetPlayer();
                        break;
                    }
                }
                bool flag = target != null;

                Events.PlayerSingleton.Instance.InvokePlayerUnCuffTargetEvent(target, cuffer, item, ref flag);

                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: PlayerUncuffTarget failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
