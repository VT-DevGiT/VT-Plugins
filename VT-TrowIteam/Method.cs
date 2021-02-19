using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VTTrowItem
{
    public static class Method
    {
        internal static IEnumerator<float> Throw(Pickup item, Vector3 direction)
        {
            yield return Timing.WaitUntilFalse(() => item != null && item.Rb == null);

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
                Synapse.Api.Logger.Get.Error($"Lunch coroutine : {e.Message}");
            }
        }
    }
}
