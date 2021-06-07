using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.Variable;

namespace VT_Referance.NpcScript
{
    public class BaseNpcScript : Dummy
    {

        public NpcMouvControler controler;

        public int Id;
        public BaseNpcScript(Vector3 pos, Vector2 rot, RoleType role, string name, string badgetext = "", string badgecolor = "") : base(pos, rot, role, name, badgetext, badgecolor)
        {
            controler = GameObject.AddComponent<NpcMouvControler>();
            Data.Npc.AddNpc(this);
        }

        public virtual PlayerMovementState MouventState()
        {
            return PlayerMovementState.Walking;
        }

    }
}
