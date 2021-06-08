using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Referance.Method;

namespace VTProget_X
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
        }

        private void OnRoundEnd()
        {
            Plugin.Instance.TeslaEnabled = true;
            Plugin.Instance.DecontInProgress = false;
            Timing.KillCoroutines("Decont");
        }

        private void OnRoundStart()
        {
            Server.Get.Host.GetComponent<Intercom>().gameObject.AddComponent<IntercomBehaviour>();
        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            if (!Plugin.Instance.TeslaEnabled)
                ev.Trigger = false;
            else if (Plugin.Config.TeslaTablets && ev.Player.Inventory.Items.FirstOrDefault(p => p.ID == (int)ItemType.WeaponManagerTablet && !p.IsCustomItem) != null)
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
