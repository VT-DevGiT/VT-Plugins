using Synapse;
using Synapse.Api;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common_Utiles
{
    [CommandInformation(
              Name = "Location",
              Aliases = new[] { "loc" },
              Description = "MapPoint of the player for config",
              Permission = "",
              Platforms = new[] { Platform.RemoteAdmin },
              Usage = ".Loc"
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
          Usage = ".Rot"
          )]
    class CmdRot : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Vector2 Rt = context.Player.Rotation;
            string Rotation = $"Vector2(\"{Rt.Room.RoomName}\", {Rt.RelativePosition.x.ToString().Replace(",", ".")}f, {Rt.RelativePosition.y.ToString().Replace(",", ".")}f, {Rt.RelativePosition.z.ToString().Replace(",", ".")}f)";
            Server.Get.Logger.Info(Rotation);
            result.State = CommandResultState.Ok;
            result.Message = Rt.ToString();
            return result;
        }
    }
}
