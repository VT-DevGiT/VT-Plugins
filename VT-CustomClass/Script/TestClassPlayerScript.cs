using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class TestClassScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.RIP;

        protected override int RoleId => (int)RoleID.TestClass;

        protected override string RoleName => Plugin.ConfigTestClass.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigTestClass;

        Vector3 oldScale;

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.MoveVent)
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
                    Player.gameObject.GetComponent<MouveVent>()?.Kill();
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
                CallPower((int)PowerType.MoveVent);
            }
        }
    }
}
