using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VTTrowItem
{
    public static class Method
    {
        internal static IEnumerator<float> Throw(Synapse.Api.Items.SynapseItem itemDroped, Vector3 direction)
        {
            yield return Timing.WaitUntilTrue(() => itemDroped.PickupBase != null && itemDroped.PickupBase.Rb != null);
            var item = itemDroped.PickupBase;
            try
            {
                item.Rb.transform.Translate(Plugin.Config.initialPosVec3, Space.Self);
                item.Rb.AddForce(direction * Plugin.Config.ThrowForce, ForceMode.Impulse);
                Vector3 rotation = new Vector3(UnityEngine.Random.Range(Plugin.Config.RotationMinX, Plugin.Config.RotationMaxX),
                                           UnityEngine.Random.Range(Plugin.Config.RotationMinY, Plugin.Config.RotationMaxY),
                                           UnityEngine.Random.Range(Plugin.Config.RotationMinZ, Plugin.Config.RotationMaxZ)).normalized;
                item.Rb.angularVelocity = rotation;
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Launch coroutine : {e.Message}");
            }
        }
        /*
        private IEnumerator<float> ThrowWhenRigidbody(Pickup pickup, Vector3 dir)
        {
            Synapse.Api.Logger.Get.Info("Starting the coroutine, waiting until the thrown Pickup has a RigidBody (has physics).");

            yield return MEC.Timing.WaitUntilFalse(() => pickup != null && pickup.Rb == null); // mom im scared of loops

            Synapse.Api.Logger.Get.Info($"Rigidbody instantiated. Translating its position to {Plugin.Config.initialPosVec3}, then throwing with a force of {dir * Config.ThrowForce}.");

            try
            {
                pickup.Rb.transform.Translate(Plugin.Config.initialPosVec3, Space.Self);
                pickup.Rb.AddForce(dir * Plugin.Config.ThrowForce, ForceMode.Impulse);
                Vector3 rand = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-100f, 1f)).normalized;
                pickup.Rb.angularVelocity = rand.normalized * Config.RandomSpinForce;

            }
            catch (System.Exception ex)
            {
                Synapse.Api.Logger.Get.Error("ThrowItems thrown an exception in its \"throw\" coroutine:\n" + ex);
            }
        }
        */
    }
}
