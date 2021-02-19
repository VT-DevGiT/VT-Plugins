using HarmonyLib;
using Mirror;
using Synapse.Api;
using System;
using UnityEngine;

namespace Better939
{
    public static class Patch
    {
        //https://github.com/sanyae2439/SanyaPlugin_Exiled

        [HarmonyPatch(typeof(Scp939_VisionController), nameof(Scp939_VisionController.AddVision))]
        public static class Scp939VisionAhp
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
                    scp939._hub.playerStats.unsyncedArtificialHealth = Mathf.Clamp(scp939._hub.playerStats.unsyncedArtificialHealth + Plugin.Instance.Scp939SeeingAhpAmount, 0, scp939._hub.playerStats.maxArtificialHealth);
                }

            }
        }

        /*
        [HarmonyPatch(typeof(Scp939_VisionController), nameof(Scp939_VisionController.UpdateVisions))]
        public static class Scp939VisionAhpRemovePatch
        {
            public static bool Prefix(Scp939_VisionController __instance)
            {
                if (Plugin.Config.Scp939SeeingAhpAmount < 0) return true;

                for (int i = 0; i < __instance.seeingSCPs.Count; i++)
                {
                    __instance.seeingSCPs[i].remainingTime -= 0.02f;
                    if (__instance.seeingSCPs[i].scp == null || !__instance.seeingSCPs[i].scp.iAm939 || __instance.seeingSCPs[i].remainingTime <= 0f)
                    {
                        if (__instance.seeingSCPs[i].scp != null && __instance.seeingSCPs[i].scp.iAm939 && __instance._ccm.CurRole.team != Team.SCP)
                        {
                            __instance.seeingSCPs[i].scp._hub.playerStats.NetworkmaxArtificialHealth = Mathf.Clamp(__instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth - Plugin.Config.Scp939SeeingAhpAmount, 0, __instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth);
                            __instance.seeingSCPs[i].scp._hub.playerStats.unsyncedArtificialHealth = Mathf.Clamp(__instance.seeingSCPs[i].scp._hub.playerStats.unsyncedArtificialHealth - Plugin.Config.Scp939SeeingAhpAmount, 0, __instance.seeingSCPs[i].scp._hub.playerStats.maxArtificialHealth);
                        }
                        __instance.seeingSCPs.RemoveAt(i);
                        return false;
                    }
                }
                return false;
            }
        }
        */
        public static void SendCustomTargetRpc(this Player target, NetworkIdentity behaviorOwner, Type targetType, string rpcName, object[] values)
        {
            NetworkWriter writer = NetworkWriterPool.GetWriter();

            foreach (var value in values)
                GetWriteExtension(value)?.Invoke(null, new object[] { writer, value });

            var msg = new RpcMessage
            {
                netId = behaviorOwner.netId,
                componentIndex = GetComponentIndex(behaviorOwner, targetType),
                functionHash = targetType.FullName.GetStableHashCode() * 503 + rpcName.GetStableHashCode(),
                payload = writer.ToArraySegment()
            };
            target.Connection.Send(msg, 0);
            NetworkWriterPool.Recycle(writer);
        }

        public static int GetComponentIndex(NetworkIdentity identity, Type type)
        {
            return Array.FindIndex(identity.NetworkBehaviours, (x) => x.GetType() == type);
        }

        public static System.Reflection.MethodInfo GetWriteExtension(object value)
        {
            Type type = value.GetType();
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteString));
                case TypeCode.Boolean:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteBoolean));
                case TypeCode.Int16:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteInt16));
                case TypeCode.Int32:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WritePackedInt32));
                case TypeCode.UInt16:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteUInt16));
                case TypeCode.Byte:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteByte));
                case TypeCode.SByte:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteSByte));
                case TypeCode.Single:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteSingle));
                case TypeCode.Double:
                    return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteDouble));
                default:
                    if (type == typeof(Vector3))
                        return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteVector3));
                    if (type == typeof(Vector2))
                        return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteVector2));
                    if (type == typeof(GameObject))
                        return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteGameObject));
                    if (type == typeof(Quaternion))
                        return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WriteQuaternion));
                    if (type == typeof(BreakableWindow.BreakableWindowStatus))
                        return typeof(BreakableWindowStatusSerializer).GetMethod(nameof(BreakableWindowStatusSerializer.WriteBreakableWindowStatus));
                    if (type == typeof(Grenades.RigidbodyVelocityPair))
                        return typeof(Grenades.RigidbodyVelocityPairSerializer).GetMethod(nameof(Grenades.RigidbodyVelocityPairSerializer.WriteRigidbodyVelocityPair));
                    if (type == typeof(ItemType))
                        return typeof(NetworkWriterExtensions).GetMethod(nameof(NetworkWriterExtensions.WritePackedInt32));
                    if (type == typeof(PlayerMovementSync.RotationVector))
                        return typeof(RotationVectorSerializer).GetMethod(nameof(RotationVectorSerializer.WriteRotationVector));
                    if (type == typeof(Pickup.WeaponModifiers))
                        return typeof(WeaponModifiersSerializer).GetMethod(nameof(WeaponModifiersSerializer.WriteWeaponModifiers));
                    if (type == typeof(Offset))
                        return typeof(OffsetSerializer).GetMethod(nameof(OffsetSerializer.WriteOffset));
                    return null;
            }
        }


    }
}
