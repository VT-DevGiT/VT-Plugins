using MEC;
using Respawning;
using Scp079Rework;
using Synapse.Command;
using System.Linq;
using UnityEngine;

namespace VT079.Command
{
    public class Air : I079Command
    {
        public KeyCode Key => KeyCode.Alpha0;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 4);

        public float Energy => PluginExtensions.GetEnergy(Name, 150);

        public float Exp => PluginExtensions.GetEnergy(Name, 30);

        public string Name => "air";

        public string Description => "Start air bombardment";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 120f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (RespawnManager.Singleton._timeForNextSequence <= 15)
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "too close to a respawn";
            }
            if (!VtController.Get.MapAction.isAirBombCurrently)
            {
                VtController.Get.MapAction.StartAirBombardement(10, 5);
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
