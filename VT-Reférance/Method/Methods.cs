using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Variable;

namespace VT_Referance.Method
{
    public static class Methods
    {

        
        /// <summary>
        /// return a player according to the nearest corpse within the limit of a sphere
        /// </summary>
        /// <param name="player">center of the sphere</param>
        /// <param name="rayon">radius of the sphere</param>
        /// <returns></returns>
        public static Player GetPlayercoprs(Player player, float rayon)
        {
           // Physics.OverlapSphere(player.Position, 3f).Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
            List<Collider> colliders = Physics.OverlapSphere(player.Position, rayon)
                .Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
            colliders.Sort((Collider x, Collider y) =>
            {
                return Vector3.Distance(x.gameObject.transform.position, player.Position)
                .CompareTo(Vector3.Distance(y.gameObject.transform.position, player.Position));
            });
 
            if (colliders.Count == 0)
                return null;

            Ragdoll doll = colliders[0].gameObject.GetComponentInParent<Ragdoll>();
            if (doll == null)
                return null;

            Player owner = Server.Get.Players.FirstOrDefault(p => p.PlayerId == doll.owner.PlayerId);
            

            if (owner != null && owner.RoleID == (int)RoleType.Spectator)
            {
                UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                return owner;
            }
            return null;
        }
        
        
        /// <summary>
        /// True if the last role of the played is in the SCP team or null if the player had no role
        /// </summary>
        /// <param name="player">the player you want tested</param>
        /// <returns></returns>
        public static bool? IsScpRole(Player player)
        {
            if (player != null && Dictionary.PlayerRole.ContainsKey(player))
            {
                int roleId = Dictionary.PlayerRole[player];

                if (_scpRole.Contains(roleId)) 
                    return true;
                else if (Server.Get.RoleManager.IsIDRegistered(roleId))
                    return Server.Get.RoleManager.GetCustomRole(roleId).GetTeamID() == (int)TeamID.SCP;
                else return null;
            }
            return null;
        }
        
        //list of the SCP Role.
        private static List<int> _scpRole = new List<int>()
        { 
            (int)RoleID.Scp0492,(int)RoleID.Scp079,(int)RoleID.Scp096,
            (int)RoleID.Scp106, (int)RoleID.Scp173,(int)RoleID.Scp93953,
            (int)RoleID.Scp93989, (int)RoleID.Scp049
        };

        
        /// <summary>
        /// if there is an active Air Bombardment or which is starting 
        /// </summary>
        public static bool isAirBombCurrently = false;


        /// <summary>
        /// Start Air Bombardment that detonates grenades all over the outer area
        /// </summary>
        /// <param name="waitforready">time before the start</param>
        /// <param name="limit">if set to -1 it continues indefinitely</param>
        /// <returns></returns>
        public static IEnumerator<float> AirSupportBomb(int waitforready = 7, int limit = -1)
        {
            if (isAirBombCurrently)
            {
                yield break;
            }
            else
                isAirBombCurrently = true;

            Map.Get.Cassie("danger . outside zone emergency termination sequence activated .", false);
            yield return Timing.WaitForSeconds(5f);

            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                waitforready--;
                yield return Timing.WaitForSeconds(1f);
            }

            int throwcount = 0;
            while (isAirBombCurrently)
            {
                List<Vector3> randampos = OutsideRandomAirbombPos.Load().OrderBy(x => Guid.NewGuid()).ToList();
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

            Map.Get.Cassie("outside zone termination completed .", false);
            yield break;
        }


        static private AmbientSoundPlayer _Song = PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>();
        /// <summary>
        /// play ambient sound ​to all players
        /// </summary>
        /// <param name="id">the id of the sound</param>
        public static void PlayAmbientSound(int id)
        {
            _Song.CallMethod("RpcPlaySound", id);
        }


        /// <summary>
        /// Get the total voltage of the generators, 1000 for 1 generator finish
        /// </summary>
        /// <returns></returns>
        public static int GetVoltage()
        {
            float totalvoltagefloat = 0;
            foreach (var i in Generator079.Generators)
            {
                totalvoltagefloat += i.localVoltage;
            }
            totalvoltagefloat *= 1000;
            return (int)totalvoltagefloat;
        }

    }
}
