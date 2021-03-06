using Synapse;
using Synapse.Api;
using Synapse.Command;
using System.Linq;
using VT_Referance.Method;

namespace VT079
{
    [CommandInformation(
        Name = "Reconf079",
        Aliases = new[] { "reconf", "r079" },
        Description = "A Command for reconfine scp 079",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Reconf and you must be at the confinement of 079 with a tablet in hand and a power of 5000 kVA"
        )]
    public class Reconf : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (Methods.Voltage() == 5000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player.Room.RoomType == RoomInformation.RoomType.HCZ_079)
            {
                var listJoueur = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Scp079);

                if (!listJoueur.Any())
                {
                    result.Message = "Scp 079 is already reconfined";
                    result.State = CommandResultState.NoPermission;
                    return result;
                }
                foreach (var joueur079 in listJoueur)
                {
                    joueur079.GodMode = false;
                    joueur079.RoleID = (int)RoleType.Spectator;
                    Map.Get.Cassie($"SCP 0 7 9 has been successfully contained.", false);
                }
                result.Message = "SCP 079 has been successfully contained";
                result.State = CommandResultState.Ok;
                return result;
            }
            else
            {
                result.Message = "you must be at the confinement of 079 with a tablet in hand and a power of 5000 kVA";
                result.State = CommandResultState.NoPermission;
            }
            return result;
        }
    }
}
