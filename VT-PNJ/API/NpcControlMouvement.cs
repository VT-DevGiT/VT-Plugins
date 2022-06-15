using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Behaviour;

namespace VT_PNJ.API
{
    public class NpcControlMouvement : RepeatingBehaviour
    {
        #region Constructors & Destructor
        NpcControlMouvement()
        {
            this.RefreshTime = 100;
            player = gameObject.GetPlayer();
            npc = (BaseNpcScript)Map.Get.Dummies.FirstOrDefault(x => x.Player == player);
        }
        #endregion

        #region Attributes & Properties

        public readonly BaseNpcScript npc;
        public readonly Player player;
        List<NpcMapPointRoute> Chemin;
        Vector3? _Goto;


        Vector3 _NextPostion
        {
            get
            {
                if (Chemin != null && Chemin.Any()) return Chemin.FirstOrDefault().Position;
                else if (Goto != null) return (Vector3)Goto;
                else throw new NotImplementedException("Npc : No desitnation has been chosen !");
            }
        }
        Synapse.Api.Door _Door
        {
            get
            {
                DoorVariant Vdoor = player.LookingAt?.GetComponentInParent<DoorVariant>();
                if (Vdoor == null) return null;
                return Map.Get.Doors.Where(p => p.VDoor == Vdoor).FirstOrDefault();
            }
        }

        public Vector3? Goto
        {
            get { return _Goto; }
            set
            {
                _Goto = value;
                if (value == null) enabled = false;
                else
                {
                    enabled = true;
                    
                    NpcMapPointRoute FirstPoint = NpcMapPointRoute.GetNearestPoint(npc.Position);
                    NpcMapPointRoute LastPoint = NpcMapPointRoute.GetNearestPoint((Vector3)value);
                    if (LastPoint == FirstPoint) Chemin = new List<NpcMapPointRoute>();
                    else Chemin = NpcDataInit.TestCheminZone.ShorterPath(FirstPoint.Id, LastPoint.Id);
                    enabled = true;
                }
            }
        }

        #endregion

        #region Methods
        protected override void Start()
        {
            enabled = false;
        }

        protected override void OnDisable()
        {
            npc.Direction = MovementDirection.Stop;
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            /*
            npc.Direction = MovementDirection.Forward;
            npc.RotateToPosition(_NextPostion);*/
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            if (Chemin == null || _Goto == null)
            { 
                enabled = false;
                return;
            }

            if (TryOpenDoor()) Timing.RunCoroutine(DoorChek());
            mouve();
        }
        #endregion

        #region Interaction

        protected virtual void mouve()
        {
            npc.RotateToPosition(_NextPostion);
            if (Vector3.Distance(npc.Position, _NextPostion) < 1f)
            {
                if (Chemin.Any()) Chemin.Remove(Chemin.First());
                else enabled = false;
            }
        }

        // train ... 
        protected bool TryInteract() => false;


        protected virtual bool TryOpenDoor()
        {
            if (_Door == null)
                return false;
            if (Vector3.Distance(_Door.Position, npc.Position) < 3 && !_Door.Open)
            {
                if (_Door.DoorPermissions.RequiredPermissions != 0 && !player.Bypass)
                {
                    SynapseItem item = player.Inventory.Items.FirstOrDefault(i => _Door.DoorPermissions.CheckPermissions(i.ItemBase, player.Hub));
                    if (item == null) return false;
                    npc.HeldItem = item.ItemType;
                }
                return TryInteract();
            }
            return false;
        }

        private IEnumerator<float> DoorChek()
        {
            for (int i = 0; i > 15; i++)
            {
                yield return Timing.WaitForSeconds(1);
                if (_Door?.Open == null) 
                { 
                    enabled = true; 
                    yield break; 
                }
                if (_Door?.Open == true || (_Door?.Open == false && Vector3.Distance(_Door.Position, npc.Position) > 3))
                    enabled = true;
                else 
                    enabled = false;
            }
        }
        #endregion
    }
}
