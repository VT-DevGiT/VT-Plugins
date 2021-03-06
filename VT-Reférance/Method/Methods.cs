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
        public static Player GetPlayercoprs(Player player, float rayon)
        {
            List<Collider> colliders = Physics.OverlapSphere(player.Position, 3f)
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
            

            if (owner != null && owner.RoleID != (int)RoleType.Spectator)
            {
                UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                return owner;
            }
            return null;
        }

        public static bool? IsScpRole(Player owner)
        {
            if (owner != null && Dictionary.PlayerRole.ContainsKey(owner))
            {
                int roleId = Dictionary.PlayerRole[owner];
                return scpRole.Contains(roleId);
            }
            return null;
        }

        public static List<int> scpRole = new List<int>()
        {
            (int)RoleID.SCP008,(int)RoleID.SCP966,(int)RoleID.SCP682,
            (int)RoleID.SCP1048,(int)RoleID.SCP953,(int)RoleID.Scp049,
            (int)RoleID.Scp0492,(int)RoleID.Scp079,(int)RoleID.Scp096,
            (int)RoleID.Scp106,(int)RoleID.Scp173,(int)RoleID.Scp93953,
            (int)RoleID.Scp93989,(int)RoleID.Scp056,(int)RoleID.Scp079
        };

        public static bool isAirBombGoing = false;
        public static IEnumerator<float> AirSupportBomb(int waitforready = 7, int limit = -1)
        {
            if (isAirBombGoing)
            {
                yield break;
            }
            else
                isAirBombGoing = true;

            Map.Get.Cassie("danger . outside zone emergency termination sequence activated .", false);
            yield return Timing.WaitForSeconds(5f);

            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                waitforready--;
                yield return Timing.WaitForSeconds(1f);
            }

            int throwcount = 0;
            while (isAirBombGoing)
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
                    isAirBombGoing = false;
                    break;
                }
                yield return Timing.WaitForSeconds(0.25f);
            }

            Map.Get.Cassie("outside zone termination completed .", false);
            yield break;
        }

        public static void PlayAmbientSound(int id)
        {
            PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
        }

        public static int Voltage()
        {
            int totalvoltagefloat = 0;
            foreach (var i in Generator079.Generators)
            {
                totalvoltagefloat += (int)i.localVoltage;
            }
            totalvoltagefloat *= 1000;
            return totalvoltagefloat;
        }

    }
}
