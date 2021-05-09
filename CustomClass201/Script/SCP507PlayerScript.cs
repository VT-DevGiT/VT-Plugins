using CustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP507Script : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.SCP, (int)TeamID.CHI, (int)TeamID.SHA };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP507;

        protected override string RoleName => PluginClass.ConfigSCP507.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP507;

        protected override void AditionalInit()
        {
            Player.gameObject.AddComponent<SCP507>();
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            Map.Get.AnnounceScpDeath("5 0 7");
            if (Player.gameObject.GetComponent<SCP507>() != null)
                Player.gameObject.GetComponent<SCP507>().Kill();
        }
    }
}
