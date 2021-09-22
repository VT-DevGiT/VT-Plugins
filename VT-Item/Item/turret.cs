using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Behaviour;
using VT_Referance.ItemScript;
using VT_Referance.Variable;

namespace VT_Item.Item
{
    public class Turret : BaseWeaponScript
    {
        public override float Weight => 25;

        public override ushort Ammos => 0;

        public override AmmoType AmmoType => AmmoType.Ammo556x45;

        public override int ID => (int)ItemID.MiniGun;

        public override ItemType ItemType => ItemType.GunLogicer;

        public override string Name => "Turret";

        public override int DamageAmmont => throw new NotImplementedException();

        //public override DamageTypes.DamageType DamageType => DamageTypes.Wall;

        //public override float ArmorPenetration => 0;

        // public override bool UseHitboxMultipliers => false;

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
