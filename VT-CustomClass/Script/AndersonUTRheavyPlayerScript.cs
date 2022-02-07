using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VTCustomClass.PlayerScript
{
    public class AndersonUTRheavyScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamManager.Group.ANDennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.ANDally.ToList();

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonUTRheavy;

        protected override string RoleName => Plugin.Instance.Config.AndUTRHeavyName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.AndUTRHeavyConfig;
    }
}
