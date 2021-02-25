using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Vent",
    Aliases = new[] { "Vent" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Vent"
    )]
    public class VentCommand : ISynapseCommand
    {
        private SerializedItem GetVentConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigVent.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;
        }
    }
}
