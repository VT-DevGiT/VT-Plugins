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
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;
        
        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosConscript;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosExpertPyrotechnie;

        protected override string RoleName => Plugin.ConfigCHIExpertPyrotechnie.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigCHIExpertPyrotechnie;

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
            if (ev.Victim == Player && ev.HitInfo.Tool == DamageTypes.Grenade)
            {
                ev.DamageAmount = 100;
                ev.Allow = false;
            }
        }
    }
}
