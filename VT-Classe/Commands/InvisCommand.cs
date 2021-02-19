using CustomClass.PlayerScript;
using CustomPlayerEffects;
using Synapse.Api.Enum;
using Synapse.Command;
using Synapse.Config;
using System.Linq;
using UnityEngine;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Invis",
    Aliases = new[] { "Invis" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Invis"
    )]
    public class InvisCommand : ISynapseCommand
    {
        private SerializedItem GetInvisConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigLocate.FirstOrDefault(c => c.ID == (int)classId);

            return result ?? new SerializedItem((int)classId, 10, 5, 0, 0, Vector3.one);
        }

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                SerializedItem LocaleConfig = GetInvisConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    if (context.Player.PlayerEffectsController.GetEffect<Scp268>().Enabled)
                    {
                        context.Player.PlayerEffectsController.DisableEffect<Scp268>();
                        result.Message = "invisibility disabled";
                        result.State = CommandResultState.Ok;
                        return result;
                    }

                    //cooldown

                    if (LocaleConfig.Durabillity != -1)
                    {
                        context.Player.GiveEffect(Effect.Scp268, (byte)LocaleConfig.Durabillity);
                        result.Message = $"invisibility activated for {LocaleConfig.Durabillity}";
                        result.State = CommandResultState.Ok;
                        return result;
                    }
                    else
                    {
                        context.Player.GiveEffect(Effect.Scp268);
                        result.Message = "invisibility activated";
                        result.State = CommandResultState.Ok;
                        return result;
                    }
                }
            }
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;
        }
    }
}
