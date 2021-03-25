using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using System;

namespace CustomClass.PlayerScript
{
    public class CHIInfirmierScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { (int)TeamID.CHI, (int)TeamID.CDP } : new List<int> { };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.CHIInfirmier;

        protected override string RoleName => PluginClass.ConfigCHIInfirmier.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIInfirmier;

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
                    if ((DateTime.Now - lastPower).TotalSeconds > PluginClass.ConfigCHIHacker.CoolDownDoor)
                    {
                        corpseowner.RoleID = Dictionary.PlayerRole[corpseowner];
                        corpseowner.Position = corpseowner.DeathPosition;
                        lastPower = DateTime.Now;
                    }
                    else
                        Reponse.Cooldown(Player, lastPower, PluginClass.ConfigCHIInfirmier.CoolDown);
                }
                return true;
            }
            return false;
        }

        private DateTime lastPower = DateTime.Now.AddSeconds(-PluginClass.ConfigCHIHacker.CoolDownDoor);
    }
}
