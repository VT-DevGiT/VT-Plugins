using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JetonClassManger
{
    internal class EventHandlers
    {
        public EventHandlers()
        { Server.Get.Events.Round.RoundStartEvent += DebutRound;

        }

        private void DebutRound()
        {
           Plugin.Instance.PlayerCanSwitch = true ;
            Timing.CallDelayed(Plugin.Config.TempChangement, () => Plugin.Instance.PlayerCanSwitch = false);
        }
    }
}