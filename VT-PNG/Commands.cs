using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.NpcScript;
using VT_Referance.Variable;

namespace VT_PNJ
{
    [CommandInformation(
        Name = "SpawnPNJ",
        Aliases = new[] { "NewPNJ", "PNJ", "NewNPC" },
        Description = "Spawn a NPC",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "PNJ"
        )]
    class CmdSpawnNPC : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            BaseNpcScript PNJ = new BaseNpcScript(
                context.Player.Position,
                context.Player.Rotation,
                context.Player.RoleType,
                context.Arguments.FirstElement(),
                "PNJ"
                );
            PNJ.Player.GodMode = false;
            result.State = CommandResultState.Ok;
            result.Message = $"New NPC id is \"{PNJ.Id}\"";
            return result;
        }
    }

    [CommandInformation(
        Name = "MovePNJ",
        Aliases = new[] { "MoveNPC" },
        Description = "for move a NPC",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "MovePNJ [Id]"
        )]
    class CmdMouveNPC : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int id;
            if (context.Arguments.Count == 1 && int.TryParse(context.Arguments.FirstOrDefault(), out id) && Data.Npc.NpcList.ContainsKey(id))
            {
                BaseNpcScript PNJ = Data.Npc.NpcList[id];
                PNJ.controler.Goto = context.Player.Position;
                result.State = CommandResultState.Ok;
                result.Message = $"NPC of the id : \"{PNJ.Id}\"; \n move to you !";
            }
            else
            {
                result.State = CommandResultState.Error;
                if (context.Arguments.Count != 1)
                    result.Message = "you must give the id of the Npc";
                else
                    result.Message = "No NPC has its id!";
            }
            return result;
        }
    }
}
