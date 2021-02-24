using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF, (int)Team.RSC, (int)Team.CDP };

        protected override List<int> FriendsList => new List<int> { (int)Team.SCP };

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)Team.SCP;

        protected override int RoleId => (int)MoreClasseID.SCP008;

        protected override string RoleName => PluginClass.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigSCP008;

        protected override void AditionalInit()
        {
            Player.gameObject.AddComponent<Aura>();
            Player.gameObject.GetComponent<Aura>().PlayerEffect = Effect.ArtificialRegen;
            Player.gameObject.GetComponent<Aura>().TargetEffect = Effect.Poisoned;
            Player.gameObject.GetComponent<Aura>().MoiHealHp = PluginClass.ConfigSCP008.HealHp;
            Player.gameObject.GetComponent<Aura>().Distance = PluginClass.ConfigSCP008.Distance;
            
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            if (Player.gameObject.GetComponent<Aura>() != null)
                Player.gameObject.GetComponent<Aura>().Destroy();
            Map.Get.AnnounceScpDeath("0 0 8");
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
            {
                CallPower(PowerType.Zombifaction);
            }
        }

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.Zombifaction)
            {
                Player corpseowner = GetPlayercoprs(Player, 2.5f);
                if (corpseowner != null)
                {
                    corpseowner.RoleID = (int)RoleType.Scp0492;
                }
                return true;
            }
            return false;
        }
        public static List<int> scpRole = new List<int>()
        {
            (int)MoreClasseID.SCP008,(int)MoreClasseID.SCP966,(int)MoreClasseID.SCP682,
            (int)MoreClasseID.SCP1048,(int)MoreClasseID.SCP953,(int)RoleType.Scp049
            ,(int)RoleType.Scp0492,(int)RoleType.Scp079,(int)RoleType.Scp096,(int)RoleType.Scp106
            ,(int)RoleType.Scp173,(int)RoleType.Scp93953,(int)RoleType.Scp93989, 56, 79
        };

        public static  bool? IsScpRole(Player owner )
        {
            if (owner != null)
            {
                if (PluginClass.Plugin.PlayerRole.ContainsKey(owner))
                {
                    int roleId = PluginClass.Plugin.PlayerRole[owner];
                    return scpRole.Contains(roleId);
                }
            }
            return null;
        }

        public static Player GetPlayercoprs(Player player, float rayon)
        {
            List<Collider> colliders = Physics.OverlapSphere(player.Position, 3f)
                .Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
            colliders.Sort((Collider x, Collider y) =>
            {
                return Vector3.Distance(x.gameObject.transform.position, player.Position)
                .CompareTo(Vector3.Distance(y.gameObject.transform.position, player.Position));
            });

            if (colliders.Count == 0 )
                return null;
            Ragdoll doll = colliders[0].gameObject.GetComponentInParent<Ragdoll>();
            if (doll == null)
                return null;
            Player owner = Server.Get.Players.FirstOrDefault( p=> p.PlayerId == doll.owner.PlayerId);
            bool? isScp = IsScpRole(owner);

            if (owner != null && owner.RoleID != (int)RoleType.Spectator && isScp == false)
            {
                UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                return owner;
            }
            return null;
        }

    }
}
