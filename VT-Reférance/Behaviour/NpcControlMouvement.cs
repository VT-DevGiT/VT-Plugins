using Interactables;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Mirror;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.NpcScript;
using VT_Referance.Variable.Npc;
using UnityEngine.AI;

namespace VT_Referance.Behaviour
{
    [API]
    public class NpcControlMouvement : BaseRepeatingBehaviour
    { 
        NpcControlMouvement() => this.RefreshTime = 100;

        BaseNpcScript npc;
        Player player;
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
                    NpcMapPointRoute LastPoint = NpcMapPointRoute.GetNearestPoint((Vector3)value);
                    if (LastPoint == FirstPoint) Chemin = new List<NpcMapPointRoute>();
                    else Chemin = NpcDataInit.TestCheminZone.PlusCourtChemin(FirstPoint.Id, LastPoint.Id);
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

            if (TryOpenDoor() == true) Timing.RunCoroutine(DoorChek());
            mouve();
        }

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

        protected bool TryInteract()
        {
            if (player.LookingAt == null)
                return false;
            InteractionCoordinator obj = player.GetComponent<InteractionCoordinator>();
            NetworkReader reader = player.LookingAt.GetComponent<NetworkReader>();
            NetworkIdentity targetInteractable = reader.ReadNetworkIdentity();
            byte colId = NetworkReaderExtensions.ReadByte(reader);
            IInteractable component;
            InteractableCollider res;
            if (targetInteractable == null || obj._hub == null || obj._hub.characterClassManager.CurClass == RoleType.Spectator || !targetInteractable.TryGetComponent(out component) || !InteractableCollider.TryGetCollider(component, colId, out res) || !obj.GetSafeRule(component).ServerCanInteract(obj._hub, res))
                return false;
            component.ServerInteract(obj._hub, colId);
            return true;
        }
        
        protected virtual bool? TryOpenDoor()
        {
            if (_Door == null)
                return null;
            if (Vector3.Distance(_Door.Position, npc.Position) < 3 && !_Door.Open)
            {
                if (_Door.DoorPermissions.RequiredPermissions != 0 && !player.Bypass)
                { 
                    SynapseItem item = player.Inventory.Items.FirstOrDefault(i => _Door.DoorPermissions.CheckPermissions(i.ItemType, player.Hub));
                    if (item == null) return false;
                    npc.HeldItem = item.ItemType;
                }
                return TryInteract();
            }
            return null;
        }

        private IEnumerator<float> DoorChek()
        {
            for (int i = 0; i > 15 ; i++)
            {
                yield return Timing.WaitForSeconds(1);
                if (_Door?.Open == null) { enabled = true; yield break; }
                if (_Door?.Open == true || (_Door?.Open == false && Vector3.Distance(_Door.Position, npc.Position) > 3)) 
                     enabled = true;
                else enabled = false; 
            }
        }
        #endregion
    }
}
