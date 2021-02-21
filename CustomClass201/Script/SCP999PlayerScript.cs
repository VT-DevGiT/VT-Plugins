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

namespace CustomClass.PlayerScript
{
    public class SCP999Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RIP;

        protected override int RoleId => (int)MoreClasseID.SCP999;

        protected override string RoleName => PluginClass.ConfigSCP999.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP999;

        protected override void AditionalInit()
        {
            Player.gameObject.AddComponent<Aura>();
            Player.gameObject.GetComponent<Aura>().PlayerEffect = Effect.Disabled;
            Player.gameObject.GetComponent<Aura>().TargetEffect = Effect.ArtificialRegen;
            Player.gameObject.GetComponent<Aura>().LuiHealHp = PluginClass.ConfigSCP999.HealHp;
            Player.gameObject.GetComponent<Aura>().Distance = PluginClass.ConfigSCP999.Distance;
            Timing.CallDelayed(1f, () =>
            {
                Player.Scale *= 0.74f;
            });
        }
        public override void DeSpawn()
        {
            Player.Scale = Vector3.one;
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("9 9 9");
            Server.Get.Events.Player.PlayerSetClassEvent -= OnSetClass;
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
        }
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }


        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Player == Player && Player.gameObject.GetComponent<Aura>() != null)
            {
                Player.gameObject.GetComponent<Aura>().Destroy();
            }
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
