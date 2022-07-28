using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Extension;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class SCP999Script : AbstractRole, IScpDeathAnnonce
    {
        public string ScpName => "9 9 9";
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP999;

        protected override string RoleName => Plugin.Instance.Config.Scp999Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp999Config;

        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            Player.GodMode = true;
            Aura aura = ActiveComponent<Aura>();
            {
                aura.playerEffect = Effect.Disabled;
                aura.targetAddHp = Plugin.Instance.Config.Scp999HealHp;
                aura.distance = Plugin.Instance.Config.Scp999DistanceForHeal;
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
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
        }

        private static void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player?.CustomRole is SCP999Script)
                ev.Allow = false;
        }
    }
}
