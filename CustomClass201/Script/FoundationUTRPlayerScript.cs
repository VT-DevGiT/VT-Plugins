using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class FoundationUTRScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP };

        protected override List<int> FriendsList => new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)TeamID.MTF;

        protected override int RoleId => (int)RoleID.FoundationUTR;

        protected override string RoleName => PluginClass.ConfigFoundationUTR.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigFoundationUTR;

        float oldStaminaUse;
        protected override void AditionalInit()
        {
            oldStaminaUse = Player.StaminaUsage;
            Player.StaminaUsage = 0;
            Player.GiveEffect(Effect.Visuals939);
            Player.GiveEffect(Effect.Disabled);
            Player.Hub.playerStats.artificialHpDecay = 0;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnFemur;
            Server.Get.Events.Player.PlayerReloadEvent += OnReload;
        }

        private void OnFemur(PlayerEnterFemurEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }

        private void OnReload(PlayerReloadEventArgs ev)
        {
            if (ev.Player == Player)
            {
                uint balleR = (uint)ev.Item.Durabillity*2;
                ev.Player.Ammo5 = balleR;
                ev.Player.Ammo5 = balleR;
                ev.Player.Ammo5 = balleR;
            }
        }

        private void OnAddTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            List<KeycardPermissions> UTRkey = new List<KeycardPermissions>() { KeycardPermissions.ArmoryLevelOne, KeycardPermissions.ArmoryLevelTwo, 
                KeycardPermissions.ArmoryLevelThree, KeycardPermissions.Checkpoints, KeycardPermissions.ContainmentLevelOne, 
                KeycardPermissions.ContainmentLevelTwo, KeycardPermissions.ExitGates, KeycardPermissions.ScpOverride };

            if (ev.Player == Player && UTRkey.Contains(ev.Door.DoorPermissions.RequiredPermissions))
                ev.Allow = true;
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            List<int> SCPnonHumain = new List<int>() { (int)RoleType.Scp049,
                (int)RoleType.Scp0492, (int)RoleType.Scp096, (int)RoleType.Scp106,
                (int)RoleType.Scp173, (int)RoleType.Scp93953, (int)RoleType.Scp93989,
                (int)RoleID.SCP008};
            if (ev.Victim == Player && SCPnonHumain.Contains(ev.Killer.RoleID))
                ev.DamageAmount = 25;
            if (ev.Killer == Player && ev.Victim.RoleID == (int)RoleType.Scp096)
                ev.Victim.Scp096Controller.AddTarget(Player);
        }

        private void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.CurrentItem.ItemCategory == ItemCategory.Medical || ev.CurrentItem.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
            }
        }

        public override void DeSpawn()
        {
            Player.StaminaUsage = oldStaminaUse;
            base.DeSpawn();
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent -= OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent -= OnFemur;
            Server.Get.Events.Player.PlayerReloadEvent -= OnReload;
        }
    }
}
