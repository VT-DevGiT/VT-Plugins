using MEC;
using Scp079Rework;
using Synapse.Command;
using UnityEngine;

namespace VT079.Command
{
    class Gaz : I079Command
    {
        public KeyCode Key => KeyCode.Alpha4;

        public int RequiredLevel => 3;

        public float Energy => 30;

        public float Exp => 5;

        public string Name => "gaz";

        public string Description => "Gaz the current room";

        public float Cooldown => 10f;

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.Room == null)
            {
                context.Player.Hub.scp079PlayerScript.Mana += 30;
                result.Message = "You can't gass here";
                result.State = CommandResultState.NoPermission;
                return result;
            }

            Timing.RunCoroutine(Methode.GasRoom(context.Player.Room, context.Player.Hub));

            return result;
        }
    }
}
