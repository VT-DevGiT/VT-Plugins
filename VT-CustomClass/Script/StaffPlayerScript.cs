using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class StaffClassScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.RIP;

        protected override int RoleId => (int)RoleID.Staff;

        protected override string RoleName => PluginClass.ConfigStaff.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigStaff;

        protected override void AditionalInit()
        {
            Player.SynapseGroup.Permissions.Add("synapse.see.invisible");
            Player.Invisible = true;
            Player.NoClip = true;
            Player.GodMode = true;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                Player.Invisible = !Player.Invisible;
        }

        public override void DeSpawn()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Player.NoClip = false;
            Player.Invisible = false;
            Player.GodMode = false;
            base.DeSpawn();
        }
    }
}
