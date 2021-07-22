using Synapse;
using Synapse.Api;
using Synapse.Command;
using UnityEngine;

namespace Common_Utiles
{
    [CommandInformation(
              Name = "Location",
              Aliases = new[] { "loc" },
              Description = "MapPoint of the player for config",
              Permission = "",
              Platforms = new[] { Platform.RemoteAdmin },
              Usage = "Loc"
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
          Description = "Rotation of the player for config",
          Permission = "",
          Platforms = new[] { Platform.RemoteAdmin },
          Usage = "Rot"
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
      Description = "Stop all Respawns",
      Permission = "",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = "StopRespawn"
      )]
    class CmdStopRespawn : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            CommonUtiles.Instance.RespawnAllow = !CommonUtiles.Instance.RespawnAllow;
            result.State = CommandResultState.Ok;
            result.Message = $"the new valeur of ReSpawnAllow is {CommonUtiles.Instance.RespawnAllow}";
            return result;
        }
    }

    [CommandInformation(
      Name = "UnBanSteam",
      Aliases = new[] { "UBSteam", "UnBanS", "UBanS" },
      Description = "Stop all Respawns",
      Permission = "vanilla.LongTermBanning",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = "UBanS [SteamID64]"
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
      Description = "Stop all Respawns",
      Permission = "vanilla.LongTermBanning",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = "BanS [SteamID64] [Duration] [Reason]"
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
      Description = "Stop all Respawns",
      Permission = "vanilla.LongTermBanning",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = "UBanD [DiscordID]"
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
      Usage = "BanD [DiscordID] [Duration] [Reason]"
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
}
