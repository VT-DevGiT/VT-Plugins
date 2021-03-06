using Assets._Scripts.Dissonance;
using Synapse;
using Synapse.Api.Items;
using Synapse.Command;
using System;
using System.Linq;
using UnityEngine;

namespace VTDevHelp
{
    [CommandInformation(
      Name = "DevDoorInfo",
      Aliases = new[] { "VTFdoor" },
      Description = "For find door",
      Permission = "synapse.command.Dev",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = ""
      )]
    public class ClassInfoCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            IOrderedEnumerable<Synapse.Api.Door> door = Synapse.Api.Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, context.Player.Position)));
            result.Message = $"No Door find";
            result.State = CommandResultState.Ok;
            if (door.Any())
            {
                result.Message = $"Door : \n Name -> {door.First().Name} \n Position -> {door.First().Position} \n Rotation -> {door.First().Rotation} \n Is Open -> {door.First().Open} \n Is Breakable -> {door.First().IsBreakable} \n Door Permissions -> {door.First().DoorPermissions}";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
     Name = "DevTest",
     Aliases = new[] { "VTTest" },
     Description = "For TEST",
     Permission = "synapse.command.Dev",
     Platforms = new[] { Platform.RemoteAdmin },
     Usage = ""
     )]
    public class AdvenceEscapeCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Radio radio = context.Player.gameObject.GetComponent<DissonanceUserSetup>().GetComponent<Radio>();
            Server.Get.Logger.Info(radio.presets[radio.lastPreset].beepRange);
            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
     Name = "DevitemInfo",
     Aliases = new[] { "VTIteam" },
     Description = "Dev iteam info",
     Permission = "synapse.command.Dev",
     Platforms = new[] { Platform.RemoteAdmin },
     Usage = ""
     )]
    public class itemInfo : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteamHand = context.Player?.ItemInHand;
            if (iteamHand == null)
            {
                result.State = CommandResultState.Error;
                return result;
            }

            Server.Get.Logger.Info($"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Barrel->{iteamHand.Barrel}, Sight->{iteamHand.Sight}, Other->{iteamHand.Other}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}");
            result.Message = $"{iteamHand} INFO : \n " +
                             $"Generale : ID->{iteamHand.ID}, Custom->{iteamHand.IsCustomItem}, Category->{iteamHand.ItemCategory}, Name->{iteamHand.Name}, Type->{iteamHand.ItemType}\n" +
                             $"Weapon : Barrel->{iteamHand.Barrel}, Sight->{iteamHand.Sight}, Other->{iteamHand.Other}, Durabillity->{iteamHand.Durabillity}\n" +
                             $"Other : Scale->{iteamHand.Scale}, Scale->{iteamHand.State}";
            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevDecont",
    Aliases = new[] { "VTDecont" },
    Description = "Dev Decont Test",
    Permission = "synapse.command.Dev",
    Platforms = new[] { Platform.RemoteAdmin, Platform.ServerConsole },
    Usage = ""
    )]
    public class TestDecont : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            Server.Get.Map.Decontamination?.CallMethod("InstantStart");
            Server.Get.Map.Decontamination?.Controller?.CallMethod("FinishDecontamination");
            result.State = CommandResultState.Error;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevGive",
    Aliases = new[] { "VTGIve" },
    Description = "Dev Give Test",
    Permission = "synapse.command.Dev",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DevGive : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int IDiteam = int.Parse(context.Arguments.FirstElement());
            int Durabliteam = int.Parse(context.Arguments.ElementAt(1));
            int Sight = int.Parse(context.Arguments.ElementAt(2));
            int Barrel = int.Parse(context.Arguments.ElementAt(3));
            int Other = int.Parse(context.Arguments.ElementAt(4));
            context.Player.Inventory.AddItem(new SynapseItem(IDiteam, Durabliteam, Sight, Barrel, Other));
            return result;
        }
    }

    [CommandInformation(
       Name = "DevSong",
       Aliases = new[] { "VTSong" },
       Description = "Dev Song Test",
       Permission = "synapse.command.Dev",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ""
       )]
    public class Song : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.State = CommandResultState.Error;
            int ID;
            if (int.TryParse(context.Arguments.FirstElement(), out ID))
            {
                DissonanceUserSetup dissonanceUserSetup = context.Player.gameObject.GetComponent<DissonanceUserSetup>();
                Method.PlayAmbientSound(ID);
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }

    [CommandInformation(
        Name = "DevRestart",
        Aliases = new[] { "Restart" },
        Description = "Un Plugin Crash ? pas problême c'est la pour toi !",
        Permission = "synapse.command.Dev",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "Utlise le si ta le serveur qui Crash pas autrement"
        )]
    public class Restart : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            switch (context.Arguments.FirstElement())
            {
                case "Server":
                    foreach (var player in Server.Get.Players)
                    {
                        player.SendBroadcast(2, "@Restart");
                    }
                    result.State = CommandResultState.Ok;
                    Server.Get.Reload();
                    Server.Get.GameConsole.TypeCommand("ForceStart");
                    break;
                case "Alert":
                    foreach (var player in Server.Get.Players)
                    {
                        player.SendBroadcast(5, "Restart");
                    }
                    result.State = CommandResultState.Ok;
                    break;
                case "Plugin":
                    Server.Get.Reload();
                    result.State = CommandResultState.Ok;
                    break;

            }
            return result;
        }
    }

    [CommandInformation(
    Name = "DevChat",
    Aliases = new[] { "VTChat" },
    Description = "Dev Chat Test",
    Permission = "synapse.command.Dev",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = "SCP, RIP, RAD, 079, 939, NONE"
    )]
    public class DevChat : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            if (context.Player.gameObject.GetComponent<ChatBehaviour>() == null)
                context.Player.gameObject.AddComponent<ChatBehaviour>();
            ChatBehaviour Chat = context.Player.gameObject.GetComponent<ChatBehaviour>();
            var result = new CommandResult();
            switch (context.Arguments.FirstElement())
            {
                case "SCP":
                    Chat.chat = 1;
                    break;

                case "RIP":
                    Chat.chat = 2;
                    break;

                case "RAD":
                    Chat.chat = 3;
                    break;

                case "079":
                    Chat.chat = 4;
                    break;

                case "939":
                    Chat.chat = 5;
                    break;

                case "NONE":
                    Chat.chat = 0;
                    break;

            }

            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
    Name = "DevGrenad",
    Aliases = new[] { "VTGrenad" },
    Description = "Dev Test Plugin",
    Permission = "synapse.command.Dev",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ""
    )]
    public class DevGrenad : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            int i = 10;
            context.Player.Cuffer = context.Player;
            if (context.Arguments.First() != null)
                int.TryParse(context.Arguments.First(), out i);
            while (i != 0)
            {
                i--;
                context.Player.Inventory.AddItem(ItemType.GrenadeFrag, 0, 0, 0, 0);
            }
            return result;
        }
    }

    [CommandInformation(
       Name = "DevClear",
       Aliases = new[] { "VTDClear" },
       Description = "Un Clear ? pas problême c'est la pour toi !",
       Permission = "synapse.command.Dev",
       Platforms = new[] { Platform.RemoteAdmin },
       Usage = ".VTClear (Iteam, Corp ou All)"
       )]
    public class Clear : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteams = Server.Get.Map.Items.Where(p => p.State == Synapse.Api.Enum.ItemState.Map);
            var ragdolls = Server.Get.Map.Ragdolls;
            switch (context.Arguments.FirstElement())
            {

                case "Iteam":

                    foreach (var iteam in iteams)
                        iteam.Destroy();
                    result.State = CommandResultState.Ok;
                    break;

                case "Corp":
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    result.State = CommandResultState.Ok;
                    break;

                case "All":
                    foreach (var iteam in iteams)
                        iteam.Despawn();
                    foreach (var ragdoll in ragdolls)
                        UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
                    
                    result.State = CommandResultState.Ok;
                    break;

            }
            return result;
        }
    }

    [CommandInformation(
    Name = "ClearIteam",
    Aliases = new[] { "VTIteamClear" },
    Description = "Un commande pour Clear",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ".VTClearIteam"
    )]
    public class ClearIteam : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var iteams = Server.Get.Map.Items.Where(p => p.State == Synapse.Api.Enum.ItemState.Map);
            if (iteams.Any())
                foreach (var iteam in iteams)
                    iteam.Despawn();
            result.State = CommandResultState.Ok;
            return result;
        }
    }

    [CommandInformation(
    Name = "ClearRagdolls",
    Aliases = new[] { "VTRagdollsClear", "VTCorpClear" },
    Description = "Un commande pour Clear",
    Permission = "",
    Platforms = new[] { Platform.RemoteAdmin },
    Usage = ".VTCorpClear"
    )]
    public class PblicClear : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            var ragdolls = Server.Get.Map.Ragdolls;
            if (ragdolls.Any())
                foreach (var ragdoll in ragdolls)
                    UnityEngine.Object.DestroyImmediate(ragdoll.GameObject, true);
            result.State = CommandResultState.Ok;
            return result;
        }
    }
}

