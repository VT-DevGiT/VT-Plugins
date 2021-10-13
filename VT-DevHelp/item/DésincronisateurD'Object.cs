using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.Script.ItemScript;

namespace VTDevHelp
{
    [ItemInformation(ID = 300, ItemType = ItemType.GunRevolver, Name = "DésincronisateurD'Object")]
    internal class DésincronisateurD_Object : BaseWeaponScript
    {
        public override ushort Ammos => 100;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int DamageAmmont => throw new System.NotImplementedException();

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            NetworkServer.UnSpawn(ev.Player.LookingAt);
            ev.Allow = false;
        }

    }
}