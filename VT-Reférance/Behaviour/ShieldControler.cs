using HarmonyLib;
using Hints;
using Mirror;
using Synapse.Api;
using UnityEngine;
using VT_Referance.Event.EventArguments;

namespace VT_Referance.Behaviour
{
    [API]
    public class ShieldControler : NetworkBehaviour
    {
        static bool IsPatch = false;

        private Player player;
        private int _Shield = 0;
        private int _MaxShield = 100;
        private bool _ShieldLock = false;

        private void Patch()
        {
            if (IsPatch) return;
            IsPatch = true;
            var instance = new Harmony("VT_Referance.Patch.VT_Patch");
            instance.PatchAll();
        }

        private void Start()
        {
            Patch();
            player = gameObject.GetPlayer();
            VTController.Server.Events.Player.PlayerDamagePostEvent += OnDamage;
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
            get => _Shield; 
            set 
            {
                if (value == _Shield)
                    return;
                if (value > _Shield || !ShieldLock)
                    _Shield = Mathf.Clamp(value, 0, MaxShield);

                string info = $"<align=left><color=#FFFFFF50><voffset=-21.5em><size=100%><b><space=2em> Shield : {_Shield}/{MaxShield} | {_Shield / MaxShield * 100}%</b></voffset></color></align>";
                player.HintDisplay.Show(new TextHint(info, new HintParameter[] { new StringHintParameter("") }, null, 8f));
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
