using Hints;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_Referance.Method
{
    [API]
    public static class Extension
    {
        /// <summary>
        /// True if the player can see a gameobject
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <param name="obj">The Tested GameObject</param>
        /// <returns></returns>
        [API]
        public static bool IsTargetVisible(this Player player, GameObject obj)
        {
            UnityEngine.Camera camera = player.gameObject.GetComponent<UnityEngine.Camera>();
            
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
        [API]
        public static bool Is939(this Player player)
        {
            return player.RoleID == (int)RoleID.Scp93953 || player.RoleID == (int)RoleID.Scp93989;
        }


        /// <summary>
        /// True if the player is a robotic tactical unit
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <returns></returns>
        [API]
        public static bool IsUTR(this Player player)
        {
            return player.CustomRole is IUtrRole;
        }

        /// <summary>
        /// Check if the config of the Inventory is not empty
        /// </summary>
        [API]
        public static bool IsDefined(this SerializedPlayerInventory item)
        {
            return !item.IsUnDefined();
        }

        /// <summary>
        /// Check if the config of the Inventory is not empty
        /// </summary>
        [API]
        public static bool IsUnDefined(this SerializedPlayerInventory item)
        {
            return item == null || item.Ammo == null && (item.Items == null || !item.Items.Any());
        }

        [API]
        public static T GetOrAddComponent<T>(this Player player) where T : Component
        {
            GameObject plyGameObject = player.gameObject;
            return plyGameObject.GetOrAddComponent<T>();
        }

        [API]
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T Component;
            if (!gameObject.TryGetComponent(out Component))
                Component = gameObject.AddComponent<T>();
            return Component;
        }


        /// <summary>
        /// Waring it wait 0.01 second if the player is spawning
        /// </summary>
        [API]
        public static void SetDisplayCustomRole(this Player player, string roleName)
        {
            player.RemoveDisplayInfo(PlayerInfoArea.Role);

            RoleType[] roleWithSquad = new RoleType[] { RoleType.FacilityGuard, RoleType.NtfPrivate,
                    RoleType.NtfSergeant, RoleType.NtfCaptain, RoleType.NtfSpecialist};
            if (roleWithSquad.Contains(player.RoleType))
            {
                player.RemoveDisplayInfo(PlayerInfoArea.UnitName);
                player.DisplayInfo = $"{roleName} ({player.UnitName})";
            }
            else
            {
                player.DisplayInfo = roleName;
            }
        }

    }
}
