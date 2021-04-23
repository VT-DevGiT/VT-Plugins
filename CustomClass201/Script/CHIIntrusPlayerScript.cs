
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

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiIntrus;

        protected override string RoleName => PluginClass.ConfigCHIntrus.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIntrus;

        protected override bool SetDisplayInfo => false;
    }
}
