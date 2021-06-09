using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Items;
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

        BaseNpcScript npc;
        Player player;

        Vector3? _Goto;
        Vector3 _NextPostion 
        {
            get
            {
                if (Chemin != null && Chemin.Any())
                    return Chemin.FirstOrDefault().Position;
                else if (Goto != null) return (Vector3)Goto;
                else throw new NotImplementedException("Npc : No desitnation has been chosen !");
            }
        }


        List<NpcMapPointRoute> Chemin;

        public Vector3? Goto
        {
            get { return _Goto; }
            set
            {
                _Goto = value;
                if (value == null) enabled = false;
                else
                {
                    NpcMapPointRoute FirstPoint = NpcMapPointRoute.GetNearestPoint(npc.Position);
                    Server.Get.Logger.Send($"PNJ #{npc.Id} First :{FirstPoint.Position}", ConsoleColor.Yellow);
                    NpcMapPointRoute LastPoint = NpcMapPointRoute.GetNearestPoint((Vector3)value);
                    Server.Get.Logger.Send($"PNJ #{npc.Id} Last : {LastPoint.Position}", ConsoleColor.Yellow);
                    if (LastPoint == FirstPoint)
                    {
                        Chemin = new List<NpcMapPointRoute>();
                        Server.Get.Logger.Send($"{_NextPostion}", ConsoleColor.Yellow);
                    }
                    else
                    { 
                        Chemin = NpcDataInit.TestCheminZone.PlusCourtChemin(FirstPoint.Id, LastPoint.Id);
                        Server.Get.Logger.Send($"PNJ #{npc.Id} Chemin", ConsoleColor.Yellow);
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
            npc = (BaseNpcScript)Map.Get.Dummies.FirstOrDefault(x => x.Player == player);
            base.Start();
            enabled = false;
        }

        protected override void OnDisable()
        {
            npc.Direction = MovementDirection.Stop;
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            npc.Direction = MovementDirection.Forward;
            npc.RotateToPosition(_NextPostion);
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            if (Chemin == null || _Goto == null)
            { 
                enabled = false;
                return;
            }
            tryOpenDoor();
            mouve();
        }

        protected virtual void tryOpenDoor()
        {
            DoorVariant Vdoor = player.LookingAt.GetComponentInParent<DoorVariant>();
            Synapse.Api.Door Door = Map.Get.Doors.Where(p => p.VDoor == Vdoor).FirstOrDefault();
            if (Vdoor != null && !Door.Open)
            {
                if (Door.DoorPermissions.RequiredPermissions != 0)
                { 
                    SynapseItem item = player.Inventory.Items.Where(
                    p => Door.DoorPermissions.CheckPermissions(p.ItemType, player.Hub)).FirstOrDefault();
                    player.VanillaInventory.curItem = item.ItemType;
                }
                enabled = false;
                Vdoor.ServerInteract(player.Hub, 0);
                Timing.CallDelayed(1, () =>
                {
                    if (Door.Open) enabled = true;
                    player.VanillaInventory.curItem = ItemType.None;
                });
            }
        }

        protected virtual void mouve()
        {
            npc.RotateToPosition(_NextPostion);
            if (Vector3.Distance(npc.Position, _NextPostion) < 1f)
            {
                if (Chemin.Any()) Chemin.Remove(Chemin.First());
                else enabled = false;
            }
        }
    }
}
