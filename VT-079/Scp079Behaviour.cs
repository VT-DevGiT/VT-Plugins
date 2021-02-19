using Mirror;
using System.Linq;

namespace VT079
{
    public class Scp079Behaviour : NetworkBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
            if (!Inited)
            {
                var scripte = Scp079PlayerScript.instances.FirstOrDefault(p => p.GetPlayer() == this.GetPlayer());
                if (scripte != null)
                {
                    Plugin.Instance.ChangeCoutUnScripte(scripte);
                    Inited = true;
                    this.enabled = false;
                    DestroyImmediate(this, true);
                }
            }
        }

        private bool Inited = false;
    }
}
