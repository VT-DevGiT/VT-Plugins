using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class ICLeaderScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHILeader;

        protected override string RoleName => PluginClass.ConfigCHILeader.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHILeader;

        private DateTime lastPower = DateTime.Now;
        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Respawn && (DateTime.Now - lastPower).TotalSeconds > PluginClass.ConfigCHILeader.Cooldown)
            {
                List<Player> spawnPlayer = new List<Player>();
                spawnPlayer.AddRange(Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Spectator && !p.OverWatch));
                Server.Get.TeamManager.SpawnTeam((int)Team.CHI, spawnPlayer);
                lastPower = DateTime.Now;
            }
            else if (power == PowerType.Respawn)
                Reponse.Cooldown(Player, lastPower, PluginClass.ConfigCHILeader.Cooldown);
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
                CallPower(PowerType.Respawn);
            }
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.PointeCreuses(Player);
        }
    }
}
