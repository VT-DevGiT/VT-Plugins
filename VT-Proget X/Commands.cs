using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Command;
using VT_Referance.Method;
using static LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase;

namespace VTProget_X
{
    [CommandInformation(
        Name = "Decontamination",
        Aliases = new[] { "Decont" },
        Description = "fort Start the for Decontamination",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Decont and you must be at the intercom with a tablet in hand and a power of 1000 kVA"
    )]
    public class DecontaminationCommand : ISynapseCommand
    {

        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "you must be at the intercom with a tablet in hand and a power of 1000 kVA";
            result.State = CommandResultState.NoPermission;
            if (Plugin.Instance.DecontInProgress)
            {
                result.Message = "decontamination has already taken place";
                result.State = CommandResultState.NoPermission;
                Server.Get.Logger.Info(DecontaminationController.Singleton.TimeOffset);
                return result;
            }
            if (Methods.GetVoltage() >= 1000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Map.Get.GlitchedCassie("Decontamination sequence commencing in 2 minutes");
                Starting.phase = 0;
                Server.Get.Logger.Info(DecontaminationController.Singleton.TimeOffset);
                var phase = DecontaminationController.Singleton.DecontaminationPhases;
                for(int i = 0; i < phase.Length; i++)
                {
                    var elem = phase[i];
                }
                DecontaminationController.DecontaminationPhase[] newPhase = new DecontaminationController.DecontaminationPhase[3];
                newPhase[0] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 1,
                    Function = PhaseFunction.None,
                    AnnouncementLine = phase[phase.Length - 3].AnnouncementLine
                };
                newPhase[1] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 2,
                    Function = PhaseFunction.OpenCheckpoints,
                    AnnouncementLine = phase[phase.Length - 2].AnnouncementLine,

                };
                newPhase[2] = new DecontaminationController.DecontaminationPhase()
                {
                    TimeTrigger = (float)DecontaminationController.GetServerTime + 120,
                    Function = PhaseFunction.Final,
                    AnnouncementLine = phase[phase.Length - 1].AnnouncementLine
                };
                Starting.DecontaminationPhases = newPhase;
                DecontaminationController.Singleton.SetField<int>("_nextPhase", 0);
                foreach (var room in Server.Get.Map.Rooms.FindAll(p => p.Zone == ZoneType.LCZ)) foreach (var door in room.Doors)
                if (door.DoorPermissions.RequiredPermissions == KeycardPermissions.None)
                {
                    door.Locked = true;
                    door.Open = true;
                }
                Timing.CallDelayed(125f, () =>
                {
                    Map.Get.GlitchedCassie("Light Containment Zone is locked down and ready for decontamination .");
                });
                result.Message = "Decontamination Start";
                result.State = CommandResultState.Ok;
                Plugin.Instance.DecontInProgress = true;
            }
            return result;
        }
    }
    [CommandInformation(
        Name = "Tesla",
        Aliases = new[] { "" },
        Description = "A Command for Enable/Diabled all tesla",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Tesla and you must be at the intercom with a tablet in hand and a power of 2000 kVA"
        )]
    public class TeslaCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (Methods.GetVoltage() >= 2000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                if (Plugin.Instance.TeslaEnabled)
                {
                    Map.Get.Cassie("all tesla doors have been disabled .", false);
                    result.Message = "Tesla Diabled";
                }
                else
                {
                    Map.Get.Cassie("all tesla doors have been enable .", false);
                    result.Message = "Tesla Enabled";
                }
                Plugin.Instance.TeslaEnabled = !Plugin.Instance.TeslaEnabled;

                result.State = CommandResultState.Ok;
            }
            else
            {
                result.Message = "you must be at the intercom with a tablet in hand and a power of 2000 kVA";
                result.State = CommandResultState.NoPermission;
            }
            return result;
        }
    }


    [CommandInformation(
        Name = "blackout",
        Aliases = new[] { "Blackout" },
        Description = "A command to deactivate the lights",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .blackout and you must be at the intercom with a tablet in hand and a power of 3000 kVA"
    )]
    public class blackoutCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (Methods.GetVoltage() >= 3000 && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player?.Room?.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Generator079.mainGenerator.ServerOvercharge(Plugin.Config.BlackOutTime, false);
                result.Message = "blackout start";
                result.State = CommandResultState.Ok;
            }
            else
            {
                result.Message = "you must be at the intercom with a tablet in hand and a power of 3000 kVA";
                result.State = CommandResultState.NoPermission;
            }
            return result;
        }
    }
    [CommandInformation(
        Name = "I like Train",
        Aliases = new[] { "Train" },
        Description = "TestIntercomSwitch",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "je sais pas"
    )]
    public class TrainCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            result.Message = "nop :(";
            result.State = CommandResultState.NoPermission;
            if (context.Player.UserId == "76561198836602642@steam" && context.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
             && context.Player.Room.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
            {
                Plugin.Instance.CustomScreen = !Plugin.Instance.CustomScreen;
                result.Message = "Sa marche :)";
                result.State = CommandResultState.Ok;
            }
            return result;
        }
    }
}
