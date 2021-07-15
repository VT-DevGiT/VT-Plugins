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

        protected override void AditionalInit()
        {
            Player.GetOrAddComponent<Invisible>().enabled = true;
        }

        protected override void Event()
        {
            Server.Get.Events.Scp.ScpAttackEvent += OnAttack;
        }

        private void OnAttack(ScpAttackEventArgs ev)
        {
            if (ev.Allow && ev.Scp == Player)
            {
                ev.Allow = false;
                ev.Target.Hurt(20, DamageTypes.Scp0492, ev.Scp);
                if (!ev.Target.IsUTR())
                {
                    ev.Target.GiveEffect(Effect.Concussed, 1, 10);
                    ev.Target.GiveEffect(Effect.Deafened, 1, 10);
                    ev.Target.GiveEffect(Effect.Exhausted, 1, 10);
                    ev.Target.GiveEffect(Effect.Asphyxiated, 1, 5);
                }
            }
        }

        public override void DeSpawn()
        {
            Server.Get.Events.Scp.ScpAttackEvent -= OnAttack;
            Player.GetOrAddComponent<Invisible>().Kill();
            base.DeSpawn();
        }
    }
}
