using MEC;
using Synapse;
using Synapse.Api;
using System;
using System.Linq;
using UnityEngine;
using VT_Referance.Variable;

namespace VTCustomClass.Pouvoir
{
    public static class Hack
    {
        public static void Door(Player player)
        {
            IOrderedEnumerable<Synapse.Api.Door> doors = Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, player.Position)));
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
        public static void light() => Server.Get.Map.HeavyController.LightsOut(30, false);

        public static void Message()
        {
            var Nato = $"Nato_{(char)(new System.Random().Next('a', 'z'))}";
            var scps = SynapseController.Server.GetPlayers(x => x.TeamID == (int)TeamID.SCP).Count;
            Synapse.Api.Map.Get.Cassie($"MTFUnit Epsilon 11 designated {Nato} 07 HasEntered AllRemaining AwaitingRecontainment {scps} ScpSubjects");
        }


    }
}
