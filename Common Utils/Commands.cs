﻿using Synapse;
using Synapse.Api;
using Synapse.Command;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Common_Utiles
{
    [CommandInformation(
        Name = "Location",
        Aliases = new[] { "loc" },
        Description = "Get a MapPoint for config",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "go to the desired position and execute the command"
        )]
    class Cmdloc : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            MapPoint Mp = context.Player.MapPoint;
            string MapPointString = $"SerializedMapPoint(\"{Mp.Room.RoomName}\", {Mp.RelativePosition.x.ToString().Replace(",", ".")}f, {Mp.RelativePosition.y.ToString().Replace(",", ".")}f, {Mp.RelativePosition.z.ToString().Replace(",", ".")}f)";
            Server.Get.Logger.Info(MapPointString);
            result.State = CommandResultState.Ok;
            result.Message = Mp.ToString();
            return result;
        }
    }

    [CommandInformation(
        Name = "Rotation",
        Aliases = new[] { "rota" },
        Description = "Get a Vector2 for config",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Look to the desired point and execute the command"
        )]
    class CmdRot : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Vector2 Rt = context.Player.Rotation;
            string Rotation = $"Vector2({Rt.y.ToString().Replace(",", ".")}f, {Rt.x.ToString().Replace(",", ".")}f)";
            Server.Get.Logger.Info(Rotation);
            result.State = CommandResultState.Ok;
            result.Message = Rt.ToString();
            return result;
        }
    }

    [CommandInformation(
        Name = "StopRespawn",
        Aliases = new[] { "StopRespawn" },
        Description = "Stop all Respawns or allow Respawn",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Execute the command"
        )]
    class CmdStopRespawn : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Plugin.Instance.RespawnAllow = !Plugin.Instance.RespawnAllow;
            result.State = CommandResultState.Ok;
            result.Message = $"the new value of ReSpawnAllow is {Plugin.Instance.RespawnAllow}";
            return result;
        }
    }

    [CommandInformation(
        Name = "UnBanSteam",
        Aliases = new[] { "UBSteam", "UnBanS", "UBanS" },
        Description = "Unblack list a steam ID",
        Permission = "vanilla.LongTermBanning",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Enter the steamID without the @",
        Arguments = new[] { "SteamID" }
        )]
    class UnBanSteam : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Arguments.Count != 1)
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid parameter";
                return result;
            }
            else if (!long.TryParse(context.Arguments.At(0), out _))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid SteamID64";
                return result;
            }
            BanHandler.RemoveBan(context.Arguments.At(0), BanHandler.BanType.UserId);

            return result;
        }
    }

    [CommandInformation(
        Name = "BanSteam",
        Aliases = new[] { "BSteam", "BanS"},
        Description = "Black list a SteamID",
        Permission = "vanilla.LongTermBanning",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Enter the steamID without the @",
        Arguments = new[] { "SteamID64", "Duration", "Reason" }
        )]
    class BanSteam : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            string reason = "";
            int duration;
            if (context.Arguments.Count < 3)
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid parameter";
                return result;
            }
            else if (!long.TryParse(context.Arguments.At(0), out _))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid SteamID64";
                return result;
            }
            else if (!int.TryParse(context.Arguments.At(1), out duration))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid Duration";
                return result;
            }
            for (int i = 2; i < context.Arguments.Count; i++ )
                reason = $"{reason} {context.Arguments.At(i)}";
            if (context.Platform == Platform.RemoteAdmin)
                Server.Get.OfflineBanID(reason, context.Player.UserId, $"{context.Arguments.At(0)}@Steam", duration);
            else
                Server.Get.OfflineBanID(reason, "ServeurConsol", $"{context.Arguments.At(0)}@Steam", duration);
            return result;
        }
    }

    [CommandInformation(
        Name = "UnBanDiscord",
        Aliases = new[] { "UBDiscord", "UnBanD", "UBanD" },
        Description = "Unbalck list a discordID",
        Permission = "vanilla.LongTermBanning",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Enter the discordID without the @",
        Arguments = new [] {"DiscordID"}
        )]
    class UnBanDiscord : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Arguments.Count != 1)
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid parameter";
                return result;
            }
            else if (!long.TryParse(context.Arguments.At(0), out _))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid DiscordID";
                return result;
            }
            BanHandler.RemoveBan(context.Arguments.At(0), BanHandler.BanType.UserId);

            return result;
        }
    }

    [CommandInformation(
        Name = "BanDiscord",
        Aliases = new[] { "BDiscord", "BanD" },
        Description = "black list a discordID",
        Permission = "vanilla.LongTermBanning",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Enter the DiscordID without the @",
        Arguments = new[] { "DiscordID", "Duration", "Reason" }
        )]
    class BanDiscord : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            string reason = "";
            int duration;
            if (context.Arguments.Count < 3)
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid parameter";
                return result;
            }
            else if (!long.TryParse(context.Arguments.At(0), out _))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid DiscordID";
                return result;
            }
            else if (!int.TryParse(context.Arguments.At(1), out duration))
            {
                result.State = CommandResultState.Error;
                result.Message = "invalid Duration";
                return result;
            }
            for (int i = 2; i < context.Arguments.Count; i++)
                reason = $"{reason} {context.Arguments.At(i)}";
            if (context.Platform == Platform.RemoteAdmin)
                Server.Get.OfflineBanID(reason, context.Player.UserId, $"{context.Arguments.At(0)}@Discord", duration);
            else
                Server.Get.OfflineBanID(reason, "ServeurConsol", $"{context.Arguments.At(0)}@Discord", duration);

            return result;
        }
    }

    [CommandInformation(
        Name = "MoveServeur",
        Aliases = new[] { "SwitchServeur", "MoveToServeur" },
        Description = "Move player(s) to an other serveur",
        Permission = "vanilla.PlayersManagement",
        Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
        Usage = "Entre the serveur port and after the player or the players to move to an other serveur, if the player ave a space in the name (use ID)",
        Arguments = new[] { "ServerPort", "Players" }
        )]
    class MoverServer : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Arguments.Count == 0)
            {
                result.Message = "Entre the serveur port and after the player or the players to move to an other serveur, if the player ave a space in the name (use ID)";
                result.State = CommandResultState.Error;
                return result;
            }

            if (!ushort.TryParse(context.Arguments.FirstElement(), out var server))
            {
                result.Message = "Invalid serveur port";
                result.State = CommandResultState.Error;
                return result;
            }

            var me = context.Platform != Platform.ServerConsole ? context.Player : null;
            if (!Server.Get.TryGetPlayers(context.Arguments.At(1), out var players, me))
            {
                result.Message = "No player found";
                result.State = CommandResultState.Error;
            }

            result.Message = string.Concat("Players move to serveur port {0} :", server);
            result.State = CommandResultState.Ok;

            foreach (var player in players)
            {
                if (player != null)
                {
                    result.Message = "\n\t" + player.name;
                    player.SendToServer(server);
                }
            }

            return result;
        }
    }

    [CommandInformation(
    Name = "RolesID",
    Aliases = new[] { "Roles" },
    Description = "Get the roles ID of all roles",
    Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole }
    )]
    public class RolesInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            result.Message = "All registred roles :\n";
            foreach (var role in ((RoleType[])Enum.GetValues(typeof(RoleType))).OrderBy(r => (int)r))
            {
                string name = Regex.Replace(role.ToString(), "<.*?>", String.Empty);
                result.Message += String.Format(" {0,-60} {1,-5} : Vanila\n", name, (int)role);
            }
            foreach (var role in Server.Get.RoleManager.CustomRoles.OrderBy(r => r.ID))
            {
                string name = Regex.Replace(role.Name, "<.*?>", String.Empty);
                string plugiName = role.RoleScript.Assembly.GetName().Name;
                result.Message += String.Format(" {0,-60} {1,-5} : {2}\n", name, role.ID, plugiName);
            }
            return result;
        }
    }

    /* C'est pas finie c'est pour ça
    [CommandInformation(
       Name = "RoleInfo",
       Aliases = new[] { "Rinfo", "infoRole", "infoR" },
       Description = "Get info on the curent role",
       Usage = "You need to be a custom role, or entre the ID of the role",
       Permission = "",
       Platforms = new[] { Platform.ServerConsole },
       Arguments = new[] { "(roleID)" }
       )]
    internal class RoleInfo : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int id;
            if (context.Arguments.Count == 0)
                id = context.Player.RoleID;
            else if (!int.TryParse(context.Arguments.FirstElement(), out id))
            {
                result.Message = "it must be a number";
                result.State = CommandResultState.Error;
            }
            else if (CommonUtiles.Instance.Config.RolesInfos.ContainsKey(id))
            {
                result.Message = CommonUtiles.Instance.Config.RolesInfos[id];
                result.State = CommandResultState.Ok;
            }
            else
            {
                result.Message = "No info";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }*/
}
