using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class ICIntrusScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHIIntrus;

        protected override string RoleName => PluginClass.ConfigCHIntrus.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIntrus;

        protected override bool SetDisplayInfo => false;

        public override bool CallPower(PowerType power) 
        {
            if (power == PowerType.ChangeRole && Player.RoleType == RoleType.Scientist)
            {
                Player.ChangeRoleAtPosition(RoleType.ChaosInsurgency);
                Player.MaxHealth = Config.GetConfigValue("Health", 120);
                return true;
            }
            return false;


        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDeathEvent -= OnDeath;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
            {
                CallPower(PowerType.ChangeRole);
            }
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == Player)
            {
                CallPower(PowerType.ChangeRole);
            }
        }

    }
}
