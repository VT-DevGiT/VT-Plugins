using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly DamageType[] IgnoredDomageType =
        {
            DamageType.MicroHID,
            DamageType.MicroHid,
            DamageType.Decontamination,
            DamageType.PocketDecay,
            DamageType.Warhead,
            DamageType.Tesla,
            DamageType.Disruptor,
            DamageType.Scp096,
            DamageType.UsedAs106Bait
        };

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
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
        }


        private static void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.Allow && ev.State == ItemInteractState.Finalizing && ev.CurrentItem.ID == (int)ItemID.SCP500)
            {
                if (ev.Player.TryGetComponent<Scp008Infected>(out var infected))
                    infected.enabled = false;
            }
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {            
            if (ev.Victim?.RoleID == (int)RoleID.SCP008 && Server.Get.Players.Where(p => p.RoleID == (int)RoleID.SCP008).Count() == 1)
            {
                Map.Get.GlitchedCassie("ALL SCP 0 0 8 SUCCESSFULLY TERMINATED . NOSCPSLEFT");
            }
            else if (ev.Victim.TryGetComponent<Scp008Infected>(out var infected) && infected.enabled)
            {
                infected.enabled = false;

                if (IgnoredDomageType.Contains(ev.DamageType))
                    return;

                var pos = ev.Victim.Position;
                ev.Allow = false;
                ev.Victim.RoleID = (int)RoleID.SCP008;
                ev.Victim.Position = pos;
                infected.Scp008.Health += 100;
                
            }
        }

        private static void OnAttack(PlayerDamageEventArgs ev)
        {
            if (ev.Allow && ev.Killer?.RoleID == (int)RoleID.SCP008)
            {
                if (!ev.Victim.IsUTR() || ev.Victim.RoleID != (int)RoleID.NtfVirologue)
                {
                    ev.Damage = 20;
                    var infected = ev.Victim.GetOrAddComponent<Scp008Infected>();
                    infected.enabled = true;
                    infected.Scp008 = ev.Killer; 
                    ev.Victim.GiveEffect(Effect.Bleeding, 2, 4);
                    ev.Victim.GiveEffect(Effect.Blinded, 2, 4);
                }
                else
                    ev.Damage = 60;

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
                if (oldteam != (int)TeamID.SCP && oldteam != (int)TeamID.BerserkSCP && 
                    ((oldrole >= 0 && oldrole <= Synapse.Api.Roles.RoleManager.HighestRole) // check if it was vanila
                     || !(Synapse.Api.Roles.RoleManager.Get.GetCustomRole(oldrole) is IUtrRole || oldrole == (int)RoleID.NtfVirologue))) // check if it was not an UTR if it was not vanila
                {
                    owner.RoleID = (int)RoleID.SCP008;
                    Player.Health += 100;
                    owner.Position = Player.Position;
                    message = "You have a new friend !";
                }
                else
                {
                    message = "Impossible sory";
                    return false;
                }
            }
            message = "You have only one power";
            return false;
        }
    }
}
