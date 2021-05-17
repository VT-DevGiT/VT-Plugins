using VTCustomClass.Pouvoir;
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
using VT_Referance.PlayerScript;

namespace VTCustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.SCPenemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP008;

        protected override string RoleName => PluginClass.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP008;
        Aura aura;
        protected override void AditionalInit()
        {
            aura = ActiveComponent<Aura>();
            {
                aura.PlayerEffect = Effect.ArtificialRegen;
                aura.TargetEffect = Effect.Poisoned;
                aura.HerIntencty = 6;
                aura.HerTime = 5;
                aura.MyHp = PluginClass.ConfigSCP008.HealHp;
                aura.HerHp = -PluginClass.ConfigSCP008.DomageHp;
                aura.Distance = PluginClass.ConfigSCP008.Distance;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            aura.PlayerEffect = null;
            aura.TargetEffect = null;
            aura.HerIntencty = 0;
            aura.HerTime = 0;
            aura.MyHp = 0;
            aura.HerHp = 0;
            aura.Distance = 0;
            KillComponent<Aura>();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            if (!Server.Get.Players.Where(p => p.RoleID == (int)RoleID.SCP008).Any())
                Map.Get.GlitchedCassie("ALL SCP 0 0 8 SUCCESSFULLY TERMINATED");
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
                Player corpseowner = Methods.GetPlayercoprs(Player, 4);
                if (Methods.IsScpRole(corpseowner) == false)
                {
                    corpseowner.RoleID = (int)RoleID.SCP008;
                    Player.Health += 100;
                    corpseowner.Position = Player.Position;
                }
                return true;
            }
            return false;
        }
    }
}
