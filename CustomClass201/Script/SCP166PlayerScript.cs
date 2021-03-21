using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP166Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.NetralSCP, (int)TeamID.SCP };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP166;

        protected override string RoleName => PluginClass.ConfigSCP166.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP166;

        protected override void AditionalInit()
        {
            ActiveComponent<Green>();
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
        }

        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (ev.Target == Player)
                ev.Allow = false;
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == Player && !(ev.Item.ItemCategory == ItemCategory.Keycard && ev.Item.ItemCategory == ItemCategory.Medical && ev.Item.ItemCategory == ItemCategory.SCPItem))
                ev.Allow = false;
        }

        public override bool CallPower(PowerType power)
        {
            if (PowerType.BadGreen == power)
            {
                Player.GetComponent<Green>().agressif = !Player.GetComponent<Green>().agressif;
                return true;
            }
            else if (PowerType.Attaque == power)
            {
                var victim = Player.LookingAt.GetPlayer();
                if (victim != null)
                {
                    Server.Get.Logger.Info(victim.name);
                    victim.Hurt(100);
                    return true;
                }
            }
            return false;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                    CallPower(PowerType.BadGreen);
                if (ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                    CallPower(PowerType.Attaque);
            }
        }

        public override void DeSpawn()
        {
            InactiveComponent<Green>();
            Map.Get.AnnounceScpDeath("1 6 6");
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
        }
    }
}
