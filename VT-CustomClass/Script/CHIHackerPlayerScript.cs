using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHIHackerScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosMarauder;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosHacker;

        protected override string RoleName => Plugin.Instance.Config.HackerName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.HackerConfig;

        public override bool CallPower(byte power, out string message)
        {
            switch (power) // TODO : add tranlastion
            {
                case (int)PowerType.LightHack:

                    if ((DateTime.Now - lastPowerLight).TotalSeconds > Plugin.Instance.Config.HackerCoolDownLight)
                    {
                        light();
                        lastPowerLight = DateTime.Now;
                        message = "Light Hacked";
                        return true;
                    }
                    else
                    {
                        message = Cooldown.Send(Player, lastPowerLight, Plugin.Instance.Config.HackerCoolDownLight);
                        return false;
                    }
                case (int)PowerType.DoorHack:
                    if ((DateTime.Now - lastPowerDoor).TotalSeconds > Plugin.Instance.Config.HackerCoolDownLight)
                    {
                        Door();
                        lastPowerDoor = DateTime.Now;
                        message = "Door Hacked";
                        return true;
                    }
                    else
                    {
                        message = Cooldown.Send(Player, lastPowerDoor, Plugin.Instance.Config.HackerCoolDownMessage);
                        return false;
                    }
                case (int)PowerType.CASSIEHack:
                    if ((DateTime.Now - lastPowerMessage).TotalSeconds > Plugin.Instance.Config.HackerCoolDownMessage)
                    {
                        Message();
                        lastPowerMessage = DateTime.Now;
                        message = "Cassie Hacked";
                        return true;
                    }
                    else
                    {
                        message = Cooldown.Send(Player, lastPowerMessage, Plugin.Instance.Config.HackerCoolDownMessage);
                        return true;
                    }
                default:
                    message = string.Format(Plugin.Instance.Translation.GetForPlayer(Player).OnlyNPower, 3);
                    return false;
            }
        }

        public void Door()
        {
            IOrderedEnumerable<Synapse.Api.Door> doors = Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, Player.Position)));
            if (doors.Any())
            {
                var door = doors.First();
                door.Open = true;
                door.Locked = true;
                Timing.CallDelayed(4f, () =>
                {
                    door.Locked = false;
                });
            }
        }
        public void light() => Server.Get.Map.HeavyController.LightsOut(30, false);

        public void Message()
        {
            var Nato = $"Nato_{(char)(new System.Random().Next('a', 'z'))}";
            var scps = SynapseController.Server.GetPlayers(x => x.TeamID == (int)TeamID.SCP).Count;
            Synapse.Api.Map.Get.Cassie($"MTFUnit Epsilon 11 designated {Nato} 07 HasEntered AllRemaining AwaitingRecontainment {scps} ScpSubjects");
        }

        private DateTime lastPowerDoor = DateTime.Now.AddSeconds(-Plugin.Instance.Config.HackerCoolDownDoor);
        private DateTime lastPowerLight = DateTime.Now.AddSeconds(-Plugin.Instance.Config.HackerCoolDownLight);
        private DateTime lastPowerMessage = DateTime.Now.AddSeconds(-Plugin.Instance.Config.HackerCoolDownMessage);
    }
}
