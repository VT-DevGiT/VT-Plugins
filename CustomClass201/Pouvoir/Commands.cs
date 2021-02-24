using CustomClass.PlayerScript;
using CustomClass.Pouvoir;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClass
{
    [CommandInformation(
           Name = "MouveVent",
           Aliases = new[] { "Vent" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Vent"
           )]
    class CmdMouveVent : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.MouveVent);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "LightHack",
           Aliases = new[] { "LightHack" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".LichHack"
           )]
    class CmdLightHack : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.LightHack);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "DoorHack",
           Aliases = new[] { "DoorHack" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".DoorHack"
           )]
    class CmdDoorHack : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.DoorHack);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "MessageHack",
           Aliases = new[] { "CassieHack" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Messagehack"
           )]
    class CmdMessageHack : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.MessageHack);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "Renfort",
           Aliases = new[] { "Renfort" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Respawn"
           )]
    class CmdRespawn : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Respawn);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "ChangeRole",
           Aliases = new[] { "ChangeRole" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".ChangeRole"
           )]
    class CmdChangeRole : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.ChangeRole);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "DropSheld",
           Aliases = new[] { "DropSheld" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".DropSheld"
           )]
    class CmdDropSheld : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.DropSheld);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "Sucide",
           Aliases = new[] { "Sucide" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Sucide"
           )]
    class CmdSucide : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Sucide);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "Zombifaction",
           Aliases = new[] { "Zombifaction" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Zombifaction"
           )]
    class CmdZombifaction : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Zombifaction);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
}
