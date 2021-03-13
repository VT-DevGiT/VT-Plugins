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
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.RSC, (int)TeamID.CDP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP008;

        protected override string RoleName => PluginClass.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP008;

        protected override void AditionalInit()
        {
            Aura aura = ActiveComponent<Aura>();
            {
                aura.PlayerEffect = Effect.ArtificialRegen;
                aura.TargetEffect = Effect.Poisoned;
                aura.LuiIntencty = 6;
                aura.LuiTime = 5;
                aura.MoiHealHp = PluginClass.ConfigSCP008.HealHp;
                aura.LuiHealHp = -PluginClass.ConfigSCP008.DomageHp;
                aura.Distance = PluginClass.ConfigSCP008.Distance;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            InactiveComponent<Aura>();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Map.Get.AnnounceScpDeath("0 0 8");
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
            {
                ev.Victim.GiveEffect(Effect.Bleeding, 2, 4);
                ev.DamageAmount = 50;
            }
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
                Server.Get.Logger.Info("Zombifaction");
                Player corpseowner = Methods.GetPlayercoprs(Player, 4);
                Server.Get.Logger.Info(corpseowner?.NickName);
                if (Methods.IsScpRole(corpseowner) == false)
                {
                    corpseowner.RoleID = (int)RoleType.Scp0492;
                    Server.Get.Logger.Info("corpseowner == true");
                }
                return true;
            }
            return false;
        }
    }
}
