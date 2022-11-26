using Scp079Rework;
using Synapse;
using Synapse.Api;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT079.Command
{

    public class DeconfSCP : I079Command
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => PluginExtensions.GetRequiredLevel(Name, 5);

        public float Energy => PluginExtensions.GetEnergy(Name, 200);

        public float Exp => PluginExtensions.GetEnergy(Name, 0);

        public string Name => "deconf";

        public string Description => "deconfine a random scp but you lose 2 levels";

        public float Cooldown => PluginExtensions.GetCooldown(Name, 420f);

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            List<Player> Spectator = Server.Get.Players.Where(x => x.Team == Team.RIP).ToList();


            if (Plugin.Instance.SCPRoleDeconf.Any() && Spectator.Any())
            {
                Map.Get.Cassie($"Alert . New containment .g1 breach detected . Code .g4 red .");
               
                context.Player.Hub.scp079PlayerScript.Network_curLvl -= 2;
                int index = UnityEngine.Random.Range(0, Plugin.Instance.SCPRoleDeconf.Count);
                int newRole = Plugin.Instance.SCPRoleDeconf.ElementAt(index);
                Player player = Spectator.ElementAt(UnityEngine.Random.Range(0, Spectator.Count));
                player.RoleID = newRole;
            }
            else
            {
                context.Player.Scp079Controller.Energy =+ 200;
                result.Message = "you cannot currently free a scp, try again later";
            }
            return result;
        }
    }
}
