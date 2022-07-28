using Synapse;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public class TestClassScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;
        protected override List<int> EnemysList => new List<int> { };

        protected override List<int> FriendsList => new List<int> { };

        protected override RoleType RoleType => RoleType.Tutorial;

        protected override int RoleTeam => (int)TeamID.RIP;

        protected override int RoleId => (int)RoleID.TestClass;

        protected override string RoleName => "Test";

        protected override SerializedPlayerRole Config => new SerializedPlayerRole();

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.MoveVent)
            {
                if (Player.gameObject.GetComponent<MouveVent>() == null)
                {
                    Player.gameObject.AddComponent<MouveVent>();
                    message = "Here you are in the ventilation !";
                }
                else
                {
                    Player.gameObject.GetComponent<MouveVent>().Kill();
                    message = "you came out of ventilation !";
                }
                return true;
            }
            message = "You ave only one power";
            return false;
        }
    }
}
