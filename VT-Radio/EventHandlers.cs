using Synapse;
using Synapse.Api.Events.SynapseEventArguments;

namespace VT079
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
        }

        private void OnSpeak(PlayerSpeakEventArgs ev)
        {
            if (ev.RadioTalk)
            {

            }
        }
    }
}