using Synapse;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournéeDeLaFondation
{
    [CommandInformation(
        Name = "Incident",
        Aliases = new[] { "Alert" },
        Description = "Pour appellez les MTF en cas d'alret",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".Alert"
        )]
    class IncidentCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.GlitchedCassie("Bell_Start ContainmentBreach AllRemaining Bell_End . CassieSystem enable");
            VTProget_X.Plugin.Instance.TeslaEnabled = true;

            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
        Name = "StopIncident",
        Aliases = new[] { "StopAlert" },
        Description = "Stop l'alert",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = ".StopAlert"
        )]
    class StopAlertCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.GlitchedCassie("Bell_Start AllSecured Bell_End . CassieSystem disabled");
            VTProget_X.Plugin.Instance.TeslaEnabled = false;

            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
       Name = "JourneeDeLaFondation",
       Aliases = new[] { "Journee" },
       Description = "Stop ou lance la journée de la fondation",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ".Journee"
       )]
    class OnorOffCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Plugin.Instance.Actif = !Plugin.Instance.Actif;
            if (Plugin.Instance.Actif)
                result.Message = "La journée est activée";
            else
                result.Message = "La journée est désactivée";
            result.State = CommandResultState.Ok
            return result;
        }
    }
}
