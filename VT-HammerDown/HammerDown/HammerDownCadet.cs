﻿using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT_HammerDown
{
    public class HammerDownCadet : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfPrivate;

        protected override int RoleTeam => (int)TeamID.CDM;

        protected override int RoleId => (int)RoleID.CdmCadet;

        protected override string RoleName => Plugin.Instance.Config.CadName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.CadConfig;
    } 
}
