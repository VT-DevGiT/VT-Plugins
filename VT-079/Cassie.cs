using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT079
{
    public class DirecteurSiteScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Config.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.Scp079;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => Plugin.CASSIE;

        protected override string RoleName => "CASSIE";

        protected override SerializedPlayerRole Config => new SerializedPlayerRole();

        public override void Spawning()
        {
            Player.Scp079Controller.Level = 6;
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Scp.Scp079.RoomLockdown += Lockdown;
        }

        public static void Lockdown(Scp079RoomLockdownEventArgs ev)
        { 
            if (Server.Get.Players.Any(p => p?.Room == ev.Room && TeamManager.Group.MTFally.Contains(p.TeamID)))
                ev.Result = Scp079EventMisc.InteractionResult.Disallow;
        }

    }
}
