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
    public class FoundationUTRScript : BaseUTRcript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.FoundationUTR;

        protected override string RoleName => PluginClass.ConfigFoundationUTR.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigFoundationUTR;
    }
}
