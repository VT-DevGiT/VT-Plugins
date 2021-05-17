using VTCustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class SCP1048cript : BasePlayerScript, IScpRole
    {
        public string ScpName => "1 0 4 8";
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.SCPenemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.Scp049;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP1048;

        protected override string RoleName => PluginClass.ConfigSCP1048.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP1048;

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null 
                    && (DateTime.Now - lastPower).TotalSeconds > PluginClass.ConfigSCP1048.CoolDown)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.gameObject.GetComponent<MouveVent>()?.Kill();
                    lastPower = DateTime.Now;
                }
                else Reponse.Cooldown(Player, lastPower, PluginClass.ConfigSCP1048.CoolDown);
                return true;
            }
            return false;
        }

        private DateTime lastPower = DateTime.Now;
    }
}
