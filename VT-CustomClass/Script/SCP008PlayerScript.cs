using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class SCP008Script : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.SCPenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.SCPally.ToList();
        
        protected override List<int> FfFriendsList => FriendsList;

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP008;

        protected override string RoleName => Plugin.Instance.Config.Scp008Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp008Config;
        Aura aura;
        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            aura = ActiveComponent<Aura>();
            {
                aura.targetEffect = Effect.Poisoned;
                aura.effectIntencty = 6;
                aura.effectTime = 5;
                aura.playerAddHp = Plugin.Instance.Config.Scp008AuraHeal;
                aura.targetAddHp = -Plugin.Instance.Config.Scp008AuraDomage;
                aura.distance = Plugin.Instance.Config.Scp008AuraDistance;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            KillComponent<Aura>();
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnAttack;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {
            
            if (ev.Victim?.RoleID == 122 && Server.Get.Players.Where(p => p.RoleID == (int)RoleID.SCP008).Count() == 1)
                Map.Get.GlitchedCassie("ALL SCP 0 0 8 SUCCESSFULLY TERMINATED . NOSCPSLEFT");
        }

        private static void OnAttack(PlayerDamageEventArgs ev)
        {
            if (ev.Allow && ev.Victim?.CustomRole is SCP008Script && ev.DamageType == DamageType.Zombie)
            {
                if (!ev.Victim.IsUTR())
                    ev.Victim.GiveEffect(Effect.Bleeding, 2, 4);
                ev.Damage = 50;
            }
        }

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.Zombifaction)
            {
                Player owner = Player.GetDeadPlayerInRangeOfPlayer(4);
                if (owner == null)
                {
                    message = "You cant...";
                    return false;
                }

                var oldteam = VtController.Get.Role.OldTeam(owner);
                var oldrole = VtController.Get.Role.OldRoleID(owner);
                if (oldteam != (int)TeamID.SCP && oldteam != (int)TeamID.BerserkSCP && (!Synapse.Api.Roles.RoleManager.Get.IsIDRegistered(oldrole) || !(Synapse.Api.Roles.RoleManager.Get.GetCustomRole(oldrole) is IUtrRole)))
                {
                    owner.RoleID = (int)RoleID.SCP008;
                    Player.Health += 100;
                    owner.Position = Player.Position;
                    message = "You ave new freend !";
                }
                else
                {
                    message = "Impossible sory";
                    return false;
                }
            }
            message = "You ave only one power";
            return false;
        }
    }
}
