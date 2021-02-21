using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT939
{
    class Patch
    {
		[HarmonyPatch(typeof(Scp939_VisionController), nameof(Scp939_VisionController.AddVision))]
		public static class Scp939VisionShieldPatch
		{
			public static void Prefix(Scp939_VisionController __instance, Scp939PlayerScript scp939)
			{
				if (Plugin.Config.Scp939SeeingAhpAmount <= 0 || __instance._ccm.CurRole.team == Team.SCP) return;
				bool isFound = false;
				for (int i = 0; i < __instance.seeingSCPs.Count; i++)
				{
					if (__instance.seeingSCPs[i].scp == scp939)
					{
						isFound = true;
					}
				}

				if (!isFound)
				{
					scp939._hub.playerStats.NetworkmaxArtificialHealth += Plugin.Config.Scp939SeeingAhpAmount;
					scp939._hub.playerStats.unsyncedArtificialHealth = Mathf.Clamp(scp939._hub.playerStats.unsyncedArtificialHealth + 
						Plugin.Config.Scp939SeeingAhpAmount, 0, scp939._hub.playerStats.maxArtificialHealth);
				}

			}
		}
		[HarmonyPatch(typeof(Scp939_VisionController), nameof(Scp939_VisionController.UpdateVisions))]
		public static class Scp939VisionShieldRemovePatch
		{
			public static bool Prefix(Scp939_VisionController __instance)
			{
				if (Plugin.Config.Scp939SeeingAhpAmount < 0) return true;

				for (int i = 0; i < __instance.seeingSCPs.Count; i++)
				{
					__instance.seeingSCPs[i].remainingTime -= 0.02f;
					if (__instance.seeingSCPs[i].scp == null || !__instance.seeingSCPs[i].scp.iAm939 || __instance.seeingSCPs[i].remainingTime <= 0f)
					{
						if (__instance.seeingSCPs[i].scp != null && __instance.seeingSCPs[i].scp.iAm939)
						{
							__instance.seeingSCPs[i].scp._hub.playerStats.NetworkmaxArtificialHealth = Mathf.Clamp(__instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth 
								- Plugin.Config.Scp939SeeingAhpAmount, 0, __instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth);
							__instance.seeingSCPs[i].scp._hub.playerStats.unsyncedArtificialHealth = Mathf.Clamp(__instance.seeingSCPs[i].scp._hub.playerStats.unsyncedArtificialHealth 
								- Plugin.Config.Scp939SeeingAhpAmount, 0, __instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth);
						}
						__instance.seeingSCPs.RemoveAt(i);
						return false;
					}
				}
				return false;
			}
		}
	}
}
