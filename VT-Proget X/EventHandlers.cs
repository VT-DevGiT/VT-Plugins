using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;

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
            Timing.KillCoroutines("Decont");
        }

        private void OnRoundStart()
        {
            Server.Get.Host.GetComponent<Intercom>().gameObject.AddComponent<IntercomBehaviour>();
        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            if (ev.Player.Inventory.Items.FirstOrDefault(p => p.ID == (int)ItemType.WeaponManagerTablet) != null || !Plugin.Instance.TeslaEnabled)
                ev.Trigger = false;
            else
                ev.Trigger = true;
        }
    }

}
