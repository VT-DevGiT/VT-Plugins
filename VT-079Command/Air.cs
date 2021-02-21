using MEC;
using Respawning;
using Scp079Rework;
using Synapse.Command;
using UnityEngine;

namespace VT079.Command
{
    public class Air : I079Command
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => 4;

        public float Energy => 150;

        public float Exp => 30;

        public string Name => "air";

        public string Description => "Start air bombardment";

        public float Cooldown => 0f;

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (RespawnManager.Singleton.GetFieldValue<float>("_timeForNextSequence") <= 15)
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "too close to a respawn";
            }
            if (!Methode._isAirBombGoing)
            {
                Timing.RunCoroutine(Methode.AirSupportBomb(10, 5));
                result.State = CommandResultState.Ok;
                result.Message = "Air Bomb Start";
            }
            else
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "there is already a bombardment";
            }
            return result;
        }
    }
}
