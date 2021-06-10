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

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Grenade), nameof(Grenade.OnCollisionEnter))]
    class ColisionGrenadePatch
    {
        private static bool Prefix(Grenade __instance)
        {
            try
            {
                if (!NetworkServer.active) return false;
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

                VTController.Server.Events.Grenade.InvokeCollisionGrenadeEvent(__instance, Type, ref falg);
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
