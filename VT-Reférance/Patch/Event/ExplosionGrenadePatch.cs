using HarmonyLib;
using Interactables;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using NorthwoodLib.Pools;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    [HarmonyPatch(typeof(ExplosionGrenade), nameof(ExplosionGrenade.Explode))]
    class FragExplosionGrenadePatch
    {
        internal static TimeGrenade grenade;

        private static bool Prefix(ExplosionGrenade __instance)
        {
            try
            {
                bool falg = true;
                GrenadeType Type1;
                if (__instance.GetType() == typeof(Scp018Projectile))
                    Type1 = GrenadeType.Scp018;
                else if (__instance.GetType() == typeof(ExplosionGrenade))
                    Type1 = GrenadeType.Grenade;
                else
                    Type1 = (GrenadeType)4;

                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(__instance, Type1, ref falg);
                if (!falg) return false;

                grenade = __instance;
                HashSet<uint> set1 = HashSetPool<uint>.Shared.Rent();
                HashSet<uint> set2 = HashSetPool<uint>.Shared.Rent();
                foreach (Collider collider in Physics.OverlapSphere(__instance.transform.position, __instance._maxRadius, __instance._detectionMask))
                {
                    if (NetworkServer.active)
                    {
                        IExplosionTrigger component1;
                        if (collider.TryGetComponent(out component1))
                            component1.OnExplosionDetected(__instance.PreviousOwner, __instance.transform.position, __instance._maxRadius);
                        IDestructible component2;
                        if (collider.TryGetComponent(out component2))
                        {
                            if (!set1.Contains(component2.NetworkId) && __instance.ExplodeDestructible(component2))
                                set1.Add(component2.NetworkId);
                        }
                        else
                        {
                            InteractableCollider component3;
                            if (collider.TryGetComponent(out component3) && component3.Target is DoorVariant target6 && set2.Add(target6.netId))
                                __instance.ExplodeDoor(target6);
                        }
                    }
                    if (collider.attachedRigidbody != null)
                        __instance.ExplodeRigidbody(collider.attachedRigidbody);
                }
                HashSetPool<uint>.Shared.Return(set1);
                HashSetPool<uint>.Shared.Return(set2);
                return false;

            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFragExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(FlashbangGrenade), nameof(FlashbangGrenade.PlayExplosionEffects))]
    class FlashExplosionGrenadePatch
    {
        private static bool Prefix(FlashbangGrenade __instance)
        {
            try
            {
                if (!NetworkServer.active)
                    return false;
                GrenadeType Type;
                if (__instance.GetType() == typeof(FlashbangGrenade))
                    Type = GrenadeType.Flashbang;
                else
                    Type = (GrenadeType)4;
                bool flag = true;
                VTController.Server.Events.Grenade.InvokeExplosionGrenadeEvent(__instance, Type, ref flag);
                if (!flag) return false;

                double time = __instance._blindingOverDistance.keys[__instance._blindingOverDistance.length - 1].time;
                float num = (float)(time * time);
                foreach (KeyValuePair<GameObject, ReferenceHub> allHub in ReferenceHub.GetAllHubs())
                    if (!(allHub.Value == null) && (__instance.transform.position - allHub.Value.transform.position).sqrMagnitude <= (double)num && !(allHub.Value == __instance.PreviousOwner.Hub) && HitboxIdentity.CheckFriendlyFire(__instance.PreviousOwner.Role, allHub.Value.characterClassManager.CurClass))
                       __instance.ProcessPlayer(allHub.Value);
                return false;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Vt-Event: GrenadeFlashExplosion failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
    
}
