using Synapse.Api;
using Synapse.Api.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.NpcScript;
using VT_Referance.Variable;

namespace VT_Referance.Behaviour
{
    [API]
    public class NpcMouvControler : BaseRepeatingBehaviour
    {
        BaseNpcScript Npc;
        Player player;

        Vector3? _Goto;
        NpcMapPoint FirstPoint;
        NpcMapPoint LastPoint;
        Vector3 NextPostion;
        List<Vector3> Parcour = new List<Vector3>();
        public Vector3? Goto
        {
            get { return _Goto; }
            set
            {
                _Goto = value;
                if (value == null)
                    enabled = false;
                else
                { 
                    FirstPoint = NpcMapPoint.GetNearestPoint(Npc.Position);
                    LastPoint = NpcMapPoint.GetNearestPoint((Vector3)value);
                    NextPostion = FirstPoint.Position;
                    CreatParcour();
                    enabled = true;
                }
            }
        }

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            Npc = (BaseNpcScript)Map.Get.Dummies.FirstOrDefault(x => x.Player == player);
            base.Start();
            enabled = false;
        }

        protected override void OnDisable()
        {
            Npc.Direction = MovementDirection.Stop;
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            Npc.Direction = MovementDirection.Forward;
            CreatParcour();
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            Npc.Movement = Npc.MouventState();
            Npc.RotateToPosition(NextPostion);
            if (Vector3.Distance(Npc.Position, NextPostion) > 1.5f)
                CalNextPoint();
            if (_Goto == null || Vector3.Distance(Npc.Position, (Vector3)_Goto) > 1.5f)
                enabled = false;
        }

        private void CreatParcour()
        {
            Parcour.Clear();
            if (FirstPoint == LastPoint)
            {
                NextPostion = (Vector3)_Goto;
                return;
            }
            Parcour.Add(FirstPoint.Position);
            Parcour.Concat(Finder.StartFinderRecusif(FirstPoint, LastPoint));
            Parcour.Add(LastPoint.Position);
        }

        private void CalNextPoint()
        {
            Parcour.Remove(NextPostion);
            NextPostion = Parcour.FirstOrDefault();
        }
        
        static private class Finder
        {
            static List<NpcMapPoint> ChekPoint;

            static public List<Vector3> StartFinderRecusif(NpcMapPoint first, NpcMapPoint last)
            {
                ChekPoint.Clear();
                List<NpcMapPoint> ParcourPoint = FinderRecusif(first, last);
                List<Vector3> ParcourVector3 = new List<Vector3>();
                foreach (var Point in ParcourPoint)
                    ParcourVector3.Insert(0, Point.Position);
                return ParcourVector3;
            }

            static List<NpcMapPoint> FinderRecusif(NpcMapPoint first, NpcMapPoint last)
            {
                List<NpcMapPoint> Parcour = null;
                if (first.AdjacentMappoints.Where(p => p == last).Any())
                {
                    Parcour.Add(first.AdjacentMappoints.Where(p => p == last).FirstOrDefault());
                    return Parcour;
                }
                foreach (var Point in first.AdjacentMappoints.Where(p => !ChekPoint.Contains(p)))
                {
                    ChekPoint.Add(Point);
                    Parcour.Concat(FinderRecusif(Point, last));
                    if (Parcour != null)
                        Parcour.Add(Point);
                }
                return Parcour;
            }

        }
    }
}
