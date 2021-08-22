using HarmonyLib;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using Synapse.Api.Enum;
using System;
using VT_Referance.Event;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(TimeGrenade), nameof(TimeGrenade.OnCollisionEnter))]
    class ColisionGrenadePatch
    {
        private static bool Prefix(TimeGrenade __instance)
        {
            try
            {
                GrenadeType Type;
                if (__instance.GetType() == typeof(ExplosionGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(FlashbangGrenade))
                    Type = GrenadeType.Flashbang;
                else if (__instance.GetType() == typeof(Scp018Projectile))
                    Type = GrenadeType.Scp018;
                else
                    Type = (GrenadeType)4;

                VTController.Server.Events.Grenade.InvokeCollisionGrenadeEvent(__instance, Type);
                return true;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeCollisionEnter failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
    
}
