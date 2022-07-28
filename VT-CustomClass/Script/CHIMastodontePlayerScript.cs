using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHIMastodonteScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosMarauder;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosMastodonte;

        protected override string RoleName => Plugin.Instance.Config.MastondonteName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.MastondonteConfig;

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDomage;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
            VtController.Get.Events.Item.RemoveLimitAmmoEvent += OnCheckLimit;
        }

        public override void Spawning()
        {
            Player.GiveEffect(Effect.Disabled, 2);
            base.Spawning();
        }

        private static void OnCheckLimit(RemoveAmmoEventArgs ev)
        {
            if (ev.Player.CustomRole is CHIMastodonteScript)
                ev.RemovAmmo.Clear();
        }

        private static void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.CurrentItem?.ItemCategory == ItemCategory.Medical && ev.Player.CustomRole is CHIMastodonteScript role)
                ev.Allow = false;
        }

        private static void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim?.CustomRole is CHIMastodonteScript)
                ev.Damage = ev.Damage / 1.5f;
            if (ev.Killer?.CustomRole is CHIMastodonteScript role && ev.DamageType != DamageType.Explosion)
            {
                ev.Killer.ArtificialHealth += ev.Damage / 2;
                ev.Killer.Heal(ev.Damage / 4);
                ev.HollowBullet();
            }
        }
    }
}
