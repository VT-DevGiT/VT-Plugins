using HarmonyLib;
using Exiled.API.Features;
using Server = Synapse.Server;
using System.Reflection;

namespace VT_Synapsesl_Exiled
{
    [HarmonyPatch(typeof(Log),nameof(Log.SendRaw))]
    class PatchLog_Info
    {
        private static bool Prefix(object message, System.ConsoleColor color)
        {
            Server.Get.Logger.Send(message.ToString(), color);
            return false;
        }
    }
}
