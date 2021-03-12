using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class SCP166cript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)TeamID.CDM, (int)TeamID.NTF, (int)TeamID.SEC, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP166;

        protected override string RoleName => PluginClass.ConfigSCP166.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP166;

        protected override void AditionalInit()
        {
            GetComponent<>();
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == Player && !(ev.Item.ItemCategory == ItemCategory.Keycard && ev.Item.ItemCategory == ItemCategory.Medical && ev.Item.ItemCategory == ItemCategory.SCPItem))
                ev.Allow = false;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            throw new NotImplementedException();
        }

        public override void DeSpawn()
        {
            Map.Get.AnnounceScpDeath("1 6 6");
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
        }
    }
}
