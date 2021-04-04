using Synapse.Api;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Interface;
using VT_Referance.Variable;

namespace VT_Referance.Method
{
    public static class PlayerExtension
    {
        /// <summary>
        /// True if the player can see a gameobject
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <param name="obj">The Tested GameObject</param>
        /// <returns></returns>
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

        /// <summary>
        /// True if the player is a robotic tactical unit
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <returns></returns>
        public static bool IsUTR(this Player player)
        {
            return player.CustomRole is IUtrRole;
            //List <int> UTRID = new List<int>() { (int)RoleID.AndersonUTRheavy, (int)RoleID.AndersonUTRlight, (int)RoleID.FoundationUTR };
            //return UTRID.Contains(player.RoleID);
        }
    }
}
