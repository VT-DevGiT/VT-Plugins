using VT_MultieLoder.API;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;

namespace VT_MultieLoder
{
    public class EventHandler
    {
        public EventHandler()
        {
            Server.Get.Events.Player.LoadComponentsEvent += OnLoadCOmponents;
        }

        private void OnLoadCOmponents(LoadComponentEventArgs ev)
        {
            if (ev.Player.GetComponent<PlayerInfoCompatiblity>() == null)
                ev.Player.AddComponent<PlayerInfoCompatiblity>();
        }
    }
}
