using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class CHIMastodonteScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.SHA };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiMastodonte;

        protected override string RoleName => PluginClass.ConfigCHIMastodonte.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIMastodonte;
        
        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.DropSheld && Shed)
            {
                Player.StaminaUsage /= 2;
                Shed = false;
                Player.ArtificialHealth = 0;
                Player.MaxArtificialHealth = 100;
                Server.Get.Events.Player.PlayerItemUseEvent -= OnUseItem;
                return true;
            }
            return false;
        }

        private bool Shed = true;

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
            Player.StaminaUsage *= 2;
            Player.Hub.playerStats.artificialHpDecay = 0;
            Player.GiveEffect(Effect.Disabled);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                CallPower(PowerType.DropSheld);
        }

        private void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player && ev.CurrentItem.ItemCategory == ItemCategory.Medical && ev.CurrentItem.ItemType != ItemType.Adrenaline && Shed)
                ev.Allow = false;
        }

        private void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player)
                ev.DamageAmount = ev.DamageAmount/1.5f;
            if (ev.Killer == Player)
            { 
                Player.Heal(ev.DamageAmount / 3);
                ev.HollowBullet(Player);
            }
        }
    }
}
