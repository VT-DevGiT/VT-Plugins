using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public class Scripte201 : AbstractRole
    {
        protected override List<int> EnemysList => new List<int>();
        protected override List<int> FfFriendsList => TeamManager.Group.SCPally.ToList();
        protected override List<int> FriendsList => TeamManager.Group.VIPally.ToList();
        protected override RoleType RoleType => RoleType.Tutorial;
        protected override int RoleTeam => (int)TeamID.SCP;
        protected override int RoleId => 201;
        protected override string RoleName => "201";
        protected override SerializedPlayerRole Config => new SerializedPlayerRole();
        protected override string SpawnMessage => "Good Luck";
    }
}
