using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class NTFVirologueScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.MTF, (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.NTFVirologue;

        protected override string RoleName => PluginClass.ConfigNTFVirologue.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFVirologue;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }

        protected override void AditionalInit()
        {
            if (Player.UserId == "steam@76561198880515778")
                Player.DisplayInfo = $"Agent du CDA(Virologue) {Player.UnitName}";
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.Destabilisantes(Player);
        }
    }
}
