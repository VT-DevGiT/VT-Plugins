using VTCustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class SCP999Script : BasePlayerScript, IScpDeathAnnonce
    {
        public string ScpName => "9 9 9";
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP999;

        protected override string RoleName => Plugin.ConfigSCP999.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP999;

        protected override void AditionalInit()
        {
            Player.GodMode = true;
            Aura aura = ActiveComponent<Aura>();
            {
                aura.PlayerEffect = Effect.Disabled;
                aura.TargetEffect = Effect.ArtificialRegen;
                aura.HerHp = Plugin.ConfigSCP999.HealHp;
                aura.Distance = Plugin.ConfigSCP999.Distance;
            }
            Timing.CallDelayed(1f, () =>
            {
                Player.Scale = new Vector3(1f, 0.3f, 1f);
            });
        }
        public override void DeSpawn()
        {
            Player.GodMode = false;
            KillComponent<Aura>();
            Player.Scale = Vector3.one;
            base.DeSpawn();
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
        }
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }
    }
}
