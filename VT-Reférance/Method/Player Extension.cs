using Synapse.Api;
using Synapse.Config;
using System.Linq;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VT_Referance.Method
{
    [API]
    public static class Player_Extension
    {
        /// <summary>
        /// True if the player can see a gameobject
        /// </summary>
        /// <param name="player">The tested player</param>
        /// <param name="obj">The Tested GameObject</param>
        /// <returns></returns>
        [Unstable]
        public static bool IsTargetVisible(this Player player, GameObject obj) 
            => Methods.IsTargetVisible(player.gameObject.GetComponent<UnityEngine.Camera>(), obj);

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

            var roleWithSquad = new RoleType[] { RoleType.FacilityGuard, RoleType.NtfPrivate,
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
