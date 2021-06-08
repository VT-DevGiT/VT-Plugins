using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.NpcScript;
using VT_Referance.Variable.Npc;

namespace VT_Referance.Behaviour
{
    [API]
    public class NpcControlMouvement : BaseRepeatingBehaviour
    {

        NpcControlMouvement()
        {
            this.RefreshTime = 100;
        }

        BaseNpcScript Npc;
        Player player;

        Vector3? _Goto;
        Vector3 _NextPostion 
        {
            get
            {
                if (Chemin != null && Chemin != new List<NpcMapPoint>())
                    return Chemin.FirstOrDefault().Position;
                else if (_Goto != null) return (Vector3)_Goto;
                else throw new NotImplementedException("Npc : No desitnation has been chosen !");
            }
        }


        List<NpcMapPoint> Chemin;

        public Vector3? Goto
        {
            get { return _Goto; }
            set
            {
                _Goto = value;
                if (value == null) enabled = false;
                else
                {
                    NpcMapPoint FirstPoint = NpcMapPoint.GetNearestPoint(Npc.Position);
                    Server.Get.Logger.Send($"PNJ #{Npc.Id} First :{FirstPoint.Position}", ConsoleColor.Yellow);
                    NpcMapPoint LastPoint = NpcMapPoint.GetNearestPoint((Vector3)value);
                    Server.Get.Logger.Send($"PNJ #{Npc.Id} Last : {LastPoint.Position}", ConsoleColor.Yellow);
                    if (LastPoint == FirstPoint)
                        Chemin = new List<NpcMapPoint>();
                    else
                    { 
                        Chemin = NpcDataControler.TestCheminZone.PlusCourtChemin(FirstPoint.Id, LastPoint.Id);
                        Server.Get.Logger.Send($"PNJ #{Npc.Id} Chemin", ConsoleColor.Yellow);
                        foreach (var point in Chemin)
                        { 
                            Server.Get.Logger.Send($"{point.Position}", ConsoleColor.Yellow);
                        }
                    }
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
            Npc.RotateToPosition(_NextPostion);
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            if (Chemin == null || _Goto == null)
                enabled = false;
            Npc.RotateToPosition(_NextPostion);
            if (Vector3.Distance(Npc.Position, _NextPostion) < 1f)
            {
                if (Vector3.Distance(Npc.Position, (Vector3)_Goto ) < 1.5f)
                    enabled = false;
                else Chemin.Remove(Chemin.First());
            }
        }

        
        
    }
}
