using Interactables.Interobjects.DoorUtils;
using Synapse.Api;
using Synapse.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Interface;
using VT_Referance.Variable;

namespace VT_Referance.Method
{
    public static class Extension
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
            foreach (var plan in planes)
            {
                if (plan.GetDistanceToPoint(point) < 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// True if the player is a 939
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <returns></returns>
        public static bool Is939(this Player player)
        {
            return player.RoleID == (int)RoleID.Scp93953 || player.RoleID == (int)RoleID.Scp93989;
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
        /// <summary>
        /// List of ally team
        /// </summary>
        private static List<List<int>> Ally = new List<List<int>>()
        {
            new List<int>{ (int)TeamID.VIP, (int)TeamID.NetralSCP, (int)TeamID.NTF, (int)TeamID.CDM, (int)TeamID.RSC},
            new List<int>{ (int)TeamID.AND},
            new List<int>{ (int)TeamID.SCP, (int)TeamID.SHA},
            new List<int>{ (int)TeamID.CDP, (int)TeamID.RSC}
        };
        /// <summary>
        /// Check if a team is ally to an other team
        /// </summary>
        /// <param name="team1">ID of team One</param>
        /// <param name="team2">ID of team tow</param>
        /// <returns></returns>
        public static bool IsAlly(this int team1, int team2)
        {
            return Ally.Any(p => p.Contains(team1) && p.Contains(team2));
        }

        public static bool IsDefined(this SerializedPlayerInventory item)
        {
            return !(item.IsUnDefined());
        }

        public static bool IsUnDefined(this SerializedPlayerInventory item)
        {
            return (item.Ammo == null && (item.Items == null || !item.Items.Any()));
        }
    }
}
