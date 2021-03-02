using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class RoboticTacticalUnityScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.UTR;

        protected override string RoleName => PluginClass.ConfigRoboticTaticalUnity.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigRoboticTaticalUnity;

        protected override void AditionalInit()
        {
            Player.GiveEffect(Effect.Visuals939);
            Player.Hub.playerStats.artificialHpDecay = 0;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnFemur;
        }

        private void OnFemur(PlayerEnterFemurEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
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
                (int)MoreClasseID.SCP008};
            if (ev.Victim == Player && SCPnonHumain.Contains(ev.Killer.RoleID))
                ev.DamageAmount = 25;
            if (ev.Killer == Player && ev.Victim.RoleID == (int)RoleType.Scp096)
                ev.Victim.Scp096Controller.AddTarget(Player);
        }

        private void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.CurrentItem.ItemCategory == ItemCategory.Medical)
                    ev.Allow = false;
                if (ev.CurrentItem.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent -= OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent -= OnFemur;
        }
    }
}
