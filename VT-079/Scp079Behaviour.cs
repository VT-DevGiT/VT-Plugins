using Mirror;
using System.Linq;
using VT_Api.Core.Behaviour;

namespace VT079
{
    public class Scp079Behaviour : RepeatingBehaviour
    {
        protected override void BehaviourAction()
        {
            var scripte = Scp079PlayerScript.instances.FirstOrDefault(p => p.GetPlayer() == this.GetPlayer());
            if (scripte != null)
            {
                Plugin.Instance.ChangeCost(scripte);
                Kill();
            }
        }
    }
}
