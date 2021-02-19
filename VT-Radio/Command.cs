using MEC;
using Respawning;
using Synapse.Command;

namespace VT079
{
    [CommandInformation(
        Name = "Avion",
        Aliases = new[] { "AirBomb" },
        Description = "A Command for Start air bombardmen",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Avion and you must ave a radio in hand"
        )]
    public class Avion : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.RoleID != (int)RoleType.NtfCommander)
            {
                result.Message = "you do not have the accreditation for this order";
                result.State = CommandResultState.NoPermission;
            }
            else if (context.Player.ItemInHand.ID != (int)ItemType.Radio)
            {
                result.Message = "you need a radio !";
                result.State = CommandResultState.NoPermission;
            }
            else if (RespawnManager.Singleton.GetFieldValue<float>("_timeForNextSequence") <= 15)
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "too close to a respawn";
            }
            else if (!Methode._isAirBombGoing)
            {
                Timing.RunCoroutine(Methode.AirSupportBomb(7, 5));
                result.State = CommandResultState.Ok;
                result.Message = "Air Bomb Start";
            }
            else
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "there is already a bombardment";
            }
            return result;
        }
    }

}
