using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Behaviour;

namespace VTCustomClass.Pouvoir
{
    class Invisible : BaseRepeatingBehaviour
    {
        private Player player;

        protected override void BehaviourAction()
        {
            player.GiveEffect(Synapse.Api.Enum.Effect.Scp268, 2);
        }

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            this.RefreshTime = 1;
            base.Start();
        }
    }
}
