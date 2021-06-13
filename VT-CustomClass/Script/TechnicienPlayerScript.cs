using VTCustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class TechnicienScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.RSCennemy;

        protected override List<int> FriendsList => TeamGroupe.RSCally;

        protected override RoleType RoleType => RoleType.FacilityGuard;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.Technicien;

        protected override string RoleName => Plugin.ConfigTechnicien.RoleName;

        protected override bool SetDisplayInfo => false;

        protected override AbstractConfigSection Config => Plugin.ConfigTechnicien;

        protected override void AditionalInit()
        {
            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.RemoveDisplayInfo(PlayerInfoArea.UnitName);
            Player.DisplayInfo = RoleName;
            Player.UnitName = null;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null 
                    && (DateTime.Now - lastPower).TotalSeconds > Plugin.ConfigTechnicien.CoolDown)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                    Player.gameObject.GetComponent<MouveVent>().duraction = Plugin.ConfigTechnicien.PowerTime;
                    Timing.CallDelayed(Plugin.ConfigTechnicien.PowerTime, () =>
                    {
                        if (Player.gameObject.GetComponent<MouveVent>() == null)
                        {
                            Player.gameObject.GetComponent<MouveVent>()?.Kill();
                            lastPower = DateTime.Now;
                        }
                    });
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.gameObject.GetComponent<MouveVent>()?.Kill();
                    lastPower = DateTime.Now;
                }
                else Reponse.Cooldown(Player, lastPower, Plugin.ConfigTechnicien.CoolDown);
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
                CallPower((int)PowerType.MoveVent);
            }
        }
    }
}
