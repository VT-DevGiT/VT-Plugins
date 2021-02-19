using Scp079Rework;
using Synapse;
using Synapse.Api;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT079.Command
{
    class DeconfSCP : I079Command
    {
        public KeyCode Key => KeyCode.None;

        public int RequiredLevel => 5;

        public float Energy => 200;

        public float Exp => 0;

        public string Name => "deconf";

        public string Description => "deconfine a random scp but you lose 2 levels";

        public float Cooldown => 420f;

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            IEnumerable<Player> Spectator = Server.Get.Players.Where(p => p.Team == Team.RIP);

            if (!Plugin.SCPRoleDeconf.Any())
            {
                result.Message = "you cannot free a SCP, all SCP are unconfined";
                result.State = CommandResultState.NoPermission;
                return result;
            }
            else if (!Spectator.Any())
            {
                result.Message = "no spectator is present to take the place of the SCP";
                result.State = CommandResultState.NoPermission;
                context.Player.Hub.scp079PlayerScript.Mana += 200;
                return result;
            }
            else
            {
                Map.Get.Cassie($"Alert. New containment .g1 breach detected. Cassie .g2 corruption detected. Code .g4 red.", false);
                context.Player.Hub.scp079PlayerScript.NetworkcurLvl -= 2;
                int newRole = Plugin.SCPRoleDeconf.ElementAt(UnityEngine.Random.Range(0, Plugin.SCPRoleDeconf.Count() - 1));
                Player player = Spectator.ElementAt(UnityEngine.Random.Range(0, Spectator.Count() - 1));
                Plugin.SCPRoleDeconf.Remove(newRole);
                player.RoleID = newRole;
                result.State = CommandResultState.Ok;
                result.Message = $"An SCP has been deconfined";
                return result;
            }
        }
    }
}
