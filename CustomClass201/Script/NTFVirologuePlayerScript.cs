using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class NTFVirologueScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP, (int)TeamID.SHA, (int)TeamID.AND };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.NtfVirologue;

        protected override string RoleName => PluginClass.ConfigNTFVirologue.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFVirologue;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }

        protected override void AditionalInit()
        {
            if (Player.UserId == "76561198880515778@steam")
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
                ev.ChemicalBullet(Player);
        }
    }
}
