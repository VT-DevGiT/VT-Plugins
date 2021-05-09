using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class NTFExpertPyrotechnieScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP, (int)TeamID.SHA, (int)TeamID.AND };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.NtfExpertPyrotechnie;

        protected override string RoleName => PluginClass.ConfigNTFExpertPyrotechnie.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFExpertPyrotechnie;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
        }
        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player && ev.HitInfo.GetDamageType() == DamageTypes.Grenade)
                ev.Allow = false;
        }
    }
}