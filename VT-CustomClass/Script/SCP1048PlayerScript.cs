using VTCustomClass.Pouvoir;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class SCP1048cript : BasePlayerScript, IScpRole
    {
        public string ScpName => "1 0 4 8";
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.SCPenemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.Scp049;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP1048;

        protected override string RoleName => Plugin.ConfigSCP1048.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP1048;

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null 
                    && (DateTime.Now - lastPower).TotalSeconds > Plugin.ConfigSCP1048.CoolDown)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.gameObject.GetComponent<MouveVent>()?.Kill();
                    lastPower = DateTime.Now;
                }
                else Reponse.Cooldown(Player, lastPower, Plugin.ConfigSCP1048.CoolDown);
                return true;
            }
            return false;
        }

        private DateTime lastPower = DateTime.Now;
    }
}
