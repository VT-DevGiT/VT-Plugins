using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public class SCP1048cript : AbstractRole, IScpDeathAnnonce
    {
        public string ScpName => "1 0 4 8";
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.SCPenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.SCPally.ToList();
        
        protected override List<int> FfFriendsList => FriendsList;

        protected override RoleType RoleType => RoleType.Scp049;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP1048;

        protected override string RoleName => Plugin.Instance.Config.Scp1048Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp1048Config;

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
                    message = Cooldown.Send(lastPower, Plugin.Instance.Config.Scp1048CoolDown);
                    return false;
                }
                return true;
            }
            message = "You ave only one power";
            return false;
        }

        private DateTime lastPower = DateTime.Now;
    }
}
