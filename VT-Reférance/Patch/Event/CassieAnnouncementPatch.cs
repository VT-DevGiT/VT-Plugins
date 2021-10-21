using HarmonyLib;
using Respawning;
using System;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(RespawnEffectsController), nameof(RespawnEffectsController.RpcCassieAnnouncement))]
    class CassiePatch
    {
        [HarmonyPrefix]
        private static bool CassieAnnoncementPatch(RespawnEffectsController __instance, string words, bool makeHold, bool makeNoise)
        {
            try
            {
                bool flag = true;
                VTController.Server.Events.Map.InvokeCassieAnnouncementEvent(ref words,ref makeHold,ref makeNoise, ref flag);
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