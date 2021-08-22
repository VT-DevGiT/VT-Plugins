using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class CoppieurDeGameObject : BaseWeaponScript
    {

        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo556x45;

        protected override int ID => 303;

        protected override ItemType ItemType => ItemType.GunCOM18;

        protected override string Name => "CoppieurDeGameObject";

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            Plugin.Instance.GameObjectMemory = ev.Player.LookingAt;
        }
    }
}