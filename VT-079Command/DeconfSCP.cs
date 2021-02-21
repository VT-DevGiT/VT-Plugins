using Scp079Rework;
using Synapse;
using Synapse.Api;
using Synapse.Command;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VT079.Command
{

    public class DeconfSCP : I079Command
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
            return result;
        }
    }
    
}
