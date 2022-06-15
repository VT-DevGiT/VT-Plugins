using Synapse.Api;
using System;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Behaviour;

namespace VT_PNJ.API
{
    public class NpcControlCortext : RepeatingBehaviour
    {
        public readonly BaseNpcScript npc;
        public readonly Player player;

        public Transform target = null;
        public float maxDistance = 10f;
        public bool TargetIsVisible { get; set; }

        [Range(0f, 360f)]
        public float angle = 45f;



        public NpcControlCortext()
        {
            this.RefreshTime = 10;
            player = gameObject.GetPlayer();
            npc = (BaseNpcScript)Map.Get.Dummies.FirstOrDefault(x => x.Player == player);
        }

        protected override void BehaviourAction()
        {
            TargetIsVisible = CheckVisibility();
        }

        public bool CheckVisibilityToPoint(Vector3 worldPoint)
        {
            Vector3 directionToTarget = worldPoint - transform.position;
            float degressToTarget = Vector3.Angle(transform.forward, directionToTarget);
            bool withinArc = degressToTarget < (angle / 2);

            if (!withinArc) return false;

            float distanceToTarget = directionToTarget.magnitude;
            float rayDisantce = Mathf.Min(maxDistance, distanceToTarget);
            Ray ray = new Ray(transform.position, directionToTarget);

            if (Physics.Raycast(ray, out RaycastHit Hit, rayDisantce))
                if (Hit.collider.transform == target) return true; else return false;
            
            return true;
        }

        public bool CheckVisibility()
        {
            Vector3 directionToTarget = target.position - transform.position;
            float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);
            bool withinArc = degreesToTarget < (angle / 2);

            if (!withinArc) return false;

            float distanceToTarget = directionToTarget.magnitude;
            float rayDistance = Mathf.Min(maxDistance, distanceToTarget);
            Ray ray = new Ray(transform.position, directionToTarget);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
                if (hit.collider.transform == target)
                    return true;
            return false;
        }

        protected override void Start()
        {
            //base.Start();
        }
    }
}
