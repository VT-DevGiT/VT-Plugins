using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class RoboticTacticalUnityScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.UTR;

        protected override string RoleName => PluginClass.ConfigRoboticTaticalUnity.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigRoboticTaticalUnity;
    }
}
