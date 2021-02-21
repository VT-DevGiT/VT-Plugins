using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class SCP966cript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)MoreClasseID.SCP966;

        protected override string RoleName => PluginClass.ConfigSCP966.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP966;

        protected override void AditionalInit()
        {
            Player.Invisible = true;
            Player.gameObject.AddComponent<Invisible>();
            Player.GiveEffect(Effect.Scp268, 2);
            
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("9 6 6");
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            if (Player.gameObject.GetComponent<Invisible>() != null)
                Player.gameObject.GetComponent<Invisible>().Destroy();
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            if (ev.Player == Player)
            {
                ev.Player.GiveEffect(Effect.Scp268, 2);
            }
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
            {
                ev.Victim.GiveEffect(Effect.Concussed, 1, 5);
                ev.Victim.GiveEffect(Effect.Amnesia, 1, 5);
                ev.Victim.GiveEffect(Effect.Deafened, 1, 5);
                ev.Victim.GiveEffect(Effect.Exhausted, 1, 5);
                ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 3);
                ev.Allow = false;
            }
        }
    }
}
