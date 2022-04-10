using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTLog
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestart;
        }

        private void OnRoundRestart()
        {
            Log.Write("--------------- End of Log ---------------");
            Log.CreateNew();
        }
    }
}