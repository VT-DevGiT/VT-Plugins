using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Summon",
    Aliases = new[] { "Summon" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Summon"
    )]
    public class SummonCommand : ISynapseCommand
    {
        private SerializedItem GetSummonConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigSummon.FirstOrDefault(c => c.ID == (int)classId);

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
