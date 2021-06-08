using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT_Referance.Variable.Npc
{
    [API]
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
            CheminZone.Clear();
        }

        static public NpcMapPointRoute GetNearestPoint(Vector3 Position) 
            => NpcMapPoints.OrderBy(p => Vector3.Distance(p.Position, Position)).FirstOrDefault();


        public NpcMapPointRoute(SerializedMapPoint serializedMapPoint, uint Id) 
            : base(serializedMapPoint.ToString())
        {
            init(Id);
        }

        public NpcMapPointRoute(Room room, Vector3 position, uint Id)
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
