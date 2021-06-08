using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT_Referance.Variable.Npc
{
    [API]
    public class NpcMapPoint : MapPoint
    {
        public static List<NpcMapPoint> NpcMapPoints = new List<NpcMapPoint>();

        public static List<NpcMapPoint> NpcMapPointsLiaisons = new List<NpcMapPoint>();

        public NpcMapPointType Type;
        public uint Id;
        
        static public void Clear()
        {
            NpcMapPoints.Clear();
            NpcMapPointsLiaisons.Clear();
            CheminZone.Clear();
        }

        static public NpcMapPoint GetNearestPoint(Vector3 Position)
        {
            List<NpcMapPoint> AllPoint = NpcMapPoints;
            AllPoint.OrderBy(p => Vector3.Distance(p.Position, Position));
            return AllPoint.FirstOrDefault();
        }

        public NpcMapPoint(SerializedMapPoint serializedMapPoint, uint Id, NpcMapPointType type = NpcMapPointType.None) 
            : base(serializedMapPoint.ToString())
        {
            init(Id);
        }

        public NpcMapPoint(Room room, Vector3 position, uint Id, NpcMapPointType type = NpcMapPointType.None) 
            : base(room, position)
        {
            init(Id);
        }
        
        void init(uint id)
        {
            NpcMapPoints.Add(this);
            Id = id;
        }
    }
}
