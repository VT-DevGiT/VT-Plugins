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
    public class CHIExpertPyrotechnieScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        
        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiExpertPyrotechnie;

        protected override string RoleName => PluginClass.ConfigCHIExpertPyrotechnie.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIExpertPyrotechnie;

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
