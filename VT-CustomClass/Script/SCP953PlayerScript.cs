using Synapse.Config;
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
    public class SCP953Script : AbstractRole, IScpDeathAnnonce
    {
        public string ScpName => "9 5 3";
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;
     
        protected override List<int> EnemysList => TeamManager.Group.BerserkSCPennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.BerserkSCPally.ToList();

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.BerserkSCP;

        protected override int RoleId => (int)RoleID.SCP953;

        protected override string RoleName => Plugin.Instance.Config.Scp953Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp953Config;
    }
}
