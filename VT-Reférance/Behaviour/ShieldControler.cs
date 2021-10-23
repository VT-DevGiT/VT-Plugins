using Hints;
using Mirror;
using Synapse;
using Synapse.Api;
using UnityEngine;
using VT_Referance.Event.EventArguments;
using VT_Referance.Method;

namespace VT_Referance.Behaviour
{
    [API]
    public class ShieldControler : NetworkBehaviour
    {
        private Player player;
        private int _Shield = 0;
        private int _MaxShield = 100;
        private bool _ShieldLock = false;

        private void Start()
        {
            VTController.Server.Events.Player.PlayerDamagePostEvent += OnDamage;
            VTController.Server.Events.Player.PlayerSetClassEvent += OnSetClass;
            player = gameObject.GetPlayer();
        }

        private void OnEnable()
        {
            VTController.Server.Events.Player.PlayerDamagePostEvent += OnDamage;
            VTController.Server.Events.Player.PlayerSetClassEvent += OnSetClass;
            player = gameObject.GetPlayer();
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Player == player)
            {
                _ShieldLock = false;
                _MaxShield = 100;
                _Shield = 0;
            }
        }

        private void OnDamage(PlayerDamagePostEventArgs ev)
        {
            var damageType = ev.HitInfo.Tool;
            if (Shield != 0 && ev.Victim == player && ev.Allow 
                && (damageType.Weapon != ItemType.None || damageType.Scp != RoleType.None || damageType == DamageTypes.Grenade))
            {
                int damge = 2*((int)ev.DamageAmount / 3);
                damge = Mathf.Clamp(damge, 0, Shield);
                Shield -= damge;
                ev.DamageAmount -= damge;
            }
        }


        /// <summary>
        /// The amount of shield for this ShieldControler;
        /// cannot exceed the Maxshield and cannot be less than 0;
        /// cannot be increased if the ShieldLock is set to true;
        /// </summary>
        public int Shield
        {
            get { return _Shield; }
            set 
            {
                if (value == _Shield)
                    return;
                if (value > _Shield || !ShieldLock)
                    _Shield = Mathf.Clamp(value, 0, MaxShield);

                string info = $"<align=right><color=#FFFFFF50><voffset=-21.65em><size=100%><b>Shield : {_Shield}/{MaxShield} | {(double)_Shield / MaxShield * 100}%</b></voffset></color></align>";
                player.HintDisplay.Show(new TextHint(info, new HintParameter[] { new StringHintParameter("") }, null, 3f));
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
    }
}
