using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP966cript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.RSC, (int)TeamID.CDP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP966;

        protected override string RoleName => PluginClass.ConfigSCP966.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP966;

        protected override void AditionalInit()
        {
            Player.Invisible = true;
            SynapseItem Chapeau = new SynapseItem(ItemType.SCP268, 0, 0, 0, 0);
            ActiveComponent<Invisible>();
            Player.Inventory.AddItem(Chapeau);
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        public override void DeSpawn()
        {
            Player.Inventory.Clear();
            Player.Invisible = false;
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("9 6 6");
            InactiveComponent<Invisible>();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
        }


        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player && ev.Victim.RoleID != (int)RoleID.FoundationUTR)
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
