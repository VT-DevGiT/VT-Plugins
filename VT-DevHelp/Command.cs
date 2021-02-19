using Assets._Scripts.Dissonance;
using Synapse;
using Synapse.Command;
using System;
using System.Linq;
using UnityEngine;

namespace VT079
{
    [CommandInformation(
      Name = "DevDoorInfo",
      Aliases = new[] { "Fdoor" },
      Description = "For find door",
      Permission = "",
      Platforms = new[] { Platform.RemoteAdmin },
      Usage = ""
      )]
    public class ClassInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            IOrderedEnumerable<Synapse.Api.Door> lst = Synapse.Api.Map.Get.Doors.OrderBy(p => Math.Abs(Vector3.Distance(p.Position, context.Player.Position)));
            result.Message = $"No Door find";
            result.State = CommandResultState.Ok;
            if (lst.Any())
            {
                result.Message = $"Door : \n Name -> {lst.First().Name} \n Position -> {lst.First().Position} \n Rotation -> {lst.First().Rotation} \n Is Open -> {lst.First().Open} \n Is Breakable -> {lst.First().IsBreakable} \n Door Permissions -> {lst.First().DoorPermissions}";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }
    [CommandInformation(
     Name = "DevTest",
     Aliases = new[] { "Test" },
     Description = "For TEST",
     Permission = "",
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
        Aliases = new[] { "Iteam" },
        Description = "Dev iteam info",
        Permission = "",
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
    Aliases = new[] { "" },
    Description = "Dev Decont Test",
    Permission = "",
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
       Name = "DevSong",
       Aliases = new[] { "Song" },
       Description = "Dev Song Test",
       Permission = "",
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
        Name = "DevChat",
        Aliases = new[] { "Chat" },
        Description = "Dev Chat Test",
        Permission = "",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "SCP, RIP, RAD, 079, 939, NONE"
        )]
    public class Chat : ISynapseCommand
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
                    Chat.SCP = true;
                    break;

                case "RIP":
                    Chat.RIP = true;
                    break;

                case "RAD":
                    Chat.RAD = true;
                    break;

                case "079":
                    Chat.IAA = true;
                    break;

                case "939":
                    Chat.DOG = true;
                    break;

                case "NONE":
                    Chat.SCP = false;
                    Chat.RIP = false;
                    Chat.RAD = false;
                    Chat.IAA = false;
                    Chat.DOG = false;
                    break;

            }

            result.State = CommandResultState.Ok;
            return result;
        }
    }
}
