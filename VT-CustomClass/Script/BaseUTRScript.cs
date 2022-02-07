using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using VT_Api.Core.Roles;

namespace VTCustomClass.PlayerScript
{
    public abstract class BaseUTRScript : AbstractRole, IUtrRole
    {
        #region Properties & Variable
        protected virtual bool heavyUTR => true;
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
        protected bool Protected096 { get; set; } = true;

        private float oldStaminaUse;

        #endregion

        #region Methods
        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            oldStaminaUse = Player.StaminaUsage; 
            Player.StaminaUsage = 0;
            Player.GiveEffect(Effect.Visuals939); // maby work ?
            Player.FallDamage.groundMaxDistance = 1000; 
            if (heavyUTR) Player.GiveEffect(Effect.Disabled);

            // Specific event for this player and not for this role.
            Server.Get.Events.Player.PlayerSetClassEvent += OnScp173Spawn;

            foreach (var player in RoleType.Scp173.GetPlayers())
            {
                if (!player.Scp173Controller?.IgnoredPlayers?.Contains(Player) ?? false)
                    player.Scp173Controller.IgnoredPlayers.Add(player);
            }
        }
        public override void DeSpawn()
        {
            Server.Get.Events.Player.PlayerSetClassEvent -= OnScp173Spawn;

            foreach (var player in Server.Get.Players)
            {
                if (player.Scp173Controller?.IgnoredPlayers?.Contains(Player) ?? false)
                    player.Scp173Controller.IgnoredPlayers.Remove(player);
            }

            Player.StaminaUsage = oldStaminaUse;

            base.DeSpawn();
        }
        #endregion

        #region Events
        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnFemur;
        }

        private static void OnFemur(PlayerEnterFemurEventArgs ev)
        {
            if (ev.Player?.CustomRole is BaseUTRScript)
                ev.Allow = false;
        }

        private static void OnAddTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player?.CustomRole is BaseUTRScript utr)
                ev.Allow = utr.Protected096;
        }

        private static void OnDoorInteract(DoorInteractEventArgs ev)
        {
            KeycardPermissions perm = ev.Door.DoorPermissions.RequiredPermissions;
            if (ev.Player?.CustomRole is BaseUTRScript && (perm != KeycardPermissions.AlphaWarhead && perm != KeycardPermissions.ScpOverride))
                ev.Allow = true;
        }

        private static void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim?.CustomRole is BaseUTRScript)
            {
                if (ev.DamageType == DamageType.Falldown)
                    ev.Allow = false;
                if (ev.Killer != null)
                {
                    if (Plugin.Instance.Config.UTRListScpNoDamge != null && Plugin.Instance.Config.UTRListScpNoDamge.Contains(ev.Killer.RoleID))
                        ev.Allow = false;
                    else if (Plugin.Instance.Config.UTRListScpDamge != null && Plugin.Instance.Config.UTRListScpDamge.Contains(ev.Killer.RoleID))
                        ev.Damage = Plugin.Instance.Config.UTRScpDamage;
                }
            }
            else if (ev.Killer?.CustomRole is BaseUTRScript utr && ev.Victim?.RoleID == (int)RoleType.Scp096)
                utr.Protected096 = false;
        }

        private static void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player?.CustomRole is BaseUTRScript)
            {
                if (ev.CurrentItem.ItemCategory == ItemCategory.Medical || ev.CurrentItem.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
            }
        }

        private void OnScp173Spawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp173)
                ev.Player.Scp173Controller.IgnoredPlayers.Add(Player);
        }
        #endregion
    }
}
