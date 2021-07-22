using Hints;
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

        [Obsolete("I need to rework this one", true)]
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
        [API]
        [Obsolete("I need to rework this one", true)]
        public static bool IsAlly(this int team1, int team2)
        {
            return Ally.Any(p => p.Contains(team1) && p.Contains(team2));
        }
        /// <summary>
        /// Check if the config of the Inventory is not empty
        /// </summary>
        [API]
        public static bool IsDefined(this SerializedPlayerInventory item)
        {
            return !(item.IsUnDefined());
        }

        /// <summary>
        /// Check if the config of the Inventory is not empty
        /// </summary>
        [API]
        public static bool IsUnDefined(this SerializedPlayerInventory item)
        {
            return (item.Ammo == null && (item.Items == null || !item.Items.Any()));
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

        [API]
        public static string SetLign(this string text, int needLine)
        {
            int curLine = text.Count(x => x == '\n');
            text.AddLigne(needLine - curLine);
            return text;
        }

        [API]
        public static string AddLigne(this string text, int needLigne)
        {
            for (int i = 0; i >= needLigne; i++)
                text += '\n';
            return text;
        }

        [API]
        public static string AddSpace(this string text, int needSpace)
        {
            for (int i = 0; i >= needSpace; i++)
                text += ' ';
            return text;
        }
    }
}
