using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class TestClassScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.SCP, (int)Team.CDP, (int)Team.CHI, (int)Team.MTF, (int)Team.RIP, (int)Team.RSC, (int)Team.TUT };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP, (int)Team.CDP, (int)Team.CHI, (int)Team.MTF, (int)Team.RIP, (int)Team.RSC, (int)Team.TUT };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.ServerStaff;

        protected override int RoleId => (int)RoleID.TestClass;

        protected override string RoleName => PluginClass.ConfigTestClass.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigTestClass;

        Vector3 oldScale;

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null)
                {
                    oldScale = Player.Scale;
                    Player.Scale = new Vector3(0, 0, 0);
                    Player.gameObject.AddComponent<MouveVent>();
                }
                else if (Player.gameObject.GetComponent<MouveVent>() != null)
                {
                    Player.Scale = oldScale;
                    Player.gameObject.GetComponent<MouveVent>()?.Destroy();
                }
                return true;
            }
            return false;
        }
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
            {
                CallPower(PowerType.MoveVent);
            }
        }
    }
}
