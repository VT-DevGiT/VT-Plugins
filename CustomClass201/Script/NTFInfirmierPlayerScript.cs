using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;

namespace CustomClass.PlayerScript
{
    public class NTFInfirmierScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)TeamID.CDM, (int)TeamID.NTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)RoleID.NTFInfirmier;

        protected override string RoleName => PluginClass.ConfigNTFInfirmier.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigNTFInfirmier;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.PointeCreuses(Player);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower(PowerType.Defibrilatcion);
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Defibrilatcion)
            {
                Player corpseowner = Methods.GetPlayercoprs(Player, 2.5f);
                if (Methods.IsScpRole(corpseowner) == false)
                { 
                    corpseowner.RoleID = Dictionary.PlayerRole[corpseowner];
                    corpseowner.Position = corpseowner.DeathPosition;
                }
                return true;
            }
            return false;
        }
    }
}
