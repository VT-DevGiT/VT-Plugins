using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System;
using System.Reflection;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{

    [HarmonyPatch(typeof(PlayerEvents), "InvokePlayerDamageEvent")]
    class SynapseDamagePatch
    {
        private static void BaseEvent(PlayerEvents source, string eventName, object[] parameters)
        {
            var eventsField = typeof(PlayerEvents).GetField(eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (eventsField != null)
            {
                object eventHandlerList = eventsField.GetValue(source);
                if (eventHandlerList != null)
                {
                    var my_event_invoke = eventHandlerList.GetType().GetMethod("Invoke");
                    if (my_event_invoke != null)
                    {
                        my_event_invoke.Invoke(eventHandlerList, parameters);
                    }
                }
                else Server.Get.Logger.Error("Vt-Event: PlayerDamagePost failed!! \n eventHandlerList null");
            }
            else
            {
                Server.Get.Logger.Error("Vt-Event: PlayerDamagePost failed!! \n eventsField null");
            }
        }
        [HarmonyPrefix]
        private static bool DamageEventPatch(PlayerEvents __instance, Player victim, Player killer, ref float damage, PlayerStatsSystem.DamageHandlerBase handlerBase, out bool allow)
        {
            try
            {
                var ev = new PlayerDamageEventArgs
                {
                    Damage = damage,
                };
                ev.SetProperty<Player>("Killer", killer);
                ev.SetProperty<Player>("Victim", victim);
                ev.SetProperty<SynapseItem>("DamageType", handlerBase.GetDamageType());

               // VTController.Server.Events.Player.InvokePlayerDamagePreEvent(victim, killer, ref damage, handlerBase);

                BaseEvent(__instance, "PlayerDamageEvent", new object[] { ev });
                
                damage = ev.Damage;
                allow = ev.Allow;

               // VTController.Server.Events.Player.InvokePlayerDamagePostEvent(victim, killer, ref damage, handlerBase);
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: PlayerDamagePost failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                allow = true;
                return true;
            }
        }
    }
}
