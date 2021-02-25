using CustomClass.PlayerScript;
using CustomPlayerEffects;
using MEC;
using Synapse.Api.Enum;
using Synapse.Command;
using Synapse.Config;
using System.Linq;
using UnityEngine;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Locate",
    Aliases = new[] { "Locate" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Locate"
    )]
    public class LocateCommand : ISynapseCommand
    {
        private SerializedItem GetLocateConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigLocate.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }

        public CommandResult Execute(CommandContext context)
        {

            var result = new CommandResult();
            if ((context.Player.CustomRole is BasePlayerScript script))
            {
                SerializedItem LocaleConfig = GetLocateConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    var effect = context.Player.PlayerEffectsController.GetEffect<Visuals939>();
                    bool remove = false;
                    if (effect == null)
                    {
                        context.Player.GiveEffect(Effect.Visuals939);
                        remove = true;
                    }
                    else if (!effect.Enabled)
                    {
                        context.Player.PlayerEffectsController.EnableEffect<Visuals939>();
                        remove = true;
                    }

                    if (LocaleConfig != null)
                    {
                        Collider[] lcolliders = Physics.OverlapSphere(context.Player.Position, LocaleConfig.Durabillity);
                        foreach (Collider PlayerCollider in lcolliders.Where(c => c.gameObject.GetPlayer() != null))
                        {
                            PlayerCollider.gameObject.GetPlayer().Hub.footstepSync?.CmdScp939Noise(100f);
                        }

                    }
                    if (remove)
                    {
                        Timing.CallDelayed(LocaleConfig.Sight, () => { context.Player.PlayerEffectsController.DisableEffect<Visuals939>(); });
                    }
                    result.Message = "";
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }

            result.Message = "";
            result.State = CommandResultState.Error;
            return result;
        }
    }
}
