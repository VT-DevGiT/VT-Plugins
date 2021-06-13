using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class SCP682Script : BasePlayerScript, IScpRole
    {
        public string ScpName => "6 8 2";
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.BerserkSCPennemy;

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Scp93953;

        protected override int RoleTeam => (int)TeamID.BerserkSCP;

        protected override int RoleId => (int)RoleID.SCP682;

        protected override string RoleName => Plugin.ConfigSCP682.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP682;
    }
}
