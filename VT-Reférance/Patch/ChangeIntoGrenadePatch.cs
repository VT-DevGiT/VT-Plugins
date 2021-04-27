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
    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ChangeIntoGrenade))]
    class ChangeIntoFragPatch
    {
        private static bool Prefix(FragGrenade __instance, Pickup pickup, ref bool __result)
        {
            Server.Get.Logger.Info("VT_Referance Event Patch ChangeIntoGrenade");
            try
            {
                if (!NetworkServer.active) return false;
                
                    
                FragGrenade grenade = __instance;
                SynapseItem item = pickup.GetSynapseItem();
                GrenadeType Type;
                if (__instance.GetType() == typeof(FragGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(Scp018Grenade))
                    Type = GrenadeType.Scp018;
                else
                    Type = (GrenadeType)4;

                bool falg = true;
                Events.GrenadeSingleton.Instance.InvokeChangeIntoFragEvent(item, grenade, Type, ref falg);
                if (falg) 
                    __result = false;
                return falg;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: ChangeIntoGrenade failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
