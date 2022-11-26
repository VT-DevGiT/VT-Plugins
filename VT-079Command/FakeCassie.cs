using Scp079Rework;
using Synapse.Api;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Reflexion;

namespace VT079.Command
{
    public class FakeCassie : I079Command
    {
        const string CommandName = "FakeCassie";

        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 3);

        public float Energy => PluginExtensions.GetEnergy(Name, 30);

        public float Exp => PluginExtensions.GetEnergy(Name, 20);

        public string Name => CommandName;

        public string Description => "Play a custom cassie, use help to now the list of the custom cassie";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 480f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var arg = string.Join(" ", context.Arguments.ToArray());

            var annoces = VT079.Plugin.Instance.Config.Annonces;

            if (int.TryParse(arg, out int index))
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

                result.Message = "Do \".079 [id]\" to play the cassie message:";
                result.State = CommandResultState.Error;

                var length = annoces.Count;
                var i = 0;

                foreach (string cassie in annoces.Keys)
                {
                    i++;
                    result.Message += $"\n{i} - \"{cassie}\"";
                }
            }

            return result;
        }
    }
}
