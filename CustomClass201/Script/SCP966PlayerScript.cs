using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
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

        private SynapseItem Chapeau;

        protected override void AditionalInit()
        {
            Player.Invisible = true;
            Chapeau = new SynapseItem(ItemType.SCP268, 0, 0, 0, 0);
            Player.Inventory.AddItem(Chapeau);
            Player.gameObject.AddComponent<Invisible>();
            Player.GiveEffect(Effect.Scp268);
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        public override void DeSpawn()
        {
            Player.Invisible = false;
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("9 6 6");
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            if (Player.gameObject.GetComponent<Invisible>() != null)
                Player.gameObject.GetComponent<Invisible>().Destroy();
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Killer == Player)
            {
                Player.Inventory.Clear();
            }
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player && ev.Victim.RoleID == (int)MoreClasseID.UTR)
            {
                ev.Victim.GiveEffect(Effect.Concussed, 1, 10);
                ev.Victim.GiveEffect(Effect.Amnesia, 1, 10);
                ev.Victim.GiveEffect(Effect.Deafened, 1, 10);
                ev.Victim.GiveEffect(Effect.Exhausted, 1, 10);
                ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 5);
                ev.Allow = false;
            }
        }
    }
}
