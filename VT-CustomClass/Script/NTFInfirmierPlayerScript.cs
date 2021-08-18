using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.PlayerScript;
using static VT_Referance.Variable.Data;
using System;

namespace VTCustomClass.PlayerScript
{
    public class NTFInfirmierScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.MTFenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfInfirmier;

        protected override string RoleName => Plugin.ConfigNTFInfirmier.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigNTFInfirmier;

        private DateTime lastPower = DateTime.Now.AddSeconds(-Plugin.ConfigNTFInfirmier.Cooldown);
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.HollowBullet(Player);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower((int)PowerType.Defibrillation);
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.Defibrillation)
            { 
                if ((DateTime.Now - lastPower).TotalSeconds > Plugin.ConfigNTFInfirmier.Cooldown)
                {
                    Player corpseowner = Methods.GetPlayercoprs(Player, 2.5f);
                    if (Methods.IsWasScpRole(corpseowner) == false)
                    { 
                        corpseowner.RoleID = Data.PlayerRole[corpseowner];
                        corpseowner.Position = corpseowner.DeathPosition;
                        corpseowner.Inventory.Clear();
                        lastPower = DateTime.Now;
                    }
                }
                else Reponse.Cooldown(Player, lastPower, Plugin.ConfigNTFInfirmier.Cooldown);
            }
            else return false;
            return false;
        }
    }
}
