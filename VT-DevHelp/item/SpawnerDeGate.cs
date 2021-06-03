using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class SpawnerDeGate : BaseWeaponScript
    {
        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo5;

        protected override int ID => 301;

        protected override ItemType ItemType => ItemType.GunUSP;

        protected override string Name => "SpawnerDeGate";

        protected override void Shoot(PlayerShootEventArgs args)
        {
            if (args.TargetPosition != Vector3.zero)
            {
                var Gate = GameObject.Find("GATE_A");
                GameObject.Instantiate(Gate, args.TargetPosition, new Quaternion (args.Player.Rotation.x, args.Player.Rotation.y, 0, 0));
            }
        }

    }
}