using CustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class TechnicienScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM,   (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.Technicien;

        protected override string RoleName => PluginClass.ConfigTechnicien.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigTechnicien;

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null 
                    && (DateTime.Now - lastPower).TotalSeconds > PluginClass.ConfigTechnicien.CoolDown)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                    Player.gameObject.GetComponent<MouveVent>().duraction = PluginClass.ConfigTechnicien.PowerTime;
                    Timing.CallDelayed(PluginClass.ConfigTechnicien.PowerTime, () =>
                    {
                        if (Player.gameObject.GetComponent<MouveVent>() == null)
                        {
                            Player.gameObject.GetComponent<MouveVent>()?.Destroy();
                            lastPower = DateTime.Now;
                        }
                    });
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.gameObject.GetComponent<MouveVent>()?.Destroy();
                    lastPower = DateTime.Now;
                }
                else Reponse.Cooldown(Player, lastPower, PluginClass.ConfigTechnicien.CoolDown);
                return true;
            }
            return false;
        }
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        private DateTime lastPower = DateTime.Now;
        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
            {
                CallPower(PowerType.MoveVent);
            }
        }
    }
}
