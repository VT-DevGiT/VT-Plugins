using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTDevHelp
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Server.UpdateEvent += OnUpdate;
        
        }

        private void OnUpdate()
        {
            foreach (var player in Server.Get.Players)
            {

            }
        }
    }
}