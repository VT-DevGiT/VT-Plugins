using Synapse.Config;
using System;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ICKamikazeScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => throw new NotImplementedException();

        protected override int RoleTeam => throw new NotImplementedException();

        protected override int RoleId => (int)MoreClasseID.CHIKamikaze;

        protected override string RoleName => PluginClass.ConfigCHIKamikaze.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIKamikaze;
    }
}
