using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Surge",
    Aliases = new[] { "Surge" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Surge"
    )]
    public class SurgeCommand : ISynapseCommand
    {
        private SerializedItem GetSurgeConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigSurge.FirstOrDefault(c => c.ID == (int)classId);

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
