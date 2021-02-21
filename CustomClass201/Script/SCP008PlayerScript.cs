using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)MoreClasseID.SCP008;

        protected override string RoleName => PluginClass.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP008;

        protected override void AditionalInit()
        {
            Player.gameObject.AddComponent<Aura>();
            Player.gameObject.GetComponent<Aura>().PlayerEffect = Effect.ArtificialRegen;
            Player.gameObject.GetComponent<Aura>().TargetEffect = Effect.Poisoned;
            Player.gameObject.GetComponent<Aura>().MoiHealHp = PluginClass.ConfigSCP008.HealHp;
            Player.gameObject.GetComponent<Aura>().Distance = PluginClass.ConfigSCP008.Distance;
            
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            if (Player.gameObject.GetComponent<Aura>() != null)
                Player.gameObject.GetComponent<Aura>().Destroy();
            Map.Get.AnnounceScpDeath("0 0 8");
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
            {
                CallPower(PowerType.Zombifaction);
            }
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Zombifaction)
            {
                Player corpseowner = GetPlayercoprs(Player, 2.5f);
                if (corpseowner != null)
                    corpseowner.RoleID = (int)RoleType.Scp0492;
                return true;
            }
            return false;
        }

        public static Player GetPlayercoprs(Player player, float rayon)
        {
            foreach(var mort in Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Spectator))
            {
                if (Vector3.Distance(mort.DeathPosition, player.Position) < rayon)
                    return mort;
            }
            return null;
        }

    }
}
