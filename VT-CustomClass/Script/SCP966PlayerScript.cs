using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.PlayerScript;
using static VT_Referance.Variable.Data;
using System;

namespace VTCustomClass.PlayerScript
{
    public class SCP966cript : BasePlayerScript, IScpRole
    {
        public string ScpName => "9 6 6";
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.SCPenemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP966;

        protected override string RoleName => Plugin.ConfigSCP966.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP966;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnAttack;
        }

        private void OnAttack(PlayerDamageEventArgs ev)
        {
            if (ev.Allow && ev.Allow == Player && ev.HitInfo.GetDamageType() == DamageTypes.Scp0492)
            {
                ev.DamageAmount = 20;
                if (!ev.Victim.IsUTR())
                {
                    ev.Victim.GiveEffect(Effect.Concussed, 1, 10);
                    ev.Victim.GiveEffect(Effect.Deafened, 1, 10);
                    ev.Victim.GiveEffect(Effect.Exhausted, 1, 10);
                    ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 5);
                }
            }
        }

        public override void DeSpawn()
        {
            Server.Get.Events.Player.PlayerDamageEvent -= OnAttack;
            base.DeSpawn();
        }
    }
}
