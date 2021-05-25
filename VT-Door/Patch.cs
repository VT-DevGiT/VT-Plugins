using HarmonyLib;
using Interactables.Interobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTDoor
{

    [HarmonyPatch(typeof(AirlockController), nameof(AirlockController.Update))]
    internal static class KillForTest1
    {
        public static bool Prefix(Handcuffs __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(AirlockController), nameof(AirlockController.ToggleAirlock))]
    internal static class KillForTest2
    {
        public static bool Prefix(Handcuffs __instance)
        {
            return false;
        }
    }
}
