using Grenades;
using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Event;

namespace VT_Referance.Patch
{
    [HarmonyPatch(typeof(Grenade), nameof(Grenade.OnCollisionEnter))]
    class ColisionGrenadePatch
    {
        private static bool Prefix(FragGrenade __instance, ref bool __result)
        {
            Server.Get.Logger.Info("VT_Referance Event Patch OnCollisionEnter");
            try
            {
                if (!NetworkServer.active) return false;
                FragGrenade grenade = __instance;
                bool falg = true;
                GrenadeType Type;
                if (__instance.GetType() == typeof(FragGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(FlashGrenade))
                    Type = GrenadeType.Flashbang;
                else if (__instance.GetType() == typeof(Scp018Grenade))
                    Type = GrenadeType.Scp018;
                else
                    Type = (GrenadeType)4;

                Events.GrenadeSingleton.Instance.InvokeExplosionGrenadeEvent(grenade, Type, ref falg);
                if (falg)
                    __result = __instance.ServersideExplosion();
                return falg;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeCollisionEnter failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
