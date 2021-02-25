using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Noclip",
    Aliases = new[] { "Noclip" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Noclip"
    )]
    public class NoclipCommand : ISynapseCommand
    {
        private SerializedItem GetNoclipConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigNoclip.FirstOrDefault(c => c.ID == (int)classId);

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
