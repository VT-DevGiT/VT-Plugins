using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.NpcScript;
using VT_Referance.Variable;
using VT_Referance.Variable.Npc;

namespace VT_PNJ
{
    [CommandInformation(
        Name = "SpawnPNJ",
        Aliases = new[] { "NewPNJ", "PNJ", "NewNPC" },
        Description = "Spawn a NPC",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "PNJ [Name = null]"
        )]
    class CmdSpawnNPC : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            BaseNpcScript PNJ;
            if (!context.Arguments.Any())
            {
                PNJ = new BaseNpcScript(
                               context.Player.Position,
                               context.Player.Rotation,
                               context.Player.RoleType,
                               "J'aime les trains",
                               "PNJ"
                               );
            }
            else
            { 
                PNJ = new BaseNpcScript(
                    context.Player.Position,
                    context.Player.Rotation,
                    context.Player.RoleType,
                    context.Arguments.FirstElement(),
                    "PNJ"
                    );
            }
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
            uint id;
            if (context.Arguments.Count == 1 && uint.TryParse(context.Arguments.FirstOrDefault(), out id) && BaseNpcScript.NpcList.ContainsKey(id))
            {
                BaseNpcScript PNJ = BaseNpcScript.NpcList[id];
                PNJ.Mouvement.Goto = context.Player.Position;
                result.State = CommandResultState.Ok;
                result.Message = $"NPC of the id : \"{PNJ.Id}\"; \n move to you !\n Position : {context.Player.Position}";
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

    [CommandInformation(
        Name = "GoToMovePNJ",
        Aliases = new[] { "GoToGoToPNJ" },
        Description = "go to the go to of the NPC",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "GoToGoToPNJ [Id]"
        )]
    class CmdGotoMouveNPC : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            uint id;
            if (context.Arguments.Count == 1 && uint.TryParse(context.Arguments.FirstOrDefault(), out id) && BaseNpcScript.NpcList.ContainsKey(id))
            {
                BaseNpcScript PNJ = BaseNpcScript.NpcList[id];
                if (PNJ.Mouvement.Goto != null)
                { 
                    context.Player.Position = (Vector3)PNJ.Mouvement.Goto;
                    result.State = CommandResultState.Ok;
                    result.Message = $"you move to the NPC of the id : \"{PNJ.Id}\";";
                }
                else
                {
                    result.State = CommandResultState.Error;
                    result.Message = $"the NPC of the id : \"{PNJ.Id}\"; go no weher!";
                }
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

    [CommandInformation(
        Name = "GoToPointPNJ",
        Aliases = new[] { "GoPointPNJ" },
        Description = "go to the PointNPC",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "GoPointPNJ [Id]"
        )]
    class CmdGotoGoPointNPC : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int id;
            if (context.Arguments.Count == 1 && int.TryParse(context.Arguments.FirstOrDefault(), out id))
            {
                NpcMapPoint PointPNJ = NpcMapPoint.NpcMapPoints.Where(p => p.Id == id).FirstOrDefault();
                if (PointPNJ != null)
                {
                    context.Player.Position = PointPNJ.Position;
                    result.State = CommandResultState.Ok;
                    result.Message = $"you move to the PointPNJ of the id : \"{PointPNJ.Id}\";";
                }
                else
                {
                    result.State = CommandResultState.Error;
                    result.Message = "No PointPNJ has its id!";
                }
            }
            else
            {
                result.State = CommandResultState.Error;
                if (context.Arguments.Count != 1)
                    result.Message = "you must give the id of the PointPNJ";
                else
                    result.Message = "No PointPNJ has its id!";
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "ListPointPNJ",
       Aliases = new[] { "ListPointPNJ" },
       Description = "List of all PointNPC",
       Permission = "",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = "ListPointPNJ"
       )]
    class CmdListPointPNJ : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Logger.Send($"All point :", ConsoleColor.White);
            foreach (var point in NpcMapPoint.NpcMapPoints)
            {
                Server.Get.Logger.Send($"{point.Id} : {point.Position}", ConsoleColor.White);
            }
            result.State = CommandResultState.Ok;
            result.Message = "chek the consol of the serveur";
            return result;
        }
    }
}
