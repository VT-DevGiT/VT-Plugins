using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VTCustomClass.PlayerScript
{
    public class CHIIntrusScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosRifleman;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosIntrus;

        protected override string RoleName => Plugin.Instance.Config.IntruderName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.IntruderConfig;

        public override void SetDisplayInfo() { }
    }
}
