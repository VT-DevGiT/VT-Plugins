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
    [HarmonyPatch(typeof(Scp914Controller), nameof(Scp914Controller.ServerInteract))]
    class Scp914InteractPatch
    {
        private static bool Prefix(Scp914Controller __instance, ReferenceHub ply, byte colliderId)
        {
            try
            {
                if (__instance._remainingCooldown > 0.0)
                    return false;
                bool flag = true;
                switch ((Scp914InteractCode)colliderId)
                {
                    case Scp914InteractCode.ChangeMode:
                        VTController.Server.Events.Map.InvokeChange914KnobSettingEvent(__instance.GetPlayer(), ref flag);
                        return flag;
                    case Scp914InteractCode.Activate:
                        VTController.Server.Events.Map.InvokeScp914ActivateEvent(__instance.GetPlayer(), ref flag);
                        return flag;
                }
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: Activate914Patch failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
