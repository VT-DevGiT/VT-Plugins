using Assets._Scripts.Dissonance;
using HarmonyLib;
using VT_Referance.Method;

namespace VTRpSCP.Patch
{
    [HarmonyPatch(typeof(ScpVoiceProfile), nameof(ScpVoiceProfile.Apply))]
    static class TextHintPatch
    {
        private static void Postfix(ScpVoiceProfile __instance)
        {
            if (!__instance.dissonanceSetup.GetPlayer().Is939())
                __instance.dissonanceSetup.DisableSpeaking(TriggerType.Role, Assets._Scripts.Dissonance.RoleType.SCP);
            if (__instance.dissonanceSetup.GetPlayer().RoleType == RoleType.Scp049)
                __instance.dissonanceSetup.EnableSpeaking(TriggerType.Proximity | TriggerType.Intercom);
        }
    }
}
