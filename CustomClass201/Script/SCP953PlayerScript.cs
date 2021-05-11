using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP953Script : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
     
        protected override List<int> EnemysList => TeamGroupe.BerserkSCPennemy;

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.BerserkSCP;

        protected override int RoleId => (int)RoleID.SCP953;

        protected override string RoleName => PluginClass.ConfigSCP953.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP953;
    }
}
