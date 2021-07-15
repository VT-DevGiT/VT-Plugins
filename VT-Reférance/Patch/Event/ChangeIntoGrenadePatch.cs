using Grenades;
using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using System;
using UnityEngine;
using VT_Referance.Event;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(FragGrenade), nameof(FragGrenade.ChangeIntoGrenade))]
    class ChangeIntoFragPatch
    {
        private static bool Prefix(FragGrenade __instance, Pickup item, ref bool __result)
        {
            try
            {
                if (!NetworkServer.active) return false;
                __result = false;
                GrenadeSettings grenadeSettings = null;
                for (int index = 0; index < __instance.thrower.availableGrenades.Length; ++index)
                {
                    GrenadeSettings availableGrenade = __instance.thrower.availableGrenades[index];
                    if (availableGrenade.inventoryID == item.ItemId)
                    {
                        if (!__instance.chainSupportedGrenades.Contains(index))
                           return false;
                        grenadeSettings = availableGrenade;
                        break;
                    }
                }
                if (grenadeSettings == null)
                    return false;
                SynapseItem pickup = item.GetSynapseItem();
                GrenadeType Type;
                bool falg = true;

                if (__instance.GetType() == typeof(FragGrenade))
                    Type = GrenadeType.Grenade;
                else if (__instance.GetType() == typeof(Scp018Grenade))
                    Type = GrenadeType.Scp018;
                else 
                    Type = (GrenadeType)4;
                

                VTController.Server.Events.Grenade.InvokeChangeIntoFragEvent(pickup, __instance, Type, ref falg);
                if (falg)
                {
                    Transform transform = item.transform;
                    Grenade component = UnityEngine.Object.Instantiate(grenadeSettings.grenadeInstance, transform.position, transform.rotation).GetComponent<Grenade>();
                    component.InitData(__instance, item);
                    NetworkServer.Spawn(component.gameObject);                    
                    item.Delete();
                    __result = true;
                }
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: ChangeIntoGrenade failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
