using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Reflection;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{

    [HarmonyPatch(typeof(PlayerEvents), "InvokePlayerDamageEvent")]
    class PatchSynapseDamage
    {
        private static void BaseEvent(PlayerEvents source, PlayerDamageEventArgs ev)
        {
            var eventsField = typeof(PlayerEvents).GetField("PlayerDamageEvent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (eventsField != null)
            {
                object eventHandlerList = eventsField.GetValue(source);
                if (eventHandlerList != null)
                {
                    var my_event_invoke = eventHandlerList.GetType().GetMethod("Invoke");
                    if (my_event_invoke != null)
                    {
                        my_event_invoke.Invoke(eventHandlerList, new object[] { ev });
                    }
                }
                else
                {
                    Server.Get.Logger.Error("Vt-Event: PlayerDamagePost failed!! \n eventHandlerList null");
                }
            }
            else
            {
                Server.Get.Logger.Error("Vt-Event: PlayerDamagePost failed!! \n eventsField null");
            }
        }
        private static bool Prefix(PlayerEvents __instance, Player victim, Player killer, ref PlayerStats.HitInfo info, out bool allow)
        {
            try
            {
                var ev = new PlayerDamageEventArgs
                {
                    HitInfo = info,
                };
                ev.SetProperty<Player>("Killer", killer);
                ev.SetProperty<Player>("Victim", victim);

                BaseEvent(__instance, ev);
                info = ev.HitInfo;
                allow = ev.Allow;
                VTController.Server.Events.Player.InvokePlayerDamagePostEvent(victim, killer, ref info, ref allow);
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
