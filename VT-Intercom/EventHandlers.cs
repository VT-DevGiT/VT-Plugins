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
        }

        private void OnSpeakIntercom(PlayerSpeakIntercomEventEventArgs ev)
        {
            if (!Plugin.Instance.Config.KeycardSpeak || ev.Player.IsDummy || ev.Player == Server.Get.Host || ev.Player.Bypass)
                return;
            if (!Map.Get.GetDoor(DoorType.Intercom).DoorPermissions.CheckPermissions(ev.Player.ItemInHand?.ItemBase, ev.Player.Hub) &&
                !Plugin.Instance.Config.KeycardSpeakIgnorRole.Contains(ev.Player.RoleID))
                ev.Allow = false;
            
        }
        private void OnRoundEnd()
        {
            Plugin.Instance.TeslaEnabled = true;
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
