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
            Plugin.Instance.DeconatmiantinEnd = false;
            Plugin.Instance.DeconatmiantionendProgress = false;
            Timing.KillCoroutines("Decont");
        }

        private void OnRoundStart()
        {
            Server.Get.Host.GetComponent<Intercom>().gameObject.AddComponent<IntercomBehaviour>();
        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            if ((ev.Player.Inventory.Items.FirstOrDefault(p => p.ID == (int)ItemType.WeaponManagerTablet && !p.IsCustomItem) != null && Plugin.Config.TeslaTablets)
                || !Plugin.Instance.TeslaEnabled)
                ev.Trigger = false;
            else
                ev.Trigger = true;
        }
    }

}
