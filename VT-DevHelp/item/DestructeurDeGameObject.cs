using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using UnityEngine;
using VT_Referance.ItemScript;

namespace VTDevHelp
{
    internal class DestructeurDeGameObject : BaseWeaponScript
    {

        protected override uint Ammo => 100;

        protected override AmmoType AmmoType => AmmoType.Ammo5;

        protected override int ID => 300;

        protected override ItemType ItemType => ItemType.GunUSP;

        protected override string Name => "DestructeurDeGameObject";

        protected override void Shoot(PlayerShootEventArgs args)
        {
            if (args.TargetPosition != Vector3.zero && Physics.Linecast(args.Player.Position, args.TargetPosition, out RaycastHit raycastHit, 1049088))
            {
                var gameOfject = raycastHit.transform.GetComponentInParent<GameObject>();
                if (gameOfject != null)
                    GameObject.Destroy(gameOfject);
            }
        }
    }
}