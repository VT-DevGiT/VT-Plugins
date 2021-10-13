using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;
using VT_Referance.Script.ItemScript;

namespace VTDevHelp
{
    [ItemInformation(ID = 301, ItemType = ItemType.GunRevolver, Name = "DestructeurDeGameObject")]

    internal class DestructeurDeGameObject : BaseWeaponScript
    {
        public override ushort Ammos => 100;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int DamageAmmont => throw new System.NotImplementedException();

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            Object.Destroy(ev.Player.LookingAt);
            ev.Allow = false;
        }
    }
}