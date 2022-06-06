using CustomPlayerEffects;
using Synapse;
using Synapse.Api;
using Synapse.Api.CustomObjects;
using Synapse.Api.Enum;
using Synapse.Api.Roles;
using Synapse.Command;
using Synapse.Config;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Extension;

namespace VTDevHelp
{
    [CommandInformation(
      Name = "DevDoorInfo",
      Aliases = new[] { "VTFdoor" },
      Description = "For find door",
      Permission = "dev",
      Platforms = new[] { Platform.RemoteAdmin }
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
    Permission = "dev",
    Platforms = new[] { Platform.RemoteAdmin }
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
    Permission = "dev",
    Platforms = new[] { Platform.RemoteAdmin }
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
  Permission = "dev",
  Platforms = new[] { Platform.RemoteAdmin }
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
     Permission = "dev",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole }
     )]
    public class TestCommand : ISynapseCommand
    {

        public class testeffect : PlayerEffect, IDisplayablePlayerEffect
        {
            public bool GetSpectatorText(out string s)
            {
                s = "SUS";
                return true;
            }
        }

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            var effectCtrl = context.Player.PlayerEffectsController;
            var effect = new testeffect();

            if (!effectCtrl.AllEffects.ContainsKey(typeof(testeffect)))
                effectCtrl.AllEffects.Add(typeof(testeffect), effect);
           
            effectCtrl.EnableEffect(effect, -1);

            //var truc = "<size=25>You were killed by</size>\\n%PlayerName%\\n<size=25>as</size>\\n%RoleName%".Replace("\\n", "\n").Replace("%PlayerName%", "Ta Mère").Replace("%RoleName%", "UTR");
            //context.Player.Kill(truc);

            /*
            var path = @"C:\Users\valentin\AppData\Roaming\Synapse\dependencies\hitman-le-cobra-philippe-je-sais-ou-tu-te-caches.raw";
            AudioApi.AudioApi.Play(path);*/
            return result;
        }
    }

    [CommandInformation(
    Name = "DevScreen",
    Aliases = new[] { "VTScreen" },
    Description = "Let's go !",
    Permission = "dev",
    Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole }
    )]
    public class ScreenCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Arguments.Count == 0)
                return result;
            var player =Server.Get.GetPlayer(context.Arguments.FirstElement());
            player.DimScreen();
            MEC.Timing.CallDelayed(1f, () => player.OpenMenu(MenuType.OldFastMenu));
            return result;
        }
    }

    [CommandInformation(
     Name = "DevRoles",
     Aliases = new[] { "VTRoles" },
     Description = "For get all roles info",
     Permission = "dev",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole }
     )]
    public class RolesInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            result.Message = "All registred roles :\n";
            foreach (var role in (RoleType[])Enum.GetValues(typeof(RoleType)))
            {
                string name = Regex.Replace(role.ToString(), "<.*?>", String.Empty);
                result.Message += String.Format("\t{0,-60} {1,-5} : valide Vanila\n", name, (int)role);
            }
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
     Permission = "dev",
     Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole }
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
     Permission = "dev",
     Platforms = new[] { Platform.RemoteAdmin }
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
    Name = "DevPermitino",
    Aliases = new[] { "VTPerm" },
    Description = "Dev Perm for Test",
    Permission = "dev",
    Platforms = new[] { Platform.RemoteAdmin }
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
       Permission = "dev",
       Platforms = new[] { Platform.RemoteAdmin }
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
    Permission = "dev",
    Platforms = new[] { Platform.RemoteAdmin }
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
       Permission = "dev",
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
            switch (context.Arguments.FirstElement().ToLower())
            {
                case "items":
                case "item":
                    foreach (var iteam in iteams)
                        iteam.Destroy();
                    result.State = CommandResultState.Ok;
                    break;

                case "ragdolls":
                case "ragdoll":
                case "corps":
                case "corp":
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;

                case "all":
                    foreach (var iteam in iteams)
                        iteam.Despawn();
                    foreach (var ragdoll in ragdolls)
                        ragdoll.Destroy();
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
            result.Message = "Méliodas é bô !";// oka l'était plus
            result.State = CommandResultState.Ok;
            return result;
        }
    }
}

