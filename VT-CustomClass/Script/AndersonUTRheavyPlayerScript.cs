using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class AndersonUTRheavyScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamGroupe.ANDennemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.ANDally;

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonUTRheavy;

        protected override string RoleName => PluginClass.ConfigAndersonUTRheavy.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigAndersonUTRheavy;
    }
}
