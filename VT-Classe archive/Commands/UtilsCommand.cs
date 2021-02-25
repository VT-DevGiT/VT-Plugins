using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Utils",
    Aliases = new[] { "Utils" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Utils"
    )]
    public class UtilsCommand : ISynapseCommand
    {
        private SerializedItem GetUtilsConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigUtils.FirstOrDefault(c => c.ID == (int)classId);

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
