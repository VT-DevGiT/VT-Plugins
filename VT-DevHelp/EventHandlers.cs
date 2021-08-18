using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTDevHelp
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSyncDataEvent += OnSyncData;
        
        }

        private void OnSyncData(PlayerSyncDataEventArgs ev)
        {
            if (ev.Player.AnimationController.curAnim == 1)
            {
                ev.Allow = false;
            }
        }
    }
}