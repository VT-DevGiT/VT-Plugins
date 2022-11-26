using Scp079Rework;
using Synapse.Api;
using Synapse.Command;
using System.Linq;
using UnityEngine;

namespace VT079.Command
{
    internal class FakeCassie
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 3);

        public float Energy => PluginExtensions.GetEnergy(Name, 30);

        public float Exp => PluginExtensions.GetEnergy(Name, 20);

        public string Name => "FakeCassie";

        public string Description => "Play a custom cassie, use help to now the list of the custom cassie";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 480f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var arg = string.Join(" ", context.Arguments.ToArray());

            Synapse.Api.Logger.Get.Info('"' + arg + '"');

            var annoces = VT079.Plugin.Config.Annonces;

            if (arg == "help")
            {
                result.Message = "Do \".079 id\" to play the cassie message:";
                result.State = CommandResultState.Ok;
                
                var length = annoces.Count;
                var i = 0;
                
                foreach (string cassie in annoces.Keys)
                {
                    i++;
                    result.Message += $"\n{i} - \"{cassie}\"";

                }
            }
            else if (int.TryParse(arg, out int index))
            {
                Map.Get.Cassie(annoces.ElementAt(index + 1).Value);
                
                result.State = CommandResultState.Ok;
                result.Message = "Cassie Played";
            }
            else if (annoces.ContainsKey(arg))
            {
                Map.Get.Cassie(annoces[arg]);
                
                result.Message = "Cassie Played";
                result.State = CommandResultState.Ok;
            }
            else
            {
                result.State = CommandResultState.Error;
                result.Message = "Do \".079 FakeCassie\" help to get the list of posisble cassie.";
            }

            return result;
        }
    }
}
