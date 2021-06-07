using Hints;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using UnityEngine;

namespace VT_Referance.Behaviour
{
    [API]
    public class ShieldControler : BaseRepeatingBehaviour
    {
        private Player player;
        private int _Shield = 0;
        private int _MaxShield = 100;
        private bool _ShieldLock = false;

        public ShieldControler()
        {
            base.RefreshTime = 500;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }


        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == player && _Shield != 0 && ev.Allow == true)
            {
                int damge = (int)ev.DamageAmount;
                damge = 2*(damge / 3);
                damge -= _Shield;
                _Shield -= (int)(2*(ev.DamageAmount / 3));
                ev.DamageAmount = (ev.DamageAmount / 3) + damge;
            }
        }
        /// <summary>
        /// The amount of shield for this ShieldControler
        /// cannot exceed the Maxshield and cannot be less than 0
        /// cannot be increased if the ShieldLock is set to true
        /// </summary>
        public float Shield
        {
            get { return _Shield; }
            set 
            { 
                if (value > _Shield || !ShieldLock)
                    _Shield = (int)Mathf.Clamp(value, 0, MaxShield); 
            }
        }

        /// <summary>
        ///  The max amount of shield for this ShieldControler
        /// </summary>

        public int MaxShield
        {
            get { return _MaxShield; }
            set { _MaxShield = value; }
        }
        /// <summary>
        /// if it is possible to increase the shield
        /// </summary>
        public bool ShieldLock
        {
            get { return _ShieldLock; }
            set { _ShieldLock = value; }
        }
        
        
        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }

        protected override void BehaviourAction()
        {
            if (Shield != 0)
            {
                // les \n c'est pas une bonne idée il faux changée 
                string info = $"<size=100%><b>Shield : {Shield} // {MaxShield}</b>";
                Hint hint = new TextHint(
                        info,
                        new HintParameter[] { new StringHintParameter(string.Empty) },
                        null,
                        1
                        );
                 player.gameObject.GetComponent<ReferenceHub>().hints.Show(hint);
            }
        }
    }
}
