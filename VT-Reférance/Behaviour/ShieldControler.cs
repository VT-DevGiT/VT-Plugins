using Hints;
using Synapse.Api;
using UnityEngine;

namespace VT_Referance.Behaviour
{
    public class ShieldControler : BaseRepeatingBehaviour
    {
        private Player player;
        private int _Shield;

        public int Shield
        {
            get { return _Shield; }
            set { _Shield = Mathf.Clamp(value,0, ShieldMax); }
        }

        public int ShieldMax { get; set; }

        public float ArtificialHealth { get; set; }

        private string Info = $"<size=100%><align=\"left\"> <b>Shield : {0} // {1}</b>";
        
        
        
        
        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }

        protected override void BehaviourAction()
        {
            string info = string.Format(Info, Shield, ShieldMax);
            
            Hint hint = new TextHint(
                    info,
                    new HintParameter[] { new StringHintParameter(string.Empty) },
                    null,
                    0.75f
                    );
            if (Shield != 0)
                player.gameObject.GetComponent<ReferenceHub>().hints.Show(hint);
        }
    }
}
