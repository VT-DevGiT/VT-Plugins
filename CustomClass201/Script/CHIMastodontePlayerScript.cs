using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class ICMastodonteScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHIMastodonte;

        protected override string RoleName => PluginClass.ConfigCHIMastondonte.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIMastondonte;

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.DropSheld && Shled)
            {
                Shled = false;
                Player.ArtificialHealth = 0;
                return true;
            }
            return false;
        }

        private bool Shled = true;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDomage;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDomage;
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseItem;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        protected override void AditionalInit()
        {
            Player.Hub.playerStats.artificialHpDecay = 0;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                CallPower(PowerType.DropSheld);
        }

        private void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.CurrentItem.ItemCategory == ItemCategory.Medical && Shled)
                ev.Allow = false;
        }

        private void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player)
                ev.DamageAmount = ev.DamageAmount/1.5f;
            if (ev.Killer == Player)
                Player.Heal(ev.DamageAmount / 3);
            ev.PointeCreuses(Player);
        }
    }
}
