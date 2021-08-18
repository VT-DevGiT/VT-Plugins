using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;


namespace VT_Referance.NpcScript
{
    public class BaseNpcScript : Dummy
    {
        public static Dictionary<uint, BaseNpcScript> NpcList { get; private set; } = new Dictionary<uint, BaseNpcScript>();
        public uint Id;

        public BaseNpcScript(Vector3 pos, Vector2 rot, RoleType role, string name, string badgetext = "", string badgecolor = "") : base(pos, rot, role, name, badgetext, badgecolor)
        {
            CtrMouvement = GameObject.AddComponent<NpcControlMouvement>();
            uint newId;
            if (NpcList.Any()) newId = NpcList.Keys.OrderByDescending(p => p).First() + 1;
            else newId = 1;
            NpcList.Add(newId, this); Id = newId;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }


        public NpcControlMouvement CtrMouvement;

        protected virtual void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == Player)
            { 
                NpcList.Remove(Id);
                Server.Get.Events.Player.PlayerDeathEvent -= OnDeath;
            }
        }

        public virtual PlayerMovementState MouventState()
        {
            return PlayerMovementState.Walking;
        }

    }
}
