using MEC;
using Respawning.NamingRules;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Variable;

using Dissonance;
using Dissonance.Audio.Capture;
using Dissonance.Audio.Codecs;
using Dissonance.Integrations.MirrorIgnorance;
using Dissonance.Networking;
using Mirror;
using NAudio.Wave;
using System.IO;
using System.Net;
using HarmonyLib;
using PlayerStatsSystem;

namespace VT_Referance.Method
{
    public static class Methods
    {

        /// <summary>
        /// return a player according to the nearest corpse within the limit of a sphere
        /// </summary>
        /// <param name="player">center of the sphere</param>
        /// <param name="rayon">radius of the sphere</param>
        /// <returns>null if the player is not found</returns>
        [API]
        public static Player GetPlayercoprs(Player player, float rayon)
        {
            List<Synapse.Api.Ragdoll> ragdolls = 
                Map.Get.Ragdolls.Where(r => Vector3.Distance(r.GameObject.transform.position, player.Position) < rayon).ToList();
            ragdolls.Sort((Synapse.Api.Ragdoll x, Synapse.Api.Ragdoll y) =>
                Vector3.Distance(x.GameObject.transform.position, player.Position).CompareTo(Vector3.Distance(y.GameObject.transform.position, player.Position)));
            if (ragdolls.Count == 0) 
                return null;
            Player owner = ragdolls.First().Owner;
            if (owner != null && owner.RoleID == (int)RoleType.Spectator)
            {
                ragdolls.First().Destroy();
                return owner;
            }
            return null;
        }


        /// <summary>
        /// True if the last role of the played is in the SCP team or null if the player had no role
        /// </summary>
        /// <param name="player">the player you want tested</param>
        /// <returns>true if it was, false if not, null if the player is not referenced</returns>
        [API]
        public static bool? IsWasScpRole(Player player)
        {
            if (player != null && Data.PlayerRole.ContainsKey(player))
            {
                int roleId = Data.PlayerRole[player];
                if (_scpRoleVanila.Contains(roleId))
                    return true;
                else if (roleId > Synapse.Api.Roles.RoleManager.HighestRole)
                    return Server.Get.RoleManager.GetCustomRole(roleId).GetTeamID() == (int)TeamID.SCP;
                else return false;
            }
            return null;
        }

        private static List<int> _scpRoleVanila = new List<int>()
        {
            (int)RoleID.Scp0492,(int)RoleID.Scp079,(int)RoleID.Scp096,
            (int)RoleID.Scp106, (int)RoleID.Scp173,(int)RoleID.Scp93953,
            (int)RoleID.Scp93989, (int)RoleID.Scp049
        };


        /// <summary>
        /// if there is an active Air Bombardment or which is starting 
        /// </summary>
        [API]
        public static bool isAirBombCurrently = false;

        // Airbomb of SanayaPlugin
        /// <summary>
        /// Start Air Bombardment that detonates grenades all over the outer area
        /// </summary>
        /// <param name="waitforready">time before the start</param>
        /// <param name="limit">if set to -1 it continues indefinitely</param>
        [API]
        public static IEnumerator<float> AirBomb(int waitforready = 7, int limit = -1)
        {
            if (isAirBombCurrently)
                yield break;
            else isAirBombCurrently = true;

            Room OutsideRoom = Server.Get.Map.GetRoom(MapGeneration.RoomName.Outside);

            Map.Get.Cassie("danger . outside zone emergency termination sequence activated .", false);
            yield return Timing.WaitForSeconds(5f);

            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                OutsideRoom.ChangeRoomLightColor(new Color(0.5f, 0, 0));
                yield return Timing.WaitForSeconds(0.5f);
                OutsideRoom.ChangeRoomLightColor(new Color(1, 0, 0));
                yield return Timing.WaitForSeconds(0.5f);
                waitforready--;
            }

            int throwcount = 0;
            while (isAirBombCurrently)
            {
                List<Vector3> randampos = Data.AirbombPos.OrderBy(x => Guid.NewGuid()).ToList();
                foreach (var pos in randampos)
                {
                    Map.Get.SpawnGrenade(pos, Vector3.zero, 0.1f);
                    yield return Timing.WaitForSeconds(0.1f);
                }
                throwcount++;
                if (limit != -1 && limit <= throwcount)
                {
                    isAirBombCurrently = false;
                    break;
                }
                yield return Timing.WaitForSeconds(0.25f);
            }
            OutsideRoom.ChangeRoomLightColor(new Color(1, 0, 0), false);
            Map.Get.Cassie("outside zone termination completed .", false);
            yield break;
        }


        /// <summary>
        /// play ambient sound ​to all players
        /// </summary>
        /// <param name="id">the id of the sound</param>
        [API]
        public static void PlayAmbientSound(int id)
        {
            PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
        }


        /// <summary>
        /// Get the total voltage of the generators
        /// </summary>
        /// <returns>1000 for 1 generator engaged</returns>
        [API]
        public static int GetVoltage()
        {
            float totalvoltagefloat = 0;
            foreach (var generator in Server.Get.Map.Generators)
                totalvoltagefloat += generator.generator._currentTime / generator.generator._totalActivationTime * 1000;
            return (int)totalvoltagefloat;
        }


        /// <summary>
        /// Creat new NTF name Unit
        /// </summary>
        [API]
        public static string GenerateNtfUnitName()
        {
            var combi = typeof(UnitNamingRule).GetFieldOrPropertyValue<List<string>>("UsedCombinations");
            string regular;
            do
            {
                var arrayOfValues = typeof(NineTailedFoxNamingRule).GetFieldOrPropertyValue<string[]>("PossibleCodes");
                regular = arrayOfValues[UnityEngine.Random.Range(0, arrayOfValues.Length)] + "-" + UnityEngine.Random.Range(1, 20).ToString("00");
            }
            while (combi.Contains(regular));
            combi.Add(regular);
            return regular;
        }

        /// <summary>
        /// Reset all color of the room light
        /// </summary>
        public static void ResetRoomsLightColor()
        {
            foreach (Room room in SynapseController.Server.Map.Rooms)
                room.ChangeRoomLightColor(new Color(1, 0, 0), false);
        }

        /// <summary>
        /// Change the color of all room light
        /// </summary>
        /// <param name="color">The new color</param>
        public static void ChangeRoomsLightColor(Color color)
        {
            foreach (Room room in SynapseController.Server.Map.Rooms)
                room.ChangeRoomLightColor(color);
        }

        /// <summary>
        /// if the player can see a gameobject
        /// </summary>
        /// <param name="camera">The tested camera</param>
        /// <param name="obj">The Tested GameObject</param>
        /// <returns>True if hi can see it, false if hi cant</returns>
        [Unstable] // change this to a ray cast ?
        public static bool IsTargetVisible(UnityEngine.Camera camera, GameObject obj)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            var point = obj.transform.position;
            foreach (var plan in planes)
            {
                if (plan.GetDistanceToPoint(point) < 0)
                    return false;
            }
            return true;
        }

        public static DamageHandlerBase.HandlerOutput DoDamage(DamageHandlerBase handler, Player victim, bool allowFF = true)
        {
            if (handler is AttackerDamageHandler atkHandler)
            {
                var attacker = atkHandler.Attacker.Hub.GetPlayer();
                if (!allowFF && !SynapseExtensions.GetHarmPermission(attacker, victim))
                    return DamageHandlerBase.HandlerOutput.Nothing;

                atkHandler.ForceFullFriendlyFire = allowFF;
                if (victim == attacker && atkHandler.AllowSelfDamage)
                    atkHandler.ForceFullFriendlyFire = true;
            }
            return handler.ApplyDamage(victim.Hub);
        }
    }

    public static class Audio
    {




        public static bool IsPatch = false;
        public static void Patch()
        { 
            if (IsPatch) return;
            IsPatch = true;
            var instance = new Harmony("VT_Referance.Patch.VT_Patch");
            instance.PatchAll();
        }
        
    }
}
