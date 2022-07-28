using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Api.Core.Teams;
using VT_Api.Core.Roles;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Config;
using VT_Api.Extension;

namespace VTCustomClass.PlayerScript
{
    public class CHIExpertPyrotechnieScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosConscript;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosExpertPyrotechnie;

        protected override string RoleName => Plugin.Instance.Config.ChiPyrotechnieExpName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.ChiPyrotechnieExpConfig;

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        private static void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim.RoleID == (int)RoleID.ChaosExpertPyrotechnie && ev.DamageType == DamageType.Explosion)
            {
                ev.Damage = 100;
                ev.Allow = false;
            }
        }
    }
}
