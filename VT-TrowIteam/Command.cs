using MEC;
using Synapse.Command;

namespace VTTrowItem
{

    [CommandInformation(
    Name = "Trow",
    Aliases = new[] { "Trow" },
    Description = "A Command for Trow the curent iteam in your hand",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = "Use .Trow and you need to have item in your hand"
    )]
    public class TeslaCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var item = context.Player?.ItemInHand;
            if (item == null)
            {
                result.Message = "you need to have item in your hand";
                result.State = CommandResultState.NoPermission;
                return result;

            }
            item.Drop();
            Timing.RunCoroutine(Method.Throw(item, (context.Player.Hub.PlayerCameraReference.forward + Plugin.Config.addLaunchForce).normalized));
            return result;
        }
    }
}
