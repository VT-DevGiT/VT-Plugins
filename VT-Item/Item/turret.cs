using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Referance.Behaviour;
using VT_Referance.ItemScript;

namespace VT_Item.Item
{
    [ItemInformation(ID = 202, ItemType = ItemType.GunRevolver, Name = "Turret")]

    public class Turret : BaseWeaponScript
    {
        public override string ScrenName => "Turret";

        public override ushort Ammos => 0;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int DamageAmmont => throw new NotImplementedException();


        protected override void Drop(PlayerDropItemEventArgs ev)
        {
            base.Drop(ev);

        }

        protected override void Shoot(PlayerShootEventArgs ev)
        {
            ev.Allow = false;
        }

        private class TurretScript : BaseRepeatingBehaviour
        {
            protected override void Start()
            {
                base.Start();
            }

            protected override void BehaviourAction()
            {
                
            }
        }
    }
}
