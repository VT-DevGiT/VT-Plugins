using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MapGeneration.ImageGenerator;

namespace VT_PNJ.API
{
    public class NpcMapPointRoute : MapPoint
    {
        public static List<NpcMapPointRoute> NpcMapPoints = new List<NpcMapPointRoute>();

        public static List<NpcMapPointRoute> NpcMapPointsLiaisons = new List<NpcMapPointRoute>();

        public NpcMapPointType Type = NpcMapPointType.Route;
        public uint Id;

        static public void Clear()
        {
            NpcMapPoints.Clear();
            NpcMapPointsLiaisons.Clear();
        }

        static public NpcMapPointRoute GetNearestPoint(Vector3 Position)
            => NpcMapPoints.OrderBy(p => Vector3.Distance(p.Position, Position)).FirstOrDefault();


        public NpcMapPointRoute(SerializedMapPoint serializedMapPoint, uint Id)
            : base(serializedMapPoint.ToString())
        {
            init(Id);
        }

        public NpcMapPointRoute(Synapse.Api.Room room, Vector3 position, uint Id)
            : base(room, position)
        {
            init(Id);
        }

        void init(uint id)
        {
            NpcMapPoints.Add(this);
            Id = (uint)id;
        }
    }
}
