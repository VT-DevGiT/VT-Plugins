using CustomClass.PlayerScript;
using Synapse.Api.Enum;
using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Flash",
    Aliases = new[] { "Flash" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Flash"
    )]
    public class FlashCommand : ISynapseCommand
    {
        private SerializedItem GetFlashConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigFlash.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                SerializedItem LocaleConfig = GetFlashConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    // cooldown

                    Synapse.Api.Map.Get.Explode(context.Player.Position, GrenadeType.Flashbang, context.Player);
                    result.Message = "All players who looked at you should now be flashed";
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;


        }
    }
}
