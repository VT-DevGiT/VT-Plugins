using Synapse;
using Synapse.Command;
using System.Linq;

namespace VT_AdminHelp
{
    [CommandInformation(
        Name = "Ticket", 
        Aliases = new string[] { "AdminHelp", "StaffHelp" },
        Description = "Pour demandée de l'aide à un staff présent", 
        Permission = "commandplugin.hello", 
        Platforms = new[] { Platform.ServerConsole }, 
        Usage = ".Ticket [la raison de la demande] tous abus sera senctionné" 
        )]
    public class HelloWorldCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Arguments == null)
            {
                result.Message = "Vous devez donnée une raison";
                result.State = CommandResultState.Error;

            }

            var Staffs = Server.Get.Players.Where( p => p.RemoteAdminAccess == true);
            if (Staffs != null)
            {
                foreach (var Staff in Staffs)
                {
                    Staff.SendBroadcast(10, Plugin.Config.AlertTicketMessage.Replace("%Ticket%", $"{context.Arguments}").Replace("\\n", "\n"));
                }
                context.Player.Jail.JailPlayer(Server.Get.Host);
            }
            else
            { 
                result.Message = "il n'y à pas de staff présente";
                result.State = CommandResultState.Error;
            }
            return result;
        }
    }
}