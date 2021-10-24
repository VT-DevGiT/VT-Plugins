using MEC;
using Scp079Rework;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT079.Command
{
    public class Gaz : I079Command
    {
        public KeyCode Key => KeyCode.Alpha7;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 3);

        public float Energy => PluginExtensions.GetEnergy(Name, 30);

        public float Exp => PluginExtensions.GetExp(Name, 5);

        public string Name => "gaz";

        public string Description => "Gaz the current room";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 10f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (Map.Get.Nuke.Active)
            {
                context.Player.Hub.scp079PlayerScript.Mana += 25;
                result.Message = "You can't gass when the WarHead is active";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            if (context.Player.Room == null)
            {
                context.Player.Hub.scp079PlayerScript.Mana += 25;
                result.Message = "You can't gass here";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            if (context.Player.Room.RoomType != MapGeneration.RoomName.Hcz079 && context.Player.Room.RoomType != MapGeneration.RoomName.Outside)
                Timing.RunCoroutine(GasRoom(context.Player.Room, context.Player));
            else
                result.Message = "Invalide Zone";
            return result;
        }
        private List<MapGeneration.RoomName> grandSalle = new List<MapGeneration.RoomName>() { MapGeneration.RoomName.Hcz106, MapGeneration.RoomName.HczWarhead, MapGeneration.RoomName.Hcz049, MapGeneration.RoomName.EzIntercom };

        private void ChangeDoors(List<Synapse.Api.Door>  doors, bool state)
        {
            foreach (var door in doors)
            {
                door.Open = state;
                door.Locked = true;
            } 
        }
    public IEnumerator<float> GasRoom(Room room, Player scp)
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
                    player.SendBroadcast(1, $"The room will be Gassed in {Temps} second(s)", true);
                }
                ChangeDoors(doors,true);
                yield return Timing.WaitForSeconds(1f);
            }
            ChangeDoors(doors, false);
            for (int i = 0; i < 12 * 2; i++)
            {
                foreach (var player in Server.Get.Players.Where(p => p.Team != Team.SCP && p.Team != Team.RIP 
                && p.RoleID != (int)RoleID.NtfVirologue && !p.IsUTR() && p.Room == room))
                {
                    
                    player.Hurt(10, DamageTypes.Decont, player);
                    player.GiveEffect(Effect.Poisoned, 3, 1);
                    if (player.Health < 35)
                        player.GiveEffect(Effect.SinkHole, 3, 1);
                    if (player.RoleID == (int)RoleType.Spectator)
                    {
                        scp.Hub.scp079PlayerScript.AddExperience(20);
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
