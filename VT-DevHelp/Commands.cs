using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using Synapse.Command;
using Synapse.Config;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VT_Api.Extension;
using System.Reflection;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using Synapse.Api.CustomObjects;
using Synapse.Api.Enum;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using MEC;

namespace VTDevHelp
{
    [CommandInformation(
      Name = "DevDoorInfo",
      Aliases = new[] { "VTFdoor" },
      Description = "For find door",
      Permission = "",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = ""
      )]
    public class DoorInfoCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            IOrderedEnumerable<Synapse.Api.Door> door = Synapse.Api.Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, context.Player.Position)));
            result.Message = $"No Door find";
            result.State = CommandResultState.Ok;
            if (door.Any())
            {
                result.Message = 
                    $"Door : " +
                    $"\n Name -> {door.First().Name} " +
                    $"\n DoorType -> {door.First().DoorType} " +
                    $"\n Position -> {door.First().Position} " +
                    $"\n Rotation -> {door.First().Rotation} " +
                    $"\n Is Open -> {door.First().Open} " +
                    $"\n Is Breakable -> {door.First().IsBreakable} " +
                    $"\n Door Permissions -> {door.First().DoorPermissions}";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
    Name = "DevDoorPos",
    Aliases = new[] { "VTPdoor" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DoorPosCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var PlayerPos = context.Player.Position;
            
            Plugin.DoorPosition = new SerializedMapPoint(context.Player.Room.RoomName, PlayerPos.x, PlayerPos.y, PlayerPos.z);
            Server.Get.Logger.Info($"{context.Player.Room.RoomName}, {PlayerPos.x}, {PlayerPos.y}, {PlayerPos.z}");
            
            Plugin.DoorRotation = context.Player.gameObject.transform.rotation;
            Server.Get.Logger.Info(context.Player.Rotation);
            
            return result;
        }
    }

    [CommandInformation(
    Name = "DevPos",
    Aliases = new[] { "VTPos" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class PosCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var PlayerPos = context.Player.Position;

            Server.Get.Logger.Info($"Vector3  : {PlayerPos.x}, {PlayerPos.y}, {PlayerPos.z}");
            Server.Get.Logger.Info($"Rotation : {context.Player.Rotation}");

            return result;
        }
    }

    [CommandInformation(
  Name = "DevDoorSpawn",
  Aliases = new[] { "VTSdoor" },
  Description = "",
  Permission = "",
  Platforms = new[] { Platform.RemoteAdmin },
  Usage = ""
  )]
    public class DoorSpawnCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            var door = new SynapseDoorObject(SpawnableDoorType.LCZ, Plugin.DoorPosition.Parse().Position, Quaternion.Euler(Vector3.zero), Vector3.one);
            
            return result;
        }
    }

    [CommandInformation(
     Name = "DevTest",
     Aliases = new[] { "VTTest" },
     Description = "For TEST",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
     Usage = ""
     )]
    public class TestCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            context.Player.DisplayInfo = "Test\b\b\b\b\b\b\b\b\b\bTest";

            context.Player.FakeRole(RoleType.Scp173);

            return result;
        }
    }

    [CommandInformation(
     Name = "DevRoles",
     Aliases = new[] { "VTRoles" },
     Description = "For get all roles info",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
     Usage = ""
     )]
    public class RolesInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            result.Message = "All registred roles :\n";
            foreach (var role in Server.Get.RoleManager.CustomRoles.OrderBy(r => r.ID))
            {
                string name = Regex.Replace(role.Name, "<.*?>", String.Empty);
                bool valid = typeof(IRole).IsAssignableFrom(role.RoleScript);
                result.Message += String.Format("\t{0,-60} {1,-5} : valide {2}\n", name, role.ID, valid.ToString());
            }
            return result;
        }
    }

    [CommandInformation(
     Name = "DevRole",
     Aliases = new[] { "VTRole" },
     Description = "For get this role info",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
     Usage = ""
     )]
    public class RoleInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            Player player;

            if (context.Arguments.Count > 0)
                player = Server.Get.GetPlayer(context.Arguments.FirstElement()) ?? context.Player;
            else
                player = context.Player;

            if (player.CustomRole != null)
            {
                result.Message = $@"Role of {player.name} : 
CustomRole !
Name     -> {player.CustomRole.GetRoleName()}
ID       -> {player.RoleID}
RoleType -> {player.RoleType}
Team     -> {(TeamID)player.CustomRole.GetTeamID()}
Abstract -> {player.CustomRole is AbstractRole}
Enemies  -> {string.Join(",", player.CustomRole.GetEnemiesID())}
Friends  -> {string.Join(",", player.CustomRole.GetFriendsID())}
";
            }
            else
            {
                result.Message = $@"Role of  {player.name} : 
VanilaRole !
Name     -> {player.RoleType}
ID       -> {player.RoleID}
Team     -> {player.Team}
MaxHp    -> {player.ClassManager.CurRole.maxHP}
Abilitie -> {player.ClassManager.CurRole.abilities.Any()}
";
            }
            result.State = CommandResultState.Ok;

            return result;
        }
    }

    [CommandInformation(
     Name = "DevitemInfo",
     Aliases = new[] { "VTIteam" },
     Description = "Dev iteam info",
     Permission = "",
     Platforms = new[] { Platform.RemoteAdmin },
     Usage = ""
     )]
    public class ItemInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteamHand = context.Player?.ItemInHand;
            if (iteamHand == null)
            {
                result.State = CommandResultState.Error;
                return result;
            }

            Server.Get.Logger.Info($"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Attachments->{iteamHand.WeaponAttachments}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}");
            result.Message = $"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Attachments->{iteamHand.WeaponAttachments}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}";
            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevDecont",
    Aliases = new[] { "VTDecont" },
    Description = "Dev Decont Test",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
    Usage = ""
    )]
    public class TestDecontCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.Decontamination.InstantStart();
            Server.Get.Map.Decontamination?.Controller?.FinishDecontamination();
            result.State = CommandResultState.Error;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevPermitino",
    Aliases = new[] { "VTPerm" },
    Description = "Dev Perm for Test",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DevGive : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Arguments.FirstElement().Any())
            { 
                int Id;
                int.TryParse(context.Arguments.FirstElement(), out Id);
                var player = Server.Get.GetPlayers(p => p.PlayerId == Id).FirstOrDefault();
                player.RemoteAdminAccess = !player.RemoteAdminAccess; 
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "DevSong",
       Aliases = new[] { "VTSong" },
       Description = "Dev Song Test",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ""
       )]
    public class SongCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.State = CommandResultState.Error;
            if (int.TryParse(context.Arguments.FirstElement(),out int ID))
            {
                Map.Get.PlayAmbientSound(ID);
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
    Name = "DevGrenad",
    Aliases = new[] { "VTGrenad" },
    Description = "Dev Test Plugin",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class GrenadCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int i = 10;
            context.Player.Cuffer = context.Player;
            if (context.Arguments.First() != null)
                int.TryParse(context.Arguments.First(), out i);
            while (i != 0)
            {
                i--;
                context.Player.Inventory.AddItem(ItemType.GrenadeHE);
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "DevClear",
       Aliases = new[] { "VTDClear" },
       Description = "",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ".VTClear (Iteam, Corp ou All)"
       )]
    public class ClearCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteams = Server.Get.Map.Items.Where(p => p.State == Synapse.Api.Enum.ItemState.Map);
            var ragdolls = Server.Get.Map.Ragdolls;
            switch (context.Arguments.FirstElement())
            {
                case "Iteam":
                    foreach (var iteam in iteams)
                        iteam.Destroy();
                    result.State = CommandResultState.Ok;
                    break;

                case "Corp":
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;

                case "All":
                    foreach (var iteam in iteams)
                        iteam.Despawn();
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;
            }
            return result;
        }
    }

    [CommandInformation(
     Name = "Méliodas",
     Aliases = new[] { "Meliodas" },
     Description = "Méliodas!!!!!!!!!",
     Permission = "",
     Platforms = new[] { Platform.ClientConsole, Platform.RemoteAdmin }, 
     Usage = ""
     )]
    public class MéliodasCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "Méliodas é bô !";
            result.State = CommandResultState.Ok;
            return result;
        }
    }
}

