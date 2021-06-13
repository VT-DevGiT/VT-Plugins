using Mirror;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class ColleurDeGameObject : BaseWeaponScript
    {

        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo5;

        protected override int ID => 305;

        protected override ItemType ItemType => ItemType.GunUSP;

        protected override string Name => "ColleurDeGameObject";

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            GameObject obj = GameObject.Instantiate(Plugin.Instance.GameObjectMemory,
                ev.Player.Position, new Quaternion(ev.Player.Rotation.x, ev.Player.Rotation.y, 0, 0));
            NetworkServer.Spawn(obj);
        }
    }
}