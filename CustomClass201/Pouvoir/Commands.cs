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
           Name = "MoveVent",
           Aliases = new[] { "Vent" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Vent"
           )]
    class CmdMoveVent : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.MoveVent);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "LightHack",
           Aliases = new[] { "LumiereHack" },
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
           Aliases = new[] { "PortHack" },
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
           Name = "CASSIEHack",
           Aliases = new[] { "MessageHack" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".CASSIEhack"
           )]
    class CmdCASSIEHack : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.CASSIEHack);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "Respawn",
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
           Name = "SwitchRole",
           Aliases = new[] { "ChangeRole" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".SwitchRole or .ChangeRole"
           )]
    class CmdSwitchRole : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.SwitchRole);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "DropShield",
           Aliases = new[] { "DropShield" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".DropShield"
           )]
    class CmdDropShield : ISynapseCommand
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
           Name = "Suicide",
           Aliases = new[] { "Suicide" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Suicide"
           )]
    class CmdSuicide : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Suicide);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
           Name = "Zombification",
           Aliases = new[] { "Zombification", "Zombie", "Zombi" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Zombifaction"
           )]
    class CmdZombification : ISynapseCommand
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
    [CommandInformation(
           Name = "Defibrilateur",
           Aliases = new[] { "Defibri" },
           Description = "to use the capacity of your role",
           Permission = "",
           Platforms = new[] { Platform.ClientConsole },
           Usage = ".Defibri"
           )]
    class CmdDefibrillator : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Defibrillation);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
          Name = "BadGreen",
          Aliases = new[] { "Green" },
          Description = "to use the capacity of your role",
          Permission = "",
          Platforms = new[] { Platform.ClientConsole },
          Usage = ".Green"
          )]
    class CmdBadGreen : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.BadGreen);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }
    [CommandInformation(
          Name = "attack",
          Aliases = new[] { "attaque", "attack" },
          Description = "to use the capacity of your role",
          Permission = "",
          Platforms = new[] { Platform.ClientConsole },
          Usage = ".attack"
          )]
    class CmdAttaque : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                script.CallPower(PowerType.Attack);
                result.State = CommandResultState.Ok;
            }
            else
                result.State = CommandResultState.NoPermission;
            return result;
        }
    }

}
