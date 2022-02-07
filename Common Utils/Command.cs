using Synapse;
using Synapse.Api;
using Synapse.Command;
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
        Aliases = new[] { "rot" },
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
            CommonUtiles.Instance.EventHandler.RespawnAllow = !CommonUtiles.Instance.EventHandler.RespawnAllow;
            result.State = CommandResultState.Ok;
            result.Message = $"the new valeur of ReSpawnAllow is {CommonUtiles.Instance.EventHandler.RespawnAllow}";
            return result;
        }
    }

    [CommandInformation(
        Name = "UnBanSteam",
        Aliases = new[] { "UBSteam", "UnBanS", "UBanS" },
        Description = "Unbalck list a steam ID",
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
        Description = "Stop all Respawns",
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
        Name = "ChangeServeur",
        Aliases = new[] { "SwitchServeur", "MoveServeur" },
        Description = "Move player(s) to an other serveur",
        Permission = "vanilla.PlayersManagement",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Entre the serveur port and after the player or the players to move to an other serveur, if the player ave a space in the name (use ID)",
        Arguments = new[] { "ServeurPort", "Player", "OtherPlayer", "OtherPlayer..." }
        )]

    class ChangeServer : ISynapseCommand
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

            var players = new Player[context.Arguments.Count - 1];

            for (int i = 1; i < context.Arguments.Count; i++)
            {
                var player = Server.Get.GetPlayer(context.Arguments.Array[i]);
                players[i - 1] = player;
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
}
