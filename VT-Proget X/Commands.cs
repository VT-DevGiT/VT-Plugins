using MEC;
using Synapse.Api;
using Synapse.Command;
using VT_Referance.Method;

namespace VTProget_X
{
    [CommandInformation(
        Name = "Decontamination",
        Aliases = new[] { "Decont" },
        Description = "fort Start the for Decontamination",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Decont and you must be at the intercom with a tablet in hand and a power of 1000 kVA"
    )]
    public class DecontaminationCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "you must be at the intercom with a tablet in hand and a power of 1000 kVA";
            result.State = CommandResultState.NoPermission;
            if (Plugin.Instance.DeconatmiantionendProgress)
            {
                result.Message = "decontamination has already taken place";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            if (Methods.GetVoltage() >= 1000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
                && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Timing.RunCoroutine(Methode.Decontamination(), "Decont");
                result.Message = "Decontamination Start";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }
    [CommandInformation(
        Name = "Tesla",
        Aliases = new[] { "" },
        Description = "A Command for Enable/Diabled all tesla",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Tesla and you must be at the intercom with a tablet in hand and a power of 2000 kVA"
        )]
    public class TeslaCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (Methods.GetVoltage() >= 2000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                if (Plugin.Instance.TeslaEnabled)
                {
                    Map.Get.Cassie("all tesla doors have been disabled .", false);
                    result.Message = "Tesla Diabled";
                }
                else
                {
                    Map.Get.Cassie("all tesla doors have been enable .", false);
                    result.Message = "Tesla Enabled";
                }
                Plugin.Instance.TeslaEnabled = !Plugin.Instance.TeslaEnabled;

                result.State = CommandResultState.Ok;
            }
            else
            {
                result.Message = "you must be at the intercom with a tablet in hand and a power of 2000 kVA";
                result.State = CommandResultState.NoPermission;
            }
            return result;
        }
    }


    [CommandInformation(
        Name = "blackout",
        Aliases = new[] { "Blackout" },
        Description = "A command to deactivate the lights",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .blackout and you must be at the intercom with a tablet in hand and a power of 3000 kVA"
    )]
    public class blackoutCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (Methods.GetVoltage() >= 3000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Generator079.mainGenerator.ServerOvercharge(Plugin.Config.BlackOutTime, false);
                result.Message = "blackout start";
                result.State = CommandResultState.Ok;
            }
            else
            {
                result.Message = "you must be at the intercom with a tablet in hand and a power of 3000 kVA";
                result.State = CommandResultState.NoPermission;
            }
            return result;
        }
    }
    [CommandInformation(
        Name = "I like Train",
        Aliases = new[] { "Train" },
        Description = "TestIntercomSwitch",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "je sais pas"
    )]
    public class TrainCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "nop :(";
            result.State = CommandResultState.NoPermission;
            if (context.Player.UserId == "76561198836602642@steam" && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player.Room.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Plugin.Instance.CustomScreen = !Plugin.Instance.CustomScreen;
                result.Message = "Sa marche :)";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }
    [CommandInformation(
       Name = "DebugTest",
       Aliases = new[] { "DebugTest" },
       Description = "",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ""
   )]
    public class IntercomTest : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = Methods.GetVoltage().ToString();
            result.State = CommandResultState.Ok;
            return result;
        }
    }
}
