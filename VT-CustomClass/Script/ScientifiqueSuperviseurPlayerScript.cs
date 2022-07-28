using Synapse;
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
    public class ScientifiqueSuperviseurScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.RSCennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.RSCally.ToList();

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.ScientifiqueSuperviseur;

        protected override string RoleName => Plugin.Instance.Config.SciSupervisorName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.SciSupervisorConfig;
    }
}
