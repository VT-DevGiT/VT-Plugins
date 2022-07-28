using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;

namespace VTCustomClass.PlayerScript
{
    public class DirecteurSiteScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.VIPennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.VIPally.ToList();

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.VIP;

        protected override int RoleId => (int)RoleID.DirecteurSite;

        protected override string RoleName => Plugin.Instance.Config.DirectorName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.DirectorConfig;
    }
}
