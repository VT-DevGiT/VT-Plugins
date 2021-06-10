using HarmonyLib;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(RespawnEffectsController), nameof(RespawnEffectsController.RpcCassieAnnouncement))]
    internal static class Starting
    {
        private static bool Prefix(RespawnEffectsController __instance, string words, bool makeHold, bool makeNoise)
        {
            try
            {
                bool flag = true;
                VTController.Server.Events.Map.InvokeCassieAnnouncementEvent(words, makeHold, makeNoise, ref flag);
                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: CassieAnnouncement failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}