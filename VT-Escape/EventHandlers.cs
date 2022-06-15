using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Extension;

namespace VTEscape
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
            Server.Get.Events.Player.PlayerEscapesEvent += OnEscapesEvent;
            Server.Get.Events.Player.LoadComponentsEvent += OnLoadComonentsEvent;
        }

        private void OnLoadComonentsEvent(LoadComponentEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
                ev.Player.GetOrAddComponent<NTFEscape>();
            if (Plugin.Config.ICEscapeIsEnabled)
                ev.Player.GetOrAddComponent<CHIEscape>();
        }

        private void OnEscapesEvent(PlayerEscapeEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
                ev.Allow = false;
        }

        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            var escapes = ev.Player.GetComponents<BaseEscape>();
            foreach (var escape in escapes)
                escape.enabled = true;
        }
    }
}