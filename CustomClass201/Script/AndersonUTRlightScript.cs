using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class AndersonUTRlightScript : BaseUTRcript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.AndersneRobotic };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.CHI };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.AndersonUTRlight;

        protected override string RoleName => PluginClass.ConfigAndersonUTRlight.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigAndersonUTRlight;
    }
}
