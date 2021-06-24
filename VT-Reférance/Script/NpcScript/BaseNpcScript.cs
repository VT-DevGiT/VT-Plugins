using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Behaviour;


namespace VT_Referance.NpcScript
{
    public class BaseNpcScript : Dummy
    {
        public static Dictionary<uint, BaseNpcScript> NpcList { get; private set; } = new Dictionary<uint, BaseNpcScript>();
        static uint highestId = 0;

        public static void clear()
        {
            highestId = 0;
            NpcList.Clear();
        }

        public NpcControlMouvement Mouvement;
        
        public ZoneType CurentZone
        {
            get { return Player.Zone; }
        }


        public uint Id;
        public BaseNpcScript(Vector3 pos, Vector2 rot, RoleType role, string name, string badgetext = "", string badgecolor = "") : base(pos, rot, role, name, badgetext, badgecolor)
        {
            Mouvement = GameObject.AddComponent<NpcControlMouvement>();
            highestId++;
            NpcList.Add(highestId, this);
            Id = highestId;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == Player)
                NpcList.Remove(Id);
        }

        public virtual PlayerMovementState MouventState()
        {
            return PlayerMovementState.Walking;
        }

    }
}
