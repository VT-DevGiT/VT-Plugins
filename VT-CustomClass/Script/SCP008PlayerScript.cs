using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
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

        #region Properties & Variable
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

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

        Vector3? spawnPos;
        #endregion

        #region Constructor & Destructor
        public SCP008Script()
        {
            this.spawnPos = null;
        }

        public SCP008Script(Vector3 spawnPos)
        {
            this.spawnPos = spawnPos;
        }
        #endregion

        #region Methods
        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            if (spawnPos.HasValue)
            {
                ev.Position = spawnPos.Value;
                Timing.CallDelayed(0.5f, () => ev.Player.Health = Plugin.Instance.Config.Scp008Config.Health ?? 400);
                //TODO : Fix le fait qu'il n'a pas le bon Hp au respawn
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnAttack;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
            
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

                if (CanZombifid(owner))
                {
                    owner.CustomRole = new SCP008Script(Player.Position);
                    Player.Health += 100;
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

        public static bool CanZombifid(Player owner)
        {
            var oldteam = VtController.Get.Role.OldTeam(owner);
            var oldrole = VtController.Get.Role.OldRoleID(owner);
            if (oldteam == (int)TeamID.SCP || oldteam == (int)TeamID.BerserkSCP)        //false if is SCP
                return false;
            if (oldrole >= 0 && oldrole <= Synapse.Api.Roles.RoleManager.HighestRole)   //true is not a custom Role
                return true;                                                            //false is UTR or a Virologue
            return !(Synapse.Api.Roles.RoleManager.Get.GetCustomRole(oldrole) is IUtrRole) && oldrole != (int)RoleID.NtfVirologue;
        }
        #endregion

        #region Events

        private static void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (ev.Allow && ev.State == ItemInteractState.Finalizing && ev.CurrentItem.ID == (int)ItemID.SCP500)
            {
                if (ev.Player.TryGetComponent<Scp008Infected>(out var infected) && infected.enabled)
                {
                    infected.enabled = false;
                    ev.Player.GiveEffect(Effect.Poisoned, 0, 1);
                    Synapse.Api.Logger.Get.Info("Enabled = false");
                }
            }
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {            
            if (ev.Victim?.RoleID == (int)RoleID.SCP008 && Server.Get.Players.Where(p => p?.RoleID == (int)RoleID.SCP008).Count() == 1)
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
                ev.Victim.CustomRole = new SCP008Script(pos);
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
                    ev.Victim.GiveEffect(Effect.Bleeding, 1, 10);
                    ev.Victim.GiveEffect(Effect.Blinded, 2, 4);
                }
                else
                    ev.Damage = 60;

            }
        }
        #endregion
    }
}
