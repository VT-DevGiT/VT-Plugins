using Footprinting;
using HarmonyLib;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using System;
using UnityEngine;
using VT_Referance.Event;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(TimedGrenadePickup), nameof(TimedGrenadePickup.OnExplosionDetected))]
    class ChangeIntoFragPatch
    {
        [HarmonyPrefix]
        private static bool ExplosionDetectedPatch(TimedGrenadePickup __instance, Footprint attacker, Vector3 source, float range)
        {
            try
            {
                if (Vector3.Distance(__instance.transform.position, source) / (double)range > 0.400000005960464)
                    return false;

                bool flag = true;
                GrenadeType Type;

                if (__instance.Info.ItemId == ItemType.GrenadeHE)
                    Type = GrenadeType.Grenade;
                else if (__instance.Info.ItemId == ItemType.GrenadeFlash)
                    Type = GrenadeType.Flashbang;
                else if (__instance.Info.ItemId == ItemType.SCP018)
                    Type = GrenadeType.Scp018;
                else 
                    Type = (GrenadeType)4;



                VTController.Server.Events.Grenade.InvokeChangeIntoFragEvent(__instance.GetSynapseItem(), FragExplosionGrenadePatch.grenade, Type, ref flag);
               
                return flag;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: ChangeIntoGrenade failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
    
}
