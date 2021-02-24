using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomClass.Pouvoir
{
    class Utile
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
            if (PluginClass.Plugin.PlayerRole.ContainsKey(owner))
            {
                int roleId = PluginClass.Plugin.PlayerRole[owner];
                return scpRole.Contains(roleId);
            }
            return null;
        }

        public static List<int> scpRole = new List<int>()
        {
            (int)MoreClasseID.SCP008,(int)MoreClasseID.SCP966,(int)MoreClasseID.SCP682,
            (int)MoreClasseID.SCP1048,(int)MoreClasseID.SCP953,(int)RoleType.Scp049
            ,(int)RoleType.Scp0492,(int)RoleType.Scp079,(int)RoleType.Scp096,(int)RoleType.Scp106
            ,(int)RoleType.Scp173,(int)RoleType.Scp93953,(int)RoleType.Scp93989, 56, 79
        };
    }
}
