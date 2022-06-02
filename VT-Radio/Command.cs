using MEC;
using Respawning;
using Synapse.Command;
using VT_Api.Core.Enum;

namespace VTRadio
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
        public CommandResult Execute(CommandContext context) // TODO fix this
        {
            var result = new CommandResult();
            if (context.Player.RoleID != (int)RoleID.NtfCommander && context.Player.RoleID != (int)RoleID.CdmCommander
             && context.Player.RoleID != (int)RoleID.NtfCaptain && context.Player.RoleID != (int)RoleID.NtfLieutenantColonel)
            {
                result.Message = "you do not have the accreditation for this order";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            else if (context.Player.ItemInHand.ID != (int)ItemType.Radio)
            {
                result.Message = "you need a radio !";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            else if (RespawnManager.Singleton._timeForNextSequence <= 15)
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "too close to a respawn";
                return result;
            }
            else if (!VtController.Get.MapAction.isAirBombCurrently)
            {
                VtController.Get.MapAction.StartAirBombardement(7, 5, context.Player);
                result.State = CommandResultState.Ok;
                result.Message = "Air Bomb Start";
                return result;
            }
            else
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "there is already a bombardment";
                return result;
            }
        }
    }
}
