using HarmonyLib;
using InventorySystem.Items.Radio;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT079
{
    [HarmonyPatch]
    internal static class Patchs
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Recontainer079), nameof(Recontainer079.OnClassChanged))]
        static bool OnClassChanged() => Plugin.Instance.Config.NotReconfineWhenNoSCPLeft;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Scp079PlayerScript), nameof(Scp079PlayerScript.UserCode_RpcGainExp))]
        static bool UserCode_RpcGainExp(Scp079PlayerScript __instance, ExpGainType type, RoleType details)
        {
            try
            {
                var player = __instance.GetPlayer();
                if (player.RoleID != Plugin.CASSIE) return true;

                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"UserCode_RpcGainExp patch error:\n{e}");
                return true;
            }
        }
    }
}
