using Synapse.Api.Enum;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_PNJ.API
{
    public static class NpcDataInit
    {
        public static NpcPathZone TestCheminZone = null;

        public static void ClearNpc() => BaseNpcScript.NpcList.Clear();

        public static void InitPointForTest()
        {
            if (TestCheminZone != null)
                return;
            TestCheminZone = new NpcPathZone("Salle de test", ZoneType.None);
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 184.4124f, -6.226257f, -59.35204f), 1));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 147.3329f, -6.236572f, -58.87164f), 2));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 83.85808f, -11.46069f, -59.09703f), 3));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 60.53611f, -11.46997f, -59.03751f), 4));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 74.43254f, -11.47089f, -51.50195f), 5));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 150.9656f, -5.909424f, -49.67073f), 6));
            TestCheminZone.Points.Add(new NpcMapPointRoute(new SerializedMapPoint("Root_*&*Outside Cams", 85.73423f, -5.909363f, -49.77233f), 7));
            TestCheminZone.AddPath(1, 2);
            TestCheminZone.AddPath(2, 3);
            TestCheminZone.AddPath(3, 4);
            TestCheminZone.AddPath(5, 3);
            TestCheminZone.AddPath(5, 4);
            TestCheminZone.AddPath(1, 6);
            TestCheminZone.AddPath(6, 7);
            TestCheminZone.AddPath(6, 3, false);
            TestCheminZone.AddPath(6, 4, false);
            TestCheminZone.AddPath(7, 3, false);
            TestCheminZone.AddPath(7, 4, false);
        }

    }
}
