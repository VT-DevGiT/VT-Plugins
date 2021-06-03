using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using Synapse.Api;

namespace VTCustomClass.PlayerScript
{
    public class ConciergeScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
            
        protected override List<int> EnemysList => new List<int> { (int)TeamID.SCP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.MTFally;

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.Concierge;

        protected override string RoleName => PluginClass.ConfigConcierge.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigConcierge;

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPresse;   
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.Clear)
            {
                List<Collider> collidersDoll = Physics.OverlapSphere(Player.Position, 2.5f)
                 .Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
                if (collidersDoll.Count != 0)
                {
                    Ragdoll doll = collidersDoll[0].gameObject.GetComponentInParent<Ragdoll>();
                    if (doll != null)
                        UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                }
                return true;
            }
            return false;
        }

        private void OnKeyPresse(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                CallPower((int)PowerType.Clear);
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPresse;
        }
    }
}
