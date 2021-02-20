using Synapse.Config;
using System;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ICSPYScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.NtfCadet;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHISPY;

        protected override string RoleName => Plugin.ConfigCHISPY.RoleName;

        protected override AbstractConfigSection Config => throw new NotImplementedException();
    }
}
