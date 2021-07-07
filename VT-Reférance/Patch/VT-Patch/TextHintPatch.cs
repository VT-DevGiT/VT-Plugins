using HarmonyLib;
using Hints;
using VT_Referance.Method;

namespace VT_Referance.Patch.VT_Patch
{
   
    [HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
    static class TextHintPatch
    {
        private static bool Prefix(HintDisplay __instance, Hint hint)
        {
            if (hint is TextHint && !__instance.isLocalPlayer && hint.DurationScalar != 36827)
            {
                TextHintTimed Text = new TextHintTimed(hint as TextHint);
                TextHandleSingleton.Instance.AddMessage(__instance.netIdentity, Text);
                return false;
            }
            return true;
        }
    }
}
