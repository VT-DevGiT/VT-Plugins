using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using VT_Referance.NpcScript;

namespace VT_Referance.Behaviour
{
    class NpcControlCortext : BaseRepeatingBehaviour
    {
        BaseNpcScript npc;
        public NavMeshAgent agent;
        public LayerMask ground;
        Vector3? _Goto;

        protected override void BehaviourAction() { }

        protected override void Start()
        {
            npc = (BaseNpcScript)Map.Get.Dummies.FirstOrDefault(n => n.GameObject == gameObject);
            agent = GetComponent<NavMeshAgent>();
            base.Start();
        }

        public Vector3? Goto
        {
            get { return _Goto; }
            set 
            {
                if (value == null) { enabled = false; return; }
                if (Physics.Raycast((Vector3)value, -transform.up, 2f, ground))
                    throw new Exception($"imposible d'accéder à cet zone PNJ ({npc.Id})");
                agent.SetDestination((Vector3)value);
                enabled = true;
            }
        }


    }
}
