using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class NTFExpertReconfinementScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfExpertReconfinement;

        protected override string RoleName => Plugin.ConfigNTFExpertReconfinement.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigNTFExpertReconfinement;

        protected override void Event()
        {
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnTarget;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        public override void DeSpawn()
        {
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent -= OnTarget;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            base.DeSpawn();
        }

        private bool _target096 = false;
        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player && ev.Victim.RoleID == (int)RoleType.Scp096)
            {
                _target096 = true;
            }
        }

        private void OnTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player == Player && !_target096)
            {
                ev.Scp096.Scp096Controller.RemoveTarget(ev.Player);
                ev.Allow = false;
            }
        }
    }
}
