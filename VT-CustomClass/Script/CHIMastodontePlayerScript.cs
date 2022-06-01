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
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHIMastodonteScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosMarauder;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosMastodonte;

        protected override string RoleName => Plugin.Instance.Config.MastondonteName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.MastondonteConfig;

        public bool shieldActif = true;

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.DropSheld)
            {
                if (!shieldActif)
                {
                    message = "you have already removed your shield";
                    return false;
                }
                shieldActif = false;
                message = "you have removed your shield !";
                return true;
            }
            message = "you ave only one power";
            return false;
        }


        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDomage;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
        }

        public override void Spawning()
        {
            Player.GiveEffect(Effect.Disabled, 2);
            base.Spawning();
        }

        private static void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.CurrentItem.ItemCategory == ItemCategory.Medical && ev.Player.CustomRole is CHIMastodonteScript role && role.shieldActif)
                ev.Allow = false;
        }

        private static void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim.CustomRole is CHIMastodonteScript)
                ev.Damage = ev.Damage / 1.5f;
            if (ev.Killer?.CustomRole is CHIMastodonteScript role && ev.DamageType != DamageType.Explosion && !role.shieldActif)
            {
                ev.Killer.ArtificialHealth += ev.Damage / 4;
                ev.Killer.Heal(ev.Damage / 8);
                ev.HollowBullet();
            }
        }
    }
}
