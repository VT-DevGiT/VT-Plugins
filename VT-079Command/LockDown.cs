using Scp079Rework;
using Synapse.Api;
using Synapse.Command;
using System.Linq;
using UnityEngine;

namespace VT079.Command
{
    public class LockDown : I079Command
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 2);

        public float Energy => PluginExtensions.GetEnergy(Name, 45);

        public float Exp => PluginExtensions.GetEnergy(Name, 15);

        public string Name => "LockDown";

        public string Description => "Lock all door";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 100f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            
            if (Map.Get.Nuke.Active)
            {
                result.State = CommandResultState.Ok;
                result.Message = "The nuck is active you can do that";
            }
            else 
            {
                foreach (var door in Map.Get.Doors)
                    door.Open = false;
                result.State = CommandResultState.Ok;
                result.Message = "All door are lock";
            }
            return result;
        }
    }
}
