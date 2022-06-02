using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VTCustomClass.PlayerScript
{
    public class MTFUTRScript : BaseUTRScript
    {
        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.MTFUTR;

        protected override string RoleName => Plugin.Instance.Config.NtfUTRName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.NtfUTRConfig;

        protected override Color Color => Color.blue; 
    }
}
