using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;


namespace AdvencedKeycard
{
    internal class EventHandlers
    {
        private readonly SerializedMapPoint AL01 = new SerializedMapPoint("LCZ_Airlock (1)", 0.01907826f, -0.1871053f, -4.647591f); // y+2,2
        private readonly SerializedMapPoint AL02 = new SerializedMapPoint("LCZ_Airlock (2)", 0.2492218f, -1.854498f, -5.502045f);
        private readonly SerializedMapPoint doorSpawn = new SerializedMapPoint("HCZ_049", -6.683522f, 264.0f, 24.09575f);

        public EventHandlers()
        {
            Server.Get.Events.Round.WaitingForPlayersEvent += Waiting;
        }

        private void Waiting()
        {


        }
    }
}