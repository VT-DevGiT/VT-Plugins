using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class DésincronisateurD_Object : BaseWeaponScript
    {
        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo556x45;

        protected override int ID => 302;

        protected override ItemType ItemType => ItemType.GunCOM18;

        protected override string Name => "DésincronisateurD'Object";

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            NetworkServer.UnSpawn(ev.Player.LookingAt);
        }

    }
}