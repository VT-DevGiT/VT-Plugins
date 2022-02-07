using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Extension;

namespace VT939
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
                ev.Player.GetOrAddComponent<Scp939Controller>();
            else if (ev.Player.TryGetComponent<Scp939Controller>(out var ctr) && ctr.enabled)
                ctr.enabled = false;
        }
    }
}