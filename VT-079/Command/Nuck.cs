using Scp079Rework;
using Synapse.Command;
using UnityEngine;

namespace VT079.Command
{
    class Nuck : I079Command
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => 4;

        public float Energy => 150;

        public float Exp => 30;

        public string Name => "nuck";

        public string Description => "Start or Stop the Alfa WarHead";

        public float Cooldown => 0f;

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
