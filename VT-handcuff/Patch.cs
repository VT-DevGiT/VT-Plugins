using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VThandcuff
{

    [HarmonyPatch(typeof(Handcuffs), nameof(Handcuffs.UpdateCuffedPlayers))]
    internal static class ServersideSetupKill
    {
        public static bool Prefix(Handcuffs __instance)
        {
            if (Plugin.Config.CuffLock) 
                return false;
            else
                return true;
        }
    }
    
}
