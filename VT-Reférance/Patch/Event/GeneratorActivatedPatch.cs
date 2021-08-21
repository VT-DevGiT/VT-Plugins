using HarmonyLib;
using System;

namespace VT_Referance.Patch.Event
{
    /*
    [HarmonyPatch(typeof(Generator079), nameof(Generator079.CheckFinish))]
    class GeneratorActivated
    {
        private static void Prefix(Generator079 __instance)
        {
            try
            {
                if (__instance.prevFinish || __instance._localTime > 0.0)
                    VTController.Server.Events.Map.InvokeGeneratorActivatedEvent(__instance.GetGenerator());
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GeneratorActivated failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
            }
        }
    }
    */
}
