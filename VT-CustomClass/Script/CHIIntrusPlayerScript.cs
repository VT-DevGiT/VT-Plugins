
using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class CHIIntrusScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiIntrus;

        protected override string RoleName => PluginClass.ConfigCHIntrus.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIntrus;

        protected override bool SetDisplayInfo => false;
    }
}
