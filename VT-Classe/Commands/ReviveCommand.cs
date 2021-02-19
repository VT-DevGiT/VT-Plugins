using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Revive",
    Aliases = new[] { "Revive" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Revive"
    )]
    public class ReviveCommand : ISynapseCommand
    {
        private SerializedItem GetReviveConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigRevive.FirstOrDefault(c => c.ID == (int)classId);

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
