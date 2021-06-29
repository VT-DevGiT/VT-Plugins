using HarmonyLib;
using Mirror;
using Scp914;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(PlayerInteract), nameof(PlayerInteract.CallCmdUse914))]
    class Scp914ActivatePatch
    {
        private static bool Prefix(PlayerInteract __instance)
        {
            try
            {
                if (!__instance._playerInteractRateLimit.CanExecute() || !__instance.ChckDis(Scp914Machine.singleton.button.position))
                    return false;
                bool flag = true;
                if (__instance._hc.CufferId > 0 || __instance._hc.ForceCuff && !PlayerInteract.CanDisarmedInteract || Scp914Machine.singleton.working)
                    flag = false;
                if (flag)
                    Scp914Machine.singleton.RpcActivate(NetworkTime.time);
                __instance.OnInteract();
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event:Activate914Patch failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
