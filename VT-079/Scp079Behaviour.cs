using Mirror;
using System.Linq;
using VT_Referance.Behaviour;

namespace VT079
{
    public class Scp079Behaviour : BaseRepeatingBehaviour
    {
        protected override void BehaviourAction()
        {
            var scripte = Scp079PlayerScript.instances.FirstOrDefault(p => p.GetPlayer() == this.GetPlayer());
            if (scripte != null)
            {
                Plugin.Instance.ChangeCoutUnScripte(scripte);
                Kill();
            }
        }
    }
}
