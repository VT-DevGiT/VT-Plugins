using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    [ItemInformation(ID = 302, ItemType = ItemType.GunRevolver, Name = "TerminatorDeGameObject'Object")]

    internal class TerminatorDeGameObject : BaseWeaponScript
    {
        public override ushort Ammos => 100;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int DamageAmmont => throw new System.NotImplementedException();

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            NetworkServer.UnSpawn(ev.Player.LookingAt);
            Object.Destroy(ev.Player.LookingAt);
            ev.Allow = false;
        }
    }
}