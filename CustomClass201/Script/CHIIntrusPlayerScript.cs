using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class CHIIntrusScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { (int)TeamID.CHI, (int)TeamID.CDP } : new List<int> { };

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.CHIIntrus;

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
                CallPower(PowerType.ChangeRole);
            else if (ev.Killer == Player)
                ev.Victim.OpenReportWindow(PluginClass.PluginTranslation.ActiveTranslation.KilledMessage.Replace("%RoleName%", RoleName));
        }

    }
}
