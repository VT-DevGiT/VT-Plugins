using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;

namespace VTCustomClass.PlayerScript
{
    public class NTFExpertPyrotechnieScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();
        
        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfExpertPyrotechnie;

        protected override string RoleName => Plugin.Instance.Config.NtfPyrotechnieExpName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.NtfPyrotechnieExpConfig;

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        private static void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim?.CustomRole is NTFExpertPyrotechnieScript && ev.DamageType == DamageType.Explosion)
                ev.Allow = false;
        }
    }
}