using MEC;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomClass.Pouvoir
{
    public static class Hack
    {
        public static void Door(Player player)
        {
            IOrderedEnumerable<Synapse.Api.Door> doors = Synapse.Api.Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, player.Position)));
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
        public static void light() => Generator079.mainGenerator.ServerOvercharge(30, false);

        public static void Message()
        {
            string letter = $"{new System.Random().Next('a', 'z')}";
            var scps = SynapseController.Server.GetPlayers(x => x.RealTeam == Team.SCP).Count;
            Synapse.Api.Map.Get.Cassie($"MTFUnit Epsilon 11 designated Nato_{letter} 05 HasEntered AllRemaining AwaitingRecontainment {scps} ScpSubjects");
        }


    }
}
