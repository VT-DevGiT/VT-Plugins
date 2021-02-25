using CustomClass.PlayerScript;
using Synapse.Command;

namespace CustomClass.Commands
{
    [CommandInformation(
     Name = "ClassInfo",
     Aliases = new[] { "ClassInfo" },
     Description = "",
     Permission = "",
     Platforms = new[] { Platform.ClientConsole },
     Usage = ".ClassInfo"
     )]
    public class ClassInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {

            var result = new CommandResult();

            if (context.Arguments.Count < 1)
                context.Arguments = new System.ArraySegment<string>(new[] { "" });

            if (context.Player.CustomRole is BasePlayerScript script)
            {

                result.Message = $"You are {script.ClasseID} : {script.GetRoleName()}";
                result.State = CommandResultState.Ok;
                return result;
            }
            else
            {
                result.Message = "You are not a special classe";
                result.State = CommandResultState.NoPermission;
                return result;
            }
        }
    }
}
