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
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;
        
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
            Player.NoClip = true;
            Player.GodMode = true;
        }

        public override bool CallPower(byte power, out string message)
        {
            switch (power)
            {
                case 1:
                    Invisible = !Invisible;
                    message = $"You are now {(Invisible ? "invislbe" : "visble")}";
                    return true;
            }
            message = "You can only set if you are visble or not (key : alpha5)";
            return false;
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
