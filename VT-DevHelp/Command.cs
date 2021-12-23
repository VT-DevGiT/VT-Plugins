using Assets._Scripts.Dissonance;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using Synapse;
using Synapse.Api;
using Synapse.Api.Items;
using Synapse.Api.Roles;
using Synapse.Command;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VoiceChatManager.Core.Audio.Capture;
using VoiceChatManager.Core.Audio.Playback;
using VoiceChatManager.Core.Extensions;
using VT_Referance.Method;
using Xabe.FFmpeg;

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

            var door = Synapse.Api.Door.SpawnDoorVariant(Plugin.DoorPosition.Parse().Position, Plugin.DoorRotation);
            door.Open = true;
            door.Open = false;
            
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
    public class AdvenceEscapeCommand : ISynapseCommand
    {
        const string ConvertedFileExtension = ".f32le";
        string DurationFormat = "hh\\:mm\\:ss\\.ff";
        string InvalidVolumeError = "{0} is not a valid volume, range varies from 0 to 100!";
        string PlayCommandUsage = 
             "\nvoicechatmanager play [File alias/File path] [Volume (0-100)]" +
             "\nvoicechatmanager play [File alias/File path] [Volume (0-100)] [Channel name (SCP, Intercom, Proximity, Ghost)]" +
             "\nvoicechatmanager play [File alias/File path] [Volume (0-100)] proximity [Player ID/Player Name/Player]" +
             "\nvoicechatmanager play [File alias/File path] [Volume (0-100)] proximity [X] [Y] [Z]";
        string FFmpegDirectoryIsNotSetUpProperlyError = "Your FFmpeg directory isn't set up properly, \"path\" won't be converted and played.";
        bool IsFFmpegInstalled = false;
        string GetChannelName(string rawChannelName)
        {
            switch (rawChannelName.ToLower())
            {
                default:
                case "intercom":
                    return "Intercom";

                case "p":
                case "proximity":
                    return "Proximity";

                case "spec":
                case "spectators":
                case "spectator":
                case "ghost":
                    return "Ghost";

                case "scp":
                    return "SCP";
            }
        }
        
        public static async Task<IConversionResult> ConvertFileAsync(string path, int sampleRate = 48000, int channels = 1, float speed = 1, Format format = Format.f32le, ConversionPreset preset = ConversionPreset.Medium, bool canOverwriteOutput = true, string extraParameters = null)
        {
            return await ConvertFileAsync(path, default, sampleRate, channels, speed, format, preset, canOverwriteOutput, extraParameters);
        }

        public static async Task<IConversionResult> ConvertFileAsync(string path, CancellationToken cancellationToken, int sampleRate = 48000, int channels = 1, float speed = 1, Format format = Format.f32le, ConversionPreset preset = ConversionPreset.Medium, bool canOverwriteOutput = true, string extraParameters = null)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(null, path);

            var conversion = FFmpeg.Conversions.New()
                .AddParameter($"-i \"{path}\" -ar {sampleRate} -ac {channels} -filter:a \"atempo = {speed}\"", ParameterPosition.PreInput)
                .SetOutput($"{Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path))}.{format}")
                .SetOutputFormat(format)
                .SetOverwriteOutput(canOverwriteOutput)
                .SetPreset(preset);

            if (!string.IsNullOrEmpty(extraParameters))
                conversion.AddParameter(extraParameters);

            return await conversion.Start(cancellationToken);
        }

        public CommandResult Execute(CommandContext context)
        {
            
            var result = new CommandResult();
            if (context.Arguments.Count < 2 || context.Arguments.Count > 6 || context.Arguments.Count == 5)
            {
                result.Message = PlayCommandUsage;
                result.State = CommandResultState.Error;
                return result;
            }

            if (!float.TryParse(context.Arguments.At(1), out var volume))
            {
                result.Message = string.Format(InvalidVolumeError, context.Arguments.At(1));
                result.State = CommandResultState.Error;
                return result;
            }

            var channelName = context.Arguments.Count == 2 ? "Intercom" : GetChannelName(context.Arguments.At(2));

            /*if (!VoiceChatManager.Instance.Config.Presets.TryGetValue(context.Arguments.At(0), out var path))*/
            string path = context.Arguments.At(0);

            var convertedFilePath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)) + ConvertedFileExtension;

            if (File.Exists(path) && !path.EndsWith(ConvertedFileExtension) && !File.Exists(convertedFilePath))
            {
                if (!IsFFmpegInstalled)
                {
                    result.Message = string.Format(FFmpegDirectoryIsNotSetUpProperlyError, path);
                    result.State = CommandResultState.Error;
                    return result;
                }

                result.Message = $"Converting \"{path}\"...";

                ConvertFileAsync(path).ContinueWith(
                    task =>
                    {
                        if (task.IsCompleted)
                        {
                            var arg = context.Arguments.ToArray();
                            arg[0] = convertedFilePath;

                            var isSucceded = Execute(new CommandContext() { Arguments = new ArraySegment<string>(arg), Platform = context.Platform, Player = context.Player });
                        }
                        else
                        {
                            Server.Get.Logger.Error(string.Format("Failed to convert \"{0}\": {1}", path, task.Exception));
                        }
                    }, TaskContinuationOptions.ExecuteSynchronously);

                result.State = CommandResultState.Ok;
                return result;
            }

            if (int.TryParse(path, out var id) && id.TryPlay(volume, channelName, out var streamedMicrophone))
            {
                result.Message = string.Format(
                    "Playing \"{0}\" with {1} volume on \"{2}\" channel, duration: {3}", id, volume, streamedMicrophone.ChannelName, streamedMicrophone.Duration.ToString(DurationFormat));

                result.State = CommandResultState.Ok;
                return result;
            }

            if (!path.EndsWith(ConvertedFileExtension))
                path = convertedFilePath;

            if (context.Arguments.Count == 2 || context.Arguments.Count == 3)
            {
                if (path.TryPlay(volume, channelName, out streamedMicrophone))
                {
                    result.Message = string.Format(
                        "Playing \"{0}\" with {1} volume on \"{2}\" channel, duration: {3}", id, volume, streamedMicrophone.ChannelName, streamedMicrophone.Duration.ToString(DurationFormat));

                    result.State = CommandResultState.Ok;
                    return result;
                }
            }
            else if (context.Arguments.Count == 4)
            {
                int.TryParse(context.Arguments.At(3), out int playerId);
                Player player = Server.Get.Players.FirstOrDefault(p => p.PlayerId == playerId);
                if (player?.gameObject == null)
                {
                    result.Message = string.Format("Player \"{0}\" not found!", context.Arguments.At(3));
                    result.State = CommandResultState.Error;
                    return result;
                }
                else if (path.TryPlay(Talker.GetOrCreate(player.gameObject), volume, channelName, out streamedMicrophone))
                {
                    result.Message = string.Format(
                        "Playing \"{0}\" with {1} volume, in the proximity of \"{2}\", duration: {3}", path, volume, player.NickName, streamedMicrophone.Duration.ToString(DurationFormat));
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }
            else
            {
                if (!float.TryParse(context.Arguments.At(3), out var x))
                {
                    result.Message = string.Format("\"{0}\" is not a valid {1} coordinate!", context.Arguments.At(3), "x");
                    result.State = CommandResultState.Error;
                    return result;
                }
                else if (!float.TryParse(context.Arguments.At(4), out var y))
                {
                    result.Message = string.Format("\"{0}\" is not a valid {1} coordinate!", context.Arguments.At(4), "y");
                    result.State = CommandResultState.Error;
                    return result;
                }
                else if (!float.TryParse(context.Arguments.At(5), out var z))
                {
                    result.Message = string.Format("\"{0}\" is not a valid {1} coordinate!", context.Arguments.At(5), "z");
                    result.State = CommandResultState.Error;
                    return result;
                }
                else if (path.TryPlay(new Vector3(x, y, z), volume, channelName, out streamedMicrophone))
                {
                    result.Message = string.Format(
                        "Playing \"{0}\" with {1} volume, in the proximity of ({2}, {3}, {4}) duration: {5}", path, volume, x, y, z, streamedMicrophone.Duration.ToString(DurationFormat));
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }

            result.Message = string.Format("Audio \"{0}\" not found or it's already playing!", path);
            result.State = CommandResultState.Error;
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
    public class itemInfo : ISynapseCommand
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
    public class TestDecont : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.Decontamination?.CallMethod("InstantStart");
            Server.Get.Map.Decontamination?.Controller?.CallMethod("FinishDecontamination");
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
    public class Song : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.State = CommandResultState.Error;
            if (int.TryParse(context.Arguments.FirstElement(),out int ID))
            {
                Methods.PlayAmbientSound(ID);
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
    public class DevGrenad : ISynapseCommand
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
    public class Clear : ISynapseCommand
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

