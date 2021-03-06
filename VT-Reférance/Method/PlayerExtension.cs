using Synapse.Api;
using UnityEngine;

namespace VT_Referance.Method
{
    public static class PlayerExtension
    {
        public static bool IsTargetVisible(this Player player, GameObject obj)
        {
            Camera camera = player.gameObject.GetComponent<Camera>();
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            var point = obj.transform.position;
            foreach(var plan in planes)
            {
                if (plan.GetDistanceToPoint(point) < 0)
                    return false;
            }
            return true;
        }
    }
}
