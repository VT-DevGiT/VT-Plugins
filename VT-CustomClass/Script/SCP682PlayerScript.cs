using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public class SCP682Script : AbstractRole, IScpDeathAnnonce
    {
        public string ScpName => "6 8 2";
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.BerserkSCPennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.BerserkSCPally.ToList();

        protected override RoleType RoleType => RoleType.Scp93953;

        protected override int RoleTeam => (int)TeamID.BerserkSCP;

        protected override int RoleId => (int)RoleID.SCP682;

        protected override string RoleName => Plugin.Instance.Config.Scp682Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp682Config;
    }
}
