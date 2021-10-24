using Scp079Rework;
using Synapse.Command;
using UnityEngine;

namespace VT079.Command
{
    public class Nuke : I079Command
    {
        public KeyCode Key => KeyCode.Alpha9;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 4);

        public float Energy => PluginExtensions.GetEnergy(Name, 150);

        public float Exp => PluginExtensions.GetExp(Name, 30);

        public string Name => "nuke";

        public string Description => "Start or Stop the Alfa WarHead";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 0f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.State = CommandResultState.Ok;
            if (AlphaWarheadController.Host.inProgress)
            {
                AlphaWarheadController.Host.CancelDetonation();
                result.Message = "Alfa-WarHead Stop";
            }
            else
            {
                AlphaWarheadController.Host.InstantPrepare();
                AlphaWarheadController.Host.StartDetonation();
                result.Message = "Alfa-WarHead Start";
            }
            return result;
        }
    }
}
