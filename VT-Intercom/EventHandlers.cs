using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using MEC;
using Mirror;
using Subtitles;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Reflexion;
using static LightContainmentZoneDecontamination.DecontaminationController.DecontaminationPhase;

namespace VTIntercom
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
            VtController.Get.Events.Player.PlayerSpeakIntercomEvent += OnSpeakIntercom;
            VtController.Get.Events.Map.GeneratorActivatedEvent += OnGeneratorActivated;
        }

        private void OnSpeakIntercom(PlayerSpeakIntercomEventEventArgs ev)
        {
            if (!Plugin.Instance.Config.KeycardSpeak || ev.Player.IsDummy || ev.Player == Server.Get.Host || ev.Player.Bypass)
                return;
            if (!Map.Get.GetDoor(DoorType.Intercom).DoorPermissions.CheckPermissions(ev.Player.ItemInHand?.ItemBase, ev.Player.Hub) &&
                !Plugin.Instance.Config.KeycardSpeakIgnorRole.Contains(ev.Player.RoleID))
                ev.Allow = false;
            
        }

        private void OnGeneratorActivated(GeneratorActivatedEventArgs ev)
        {

            if (Map.Get.Generators.Where(p => p.Engaged == true).Count() == 2)
            {
                /*
                 * Vanila Phase :
                 * 0 = Decontamination Start
                 * 1 = Decontamination in 10 Minutes
                 * 2 = Decontamination in 5 Minutes | this one is use to do the cassie of the 2 Minutes
                 * 3 = Decontamination in 1 Minutes
                 * 4 = Decontamination Countdown
                 * 5 = Decontamination Lockdown
                 */

                DecontaminationController.Singleton.disableDecontamination = false;

                DecontaminationController.DecontaminationPhase[] newPhase = new[]
                {
                    new DecontaminationController.DecontaminationPhase()
                    {
                        TimeTrigger = (float)DecontaminationController.GetServerTime + 1,
                        Function = PhaseFunction.GloballyAudible,
                        AnnouncementLine = null
                    },
                    new DecontaminationController.DecontaminationPhase()
                    {
                        TimeTrigger = (float)DecontaminationController.GetServerTime + 2,
                        Function = PhaseFunction.OpenCheckpoints,
                        AnnouncementLine = null,

                    },
                    new DecontaminationController.DecontaminationPhase()
                    {
                        TimeTrigger = (float)DecontaminationController.GetServerTime + 120,
                        Function = PhaseFunction.Final,
                        AnnouncementLine = null
                    }
                };

                DecontPatch.DecontaminationPhases = newPhase;
                DecontaminationController.Singleton.SetField<int>("_nextPhase", 0);
                foreach (var room in Server.Get.Map.Rooms.FindAll(p => p.Zone == ZoneType.LCZ)) foreach (var door in room.Doors)
                    if (door.DoorPermissions.RequiredPermissions == KeycardPermissions.None)
                    {
                        door.Locked = true;
                        door.Open = true;
                    }

                Map.Get.Cassie("Decontamination sequence commencing in 2 minutes");
                Subtitle(new SubtitlePart(SubtitleType.DecontaminationMinutes, new string[1]
                            {
                            "2"
                            }));

                Timing.CallDelayed(60f, () =>
                {
                    Map.Get.Cassie("Decontamination sequence commencing in 1 minute");
                    Subtitle(new SubtitlePart(SubtitleType.Decontamination1Minute, null));
                });

                Timing.CallDelayed(125f, () =>
                {
                    Map.Get.Cassie("Light Containment Zone is locked down and ready for decontamination .");
                    Subtitle(new SubtitlePart(SubtitleType.DecontaminationLockdown, null));
                });
                VTIntercom.Plugin.Instance.DecontInProgress = true;
                DecontaminationController.Singleton._stopUpdating = false;
            }
            
            void Subtitle(SubtitlePart subtitle)
            {
                foreach (var player in Server.Get.Players)
                {
                    if (player.Zone == Synapse.Api.Enum.ZoneType.LCZ)
                        player.Connection.Send(new SubtitleMessage(new[] { subtitle }));
                }
            }
        }

        private void OnRoundEnd()
        {
            Plugin.Instance.TeslaEnabled = true;
            Plugin.Instance.DecontInProgress = false;
        }

        private void OnRoundStart()
        {
            DecontaminationController.Singleton.disableDecontamination = true;
            Plugin.Instance.Intercom = Server.Get.Host.GetComponent<Intercom>();
            Plugin.Instance.Intercom.gameObject.AddComponent<IntercomBehaviour>();
        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            if (!Plugin.Instance.TeslaEnabled)
                ev.Trigger = false;
            else if (Plugin.Instance.Config.TeslaRadio && ev.Player.Inventory.Items.FirstOrDefault(p => p.ID == (int)ItemType.Radio && !p.IsCustomItem) != null)
            { 
                ev.Trigger = false;
                Tesla tesla = ev.Tesla;
                float oldTriggerSize = tesla.SizeOfTrigger;
                tesla.SizeOfTrigger = -1;
                Timing.CallDelayed(2.5f, () => tesla.SizeOfTrigger = oldTriggerSize);
            }
        }
    }

}
