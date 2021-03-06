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
using VT_Referance.Variable;
using VT_Referance.Method;

namespace CustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)RoleID.SCP008;

        protected override string RoleName => PluginClass.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP008;

        protected override void AditionalInit()
        {
            Aura aura = GetComponent<Aura>();
            {
                aura.PlayerEffect = Effect.ArtificialRegen;
                aura.TargetEffect = Effect.Poisoned;
                aura.Intencty = 6;
                aura.MoiHealHp = PluginClass.ConfigSCP008.HealHp;
                aura.LuiHealHp = -PluginClass.ConfigSCP008.DomageHp;
                aura.Distance = PluginClass.ConfigSCP008.Distance;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            InactiveComponent<Aura>();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Map.Get.AnnounceScpDeath("0 0 8");
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower(PowerType.Zombifaction);
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Zombifaction)
            {
                Player corpseowner = VT_Referance.Method.Methods.GetPlayercoprs(Player, 2.5f);
                if (Methods.IsScpRole(corpseowner) == true)
                    corpseowner.RoleID = (int)RoleType.Scp0492;
                return true;
            }
            return false;
        }
    }
}
