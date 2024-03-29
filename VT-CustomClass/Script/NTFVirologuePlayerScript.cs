﻿using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;

namespace VTCustomClass.PlayerScript
{
    public class NTFVirologueScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfVirologue;

        protected override string RoleName => Plugin.Instance.Config.VirologueName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.VirologueConfig;

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }

        private static void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.CustomRole is NTFVirologueScript)
                ev.ChemicalBullet();
        }
    }
}
