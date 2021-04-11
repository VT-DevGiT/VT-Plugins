using MEC;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;

namespace JetonClassManger
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.WaitingForPlayersEvent += Waiting;
            Server.Get.Events.Round.RoundStartEvent += RoudStart;
        }

        private void RoudStart()
        {
            Timing.CallDelayed(Plugin.Config.TimRolSwitch, () => Plugin.Instance.PlayerCanSwitch = false);
            Server.Get.Map.Items.Where(p => p.ItemHolder == null);
        }

        private void Waiting()
        {
            Plugin.Instance.PlayerCanSwitch = true;
        }
    }
}