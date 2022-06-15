using Synapse;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VT_AndersonRobotic
{
    public class GuardianGammaOneScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessageAnderson;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();
        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.AND;

        protected override int RoleId => (int)RoleID.GardienAsimov;

        protected override string RoleName => Plugin.Instance.Config.GeneralName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.GuardianAsimov;
    } 
}
