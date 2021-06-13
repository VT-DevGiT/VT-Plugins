using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class CHILeaderScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiLeader;

        protected override string RoleName => Plugin.ConfigCHILeader.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigCHILeader;

        private DateTime lastPower = DateTime.Now;
        public override bool CallPower(int  power)
        {
            if (power == (int)PowerType.Respawn && (DateTime.Now - lastPower).TotalSeconds > Plugin.ConfigCHILeader.Cooldown)
            {
                List<Player> spawnPlayer = new List<Player>();
                spawnPlayer.AddRange(Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Spectator && !p.OverWatch));
                Server.Get.TeamManager.SpawnTeam((int)TeamID.CHI, spawnPlayer);
                lastPower = DateTime.Now;
            }
            else if (power == (int)PowerType.Respawn)
                Reponse.Cooldown(Player, lastPower, Plugin.ConfigCHILeader.Cooldown);
            else return false;
            return true;
        }

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

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
            {
                CallPower((int)PowerType.Respawn);
            }
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.HollowBullet(Player);
        }
    }
}
