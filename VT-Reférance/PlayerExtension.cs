using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common_Utiles
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
