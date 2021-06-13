using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class TerminatorDeGameObject : BaseWeaponScript
    {

        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo5;

        protected override int ID => 300;

        protected override ItemType ItemType => ItemType.GunUSP;

        protected override string Name => "TerminatorDeGameObject";

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            NetworkServer.UnSpawn(ev.Player.LookingAt);
            GameObject.Destroy(ev.Player.LookingAt);

        }
    }
}