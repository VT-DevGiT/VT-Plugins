using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class StaffClassScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.ServerStaff;

        protected override int RoleId => (int)RoleID.Staff;

        protected override string RoleName => PluginClass.ConfigStaff.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigStaff;

        protected override void AditionalInit()
        {
            Player.SynapseGroup.Permissions.Add("synapse.see.invisible");
            Player.Invisible = true;
            Player.NoClip = true;
            Player.GodMode = true;
        }
        public override void DeSpawn()
        {
            Player.NoClip = false;
            Player.Invisible = false;
            Player.GodMode = false;
            base.DeSpawn();
        }
    }
}
