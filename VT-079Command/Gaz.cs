using MEC;
using Scp079Rework;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RoomInformation;

namespace VT079.Command
{
    public class Gaz : I079Command
    {
        public KeyCode Key => KeyCode.Alpha5;

        public int RequiredLevel => 3;

        public float Energy => 30;

        public float Exp => 5;

        public string Name => "gaz";

        public string Description => "Gaz the current room";

        public float Cooldown => 10f;

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.Room == null)
            {
                context.Player.Hub.scp079PlayerScript.Mana += 30;
                result.Message = "You can't gass here";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            if (context.Player.Room.RoomType != RoomType.HCZ_079)
                Timing.RunCoroutine(GasRoom(context.Player.Room, context.Player.Hub));

            return result;
        }
        private List<RoomType> grandSalle = new List<RoomType>() { RoomType.HCZ_106, RoomType.HCZ_NUKE };

        private void ChangeDoors(List<Synapse.Api.Door>  doors, bool state)
        {
            foreach (var door in doors)
            {
                door.Open = state;
                door.Locked = true;
            } 
        }
    public IEnumerator<float> GasRoom(Room room, ReferenceHub scp)
        {
            float a2cooldown = Time.timeSinceLevelLoad;
            List<Synapse.Api.Door> doors = room.Doors;
            ChangeDoors(doors,true);
            int start = 8;
            if (grandSalle.Contains(room.RoomType))
                start += 8;
            for (int Temps = start; Temps > 0; Temps--)
            {
                foreach (var player in Server.Get.Players.Where(p => p.Room == room && p.RoleID != (int)RoleType.Scp079))
                {
                    player.SendBroadcast(1, $"La pièce serra Gazée dans {Temps} seconde(s)", true);
                }
                ChangeDoors(doors,true);
                yield return Timing.WaitForSeconds(1f);
            }
            ChangeDoors(doors, false);
            Server.Get.Logger.Info($"B.{DateTime.Now}");
            for (int i = 0; i < 12 * 2; i++)
            {
                foreach (var player in Server.Get.Players.Where(p => p.Team != Team.CHI
                && p.Team != Team.SCP && p.Team != Team.RIP && p.Room == room))
                {
                    player.GiveEffect(Effect.Asphyxiated, 3, 1);
                    player.GiveEffect(Effect.Poisoned, 3, 1);
                    player.GiveEffect(Effect.SinkHole, 1, 1);
                    if (player.RoleID == (int)RoleType.Spectator)
                    {
                        scp.scp079PlayerScript.AddExperience(20);
                    }
                }
                yield return Timing.WaitForSeconds(0.5f);
            }
            foreach (var item in doors)
            {
                item.Locked = false;
            }
        }

    }
}
