using CustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP999Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP999;

        protected override string RoleName => PluginClass.ConfigSCP999.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP999;

        protected override void AditionalInit()
        {
            Aura aura = ActiveComponent<Aura>();
            {
                aura.PlayerEffect = Effect.Disabled;
                aura.TargetEffect = Effect.ArtificialRegen;
                aura.HerHp = PluginClass.ConfigSCP999.HealHp;
                aura.Distance = PluginClass.ConfigSCP999.Distance;
            }
            Timing.CallDelayed(1f, () =>
            {
                Player.Scale = new Vector3(1f, 0.3f, 1f);
            });
        }
        public override void DeSpawn()
        {
            InactiveComponent<Aura>();
            Player.Scale = Vector3.one;
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("9 9 9");
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
        }
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player)
                ev.Allow = false;
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }
    }
}
