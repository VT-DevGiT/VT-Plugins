using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class TechnicienScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.RSCennemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.RSCally.ToList();

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.Technicien;

        protected override string RoleName => Plugin.Instance.Config.TechnicienName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.TechnicienConfig;

        public override void SetDisplayInfo()
        {
            Player.UnitName = null;
            base.SetDisplayInfo();
        }

        private DateTime lastPower = DateTime.Now;

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null && (DateTime.Now - lastPower).TotalSeconds > Plugin.Instance.Config.Scp1048CoolDown)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                    message = "Here you are in the ventilation !";
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.gameObject.GetComponent<MouveVent>().Kill();
                    lastPower = DateTime.Now;
                    message = "you came out of ventilation !";
                }
                else
                {
                    message = Reponse.Cooldown(lastPower, Plugin.Instance.Config.Scp1048CoolDown);
                    return false;
                }
                return true;
            }
            message = "You ave only one power";
            return false;
        }
    }
}
