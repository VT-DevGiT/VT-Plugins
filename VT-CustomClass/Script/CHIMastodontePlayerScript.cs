using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class CHIMastodonteScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiMastodonte;

        protected override string RoleName => PluginClass.ConfigCHIMastodonte.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIMastodonte;

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.DropSheld && Shield.ShieldLock)
            {
                Shield.ShieldLock = false;
                Shield.MaxShield = 100;
                Shield.Shield = 0;
                Player.StaminaUsage /= 2;
                Server.Get.Events.Player.PlayerItemUseEvent -= OnUseItem;
                return true;
            }
            return false;
        }


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
            Shield.ShieldLock = false;
        }

        protected override void AditionalInit()
        {
            Shield.ShieldLock = true;
            Player.StaminaUsage *= 2;
            Player.GiveEffect(Effect.Disabled);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                CallPower((int)PowerType.DropSheld);
        }

        private void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player && ev.CurrentItem.ItemCategory == ItemCategory.Medical)
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
