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
        protected override uint Ammo => 0;

        protected override AmmoType AmmoType => AmmoType.Ammo556x45;

        protected override int ID => (int)ItemID.MiniGun;

        protected override ItemType ItemType => ItemType.GunLogicer;

        protected override string Name => "Turret";

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
