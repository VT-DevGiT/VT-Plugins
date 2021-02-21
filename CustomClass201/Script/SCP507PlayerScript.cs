using CustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class SCP507Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.SCP, (int)Team.CHI };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)Team.RSC;

        protected override int RoleId => (int)MoreClasseID.SCP507;

        protected override string RoleName => PluginClass.ConfigSCP507.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP507;

        protected override void AditionalInit()
        {
            if(Player == null)
                Server.Get.Logger.Info($"Player null");
            if (Player.gameObject == null)
                Server.Get.Logger.Info($"gameObject null");
            Server.Get.Logger.Info($"1");
            Player.gameObject.AddComponent<SCP507>();
            Server.Get.Logger.Info($"2");
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("5 0 7");
            if (Player.gameObject.GetComponent<SCP507>() != null)
                Player.gameObject.GetComponent<SCP507>().Destroy();
        }
    }
}
