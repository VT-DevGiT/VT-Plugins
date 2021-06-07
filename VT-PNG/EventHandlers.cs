using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using static RoomInformation;

namespace VT_PNJVT_PNJ
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            Data.Npc.NpcMapPoints.Clear();
            Data.Npc.NpcMapPointsLiaisons.Clear();
            List<NpcMapPoint> OutsidePoint = new List<NpcMapPoint>();
            NpcMapPoint nextpoin;
            OutsidePoint.Add(new NpcMapPoint(new SerializedMapPoint("Root_*&*Outside Cams", 190.4923f, -6.235657f, -59.48961f)));
            OutsidePoint.Add(new NpcMapPoint(new SerializedMapPoint("Root_*&*Outside Cams", 147.9429f, -5.888855f, -49.40312f)));
            nextpoin = new NpcMapPoint(new SerializedMapPoint("Root_*&*Outside Cams", 118.8969f, -11.32269f, -59.26124f));
            OutsidePoint.Add(nextpoin);
            List<NpcMapPoint> idk = new List<NpcMapPoint>();
            idk.Add(nextpoin);
            new NpcMapPoint(new SerializedMapPoint("Root_*&*Outside Cams", 48.72881f, -11.46997f, -59.25313f), idk);
            new NpcMapPoint(new SerializedMapPoint("Root_*&*Outside Cams", 154.1182f, -6.236511f, -59.00627f), OutsidePoint);
        }
    }
}