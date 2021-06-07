using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT_Referance.Variable
{
    public class NpcMapPoint : MapPoint
    {
        public NpcMapPointType Type;

        private List<NpcMapPoint> _AdjacentMappoints;

        public List<NpcMapPoint> AdjacentMappoints
        {
            get
            {
                if (Type != NpcMapPointType.Liaison) return _AdjacentMappoints;
                else return new List<NpcMapPoint>() { PointPair() }.Concat(_AdjacentMappoints).ToList();
            }
            set
            {
                _AdjacentMappoints = value;
                if ((int)Type > 4)
                {
                    if (_AdjacentMappoints.Count == 2)
                        Type = NpcMapPointType.Route;
                    else if (_AdjacentMappoints.Count > 2)
                        Type = NpcMapPointType.Croisement;
                    else if (_AdjacentMappoints.Count < 2)
                        Type = NpcMapPointType.Impasse;
                }
            }
        }

        private NpcMapPoint PointPair() => GetNearestPoint(this.Position, this);

        static public NpcMapPoint GetNearestPoint(Vector3 Distance, NpcMapPoint This = null)
        {
            NpcMapPoint NearPoint = null;
            float? NearPointDist = null;

            foreach (var PossiblePoint in This == null ? Data.Npc.NpcMapPointsLiaisons : Data.Npc.NpcMapPointsLiaisons.Where(p => p != This))
            {
                float TestDist = Vector3.Distance(PossiblePoint.Position, Distance);
                if (NearPoint == null || NearPointDist == null || TestDist < NearPointDist)
                {
                    NearPoint = PossiblePoint;
                    NearPointDist = TestDist;
                }
            }
            return NearPoint;
        }

        public NpcMapPoint(SerializedMapPoint serializedMapPoint, List<NpcMapPoint> adjacentMappoints = null, NpcMapPointType type = NpcMapPointType.Undefined) 
            : base(serializedMapPoint.ToString())
        {
            Init(adjacentMappoints, type);
        }

        public NpcMapPoint(Room room, Vector3 position, List<NpcMapPoint> adjacentMappoints = null, NpcMapPointType type = NpcMapPointType.Undefined) 
            : base(room, position)
        {
            Init(adjacentMappoints, type);
        }
        private void Init(List<NpcMapPoint> adjacentMappoints, NpcMapPointType type)
        {
            AdjacentMappoints = adjacentMappoints;
            if (type == NpcMapPointType.Liaison)
                Data.Npc.NpcMapPointsLiaisons.Add(this);
            if (adjacentMappoints != null)
                foreach (var point in adjacentMappoints)
                {
                    if (!point.AdjacentMappoints.Contains(this))
                        point.AdjacentMappoints.Add(this);
                }
            Data.Npc.NpcMapPoints.Add(this);
        }
    }
}
