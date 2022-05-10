using Mirror;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;
using VT_Api.Core.Items;

namespace VTDevHelp
{
    [VtItemInformation(ID = 302, BasedItemType = ItemType.GunRevolver, Name = "TerminatorDeGameObject'Object")]

    internal class TerminatorDeGameObject : AbstractWeapon
    {
        public override ushort MaxAmmos => 100;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int DamageAmmont => throw new System.NotImplementedException();

        public override bool Shoot(Vector3 targetPosition)
        {
            NetworkServer.UnSpawn(Holder.LookingAt);
            Object.Destroy(Holder.LookingAt);
            return false;
        }
    }
}