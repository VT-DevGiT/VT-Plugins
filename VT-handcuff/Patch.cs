using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VThandcuff
{
    class Patch
    {
        [HarmonyPatch(typeof(Handcuffs), nameof(Handcuffs.UpdateCuffedPlayers))]
        internal static class ServersideSetupKill
        {
            public static bool Prefix(Handcuffs __instance)
            {
                if (Plugin.Instance.CuffedPlayer.Where(p => p.PlayerId == __instance.CufferId).Any())
                    return false;
                else
                    return true;
            }
        }
    }
}
