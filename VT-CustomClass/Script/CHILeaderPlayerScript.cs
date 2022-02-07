using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHILeaderScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosMarauder;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosLeader;

        protected override string RoleName => Plugin.Instance.Config.LeaderName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.LeaderConfig;

        private DateTime lastPower = DateTime.Now;
        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.Respawn)
            {
                if ((DateTime.Now - lastPower).TotalSeconds < Plugin.Instance.Config.LeaderCooldown)
                {
                    message = Reponse.Cooldown(lastPower, Plugin.Instance.Config.LeaderCooldown);
                    return false;
                }

                List<Player> spawnPlayer = new List<Player>();
                spawnPlayer.AddRange(Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Spectator && !p.OverWatch));
                Server.Get.TeamManager.SpawnTeam((int)TeamID.CHI, spawnPlayer);
                lastPower = DateTime.Now;

                message = "the allys are here! ...maby ?";
                return true;
            }
            message = "You ave only one power";
            return false;
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }

        private static void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.CustomRole is CHILeaderScript)
                ev.HollowBullet();
        }
    }
}
