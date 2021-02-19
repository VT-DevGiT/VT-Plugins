using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Necro",
    Aliases = new[] { "Necro" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Necro"
    )]
    public class NecroCommand : ISynapseCommand
    {
        private SerializedItem GetNecroConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigNecro.FirstOrDefault(c => c.ID == (int)classId);

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
