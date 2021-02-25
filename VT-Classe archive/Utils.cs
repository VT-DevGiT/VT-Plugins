using Synapse.Api;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomClass
{
    public static class Utils
    {
        public static Player GetPlayerCorpse(Player player, float rayon)
        {
            List<Collider> colliders = Physics.OverlapSphere(player.Position, rayon).Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();

            colliders.Sort((Collider x, Collider y) =>
            {
                return Vector3.Distance(x.gameObject.transform.position, player.Position).CompareTo(Vector3.Distance(y.gameObject.transform.position, player.Position));
            });

            if (colliders.Count == 0)
            {
                return null;
            }

            Ragdoll doll = colliders[0].gameObject.GetComponentInParent<Ragdoll>();
            return doll.GetPlayer();
        }

        public static bool AttemptRevive(Player player, float rayon)
        {
            Player corpseowner = GetPlayerCorpse(player, rayon);
            if (corpseowner != null && corpseowner.RoleID == (int)RoleType.Spectator)
            {
                bool revived = false;
                // MET UN ROLE ICI
                //corpseowner.ChangeRoleAtPosition((RoleType)TrackingAndMethods.GetPreviousRole(owner), true);
                corpseowner.Inventory.Clear();
                revived = true;
                return true;
            }
            return false;
        }
    }
}
