using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT_AndersonRobotic
{
    public class AndersonRoboticLeaderScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessageAnderson;

        protected override List<int> EnemysList => TeamManager.Group.ANDennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.ANDally.ToList();

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.AndersonLeader;

        protected override string RoleName => Plugin.Instance.Config.LeaderName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.LeaderConfig;
    } 
}
