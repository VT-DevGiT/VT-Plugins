using Mirror;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better939
{
	public static class Method
    {
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
