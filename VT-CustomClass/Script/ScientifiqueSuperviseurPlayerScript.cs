using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class ScientifiqueSuperviseurScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.RSCennemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.RSCally;

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.ScientifiqueSuperviseur;

        protected override string RoleName => PluginClass.ConfigScientifiqueSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigScientifiqueSuperviseur;
    }
}
