using VTCustomClass.Pouvoir;
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
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class SCP507Script : BasePlayerScript, IScpRole
    {
        public string ScpName => "5 0 7";

        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.NetralSCPennemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP507;

        protected override string RoleName => Plugin.ConfigSCP507.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP507;

        protected override void AditionalInit()
        {
            Player.gameObject.AddComponent<SCP507>();
        }
        public override void DeSpawn()
        {
            base.DeSpawn();
            if (Player.gameObject.GetComponent<SCP507>() != null)
                Player.gameObject.GetComponent<SCP507>().Kill();
        }
    }
}
