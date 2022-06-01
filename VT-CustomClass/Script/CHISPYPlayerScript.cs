using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHISPYScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIenemy.ToList();

        protected override RoleType RoleType => RoleType.NtfPrivate;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosSpy;

        protected override string RoleName => Plugin.Instance.Config.SpyName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.SpyConfig;

        public override void SetDisplayInfo() { }

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.SwitchRole)
            {
                if (Player.RoleType != RoleType.NtfPrivate)
                {
                    message = "You have already removed your disguise";
                    return false;
                }

                Server.Get.Map.SpawnGrenade(Player.Position, Vector3.zero, 0.1f, GrenadeType.Flashbang);
                Player.ChangeRoleAtPosition(RoleType.ChaosConscript);
                Player.MaxHealth = Config.Health ?? 120;
                Player.GiveEffect(Effect.Blinded, 0, 0);

                message = "You have removed your disguise !";
                return true;
            }
            message = "You ave only one power";
            return false;
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim.CustomRole is CHISPYScript)
                ev.Victim.ChangeRoleAtPosition(RoleType.ChaosConscript);
            else if (ev.Killer?.CustomRole is CHISPYScript role)
            {
                string message = Plugin.Instance.Translation.ActiveTranslation.KilledMessage.Replace("%RoleName%", role.RoleName);
                ev.Victim.OpenReportWindow(message);
            }
        }
    }
}
