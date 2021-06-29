using Synapse.Api;
using Synapse.Api.Enum;
using VT_Referance.Behaviour;

namespace VTCustomClass.Pouvoir
{
    class Disabled : BaseRepeatingBehaviour
    {
        Player player;
        protected override void Start()
        {
            base.Start();
            player = gameObject.GetPlayer();
        }
        protected override void BehaviourAction()
        {
            player.GiveEffect(Effect.Disabled, 1, 1.1f);
        }
    }
}
