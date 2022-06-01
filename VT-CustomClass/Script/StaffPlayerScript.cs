using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;

namespace VTCustomClass.PlayerScript
{
    public class StaffClassScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
        
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.RIP;

        protected override int RoleId => (int)RoleID.Staff;

        protected override string RoleName => Plugin.Instance.Config.StaffName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.StaffConfig;

        public bool Invisible { get; private set; } = true;

        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            Player.Invisible = true;
            Player.NoClip = true;
            Player.GodMode = true;
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private static void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player.CustomRole is StaffClassScript staff && ev.KeyCode == UnityEngine.KeyCode.Alpha5)
                staff.Invisible = !staff.Invisible;
        }

        public override void DeSpawn()
        {
            Player.NoClip = false;
            Player.Invisible = false;
            Player.GodMode = false;
            base.DeSpawn();
        }
    }
}
