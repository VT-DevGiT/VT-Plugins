using Mirror;
using Synapse;
using Synapse.Api;

namespace VTDevHelp
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {

        }
    }
}