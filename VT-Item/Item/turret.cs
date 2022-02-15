using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Items;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 202, BasedItemType = ItemType.GunRevolver, Name = "Turret")]

    public class Turret : AbstractWeapon
    {
        public override string ScreenName => "Turret";

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

        private class TurretScript : RepeatingBehaviour
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
