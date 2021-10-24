using Interactables.Interobjects.DoorUtils;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.PlayerScript;

namespace VTCustomClass.PlayerScript
{
    public abstract class BaseUTRScript : BasePlayerScript, IUtrRole
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected virtual bool heavyUTR => true;
        protected float StaminaUse;
        protected bool _protected096 = true;

        protected override void AditionalInit()
        {
            StaminaUse = Player.StaminaUsage;
            Player.StaminaUsage = 0;
            Player.GiveEffect(Effect.Visuals939);
            if (heavyUTR) Player.GiveEffect(Effect.Disabled);
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent += OnFemur;
            Server.Get.Events.Player.PlayerSetClassEvent += OnScp173Spawn;
        }

        private void OnScp173Spawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Player != Player && ev.Role == RoleType.Scp173 && !ev.Player.Scp173Controller.IgnoredPlayers.Contains(Player))
                ev.Player.Scp173Controller.IgnoredPlayers.Add(Player);
        }

        private void OnFemur(PlayerEnterFemurEventArgs ev)
        {
            if (ev.Player == Player)
                ev.Allow = false;
        }

        private void OnAddTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player == Player)
                ev.Allow = _protected096;
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            KeycardPermissions perm = ev.Door.DoorPermissions.RequiredPermissions;
            if (ev.Player == Player && (perm != KeycardPermissions.AlphaWarhead && perm != KeycardPermissions.ScpOverride))
                ev.Allow = true;
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == Player)
            {
                var DamageType = ev.HitInfo.Tool;
                if (DamageType != null && DamageType.Scp == RoleType.None && DamageType.Weapon == ItemType.None && DamageType != DamageTypes.Tesla && DamageType != DamageTypes.Grenade && DamageType != DamageTypes.Nuke)
                    ev.Allow = false;
                else if (ev.Killer != Player && Plugin.ConfigUTR.ListScpNoDamge != null && Plugin.ConfigUTR.ListScpNoDamge.Contains(ev.Killer.RoleID))
                    ev.Allow = false;
                else if (ev.Killer != Player && Plugin.ConfigUTR.ListScpDamge != null && Plugin.ConfigUTR.ListScpDamge.Contains(ev.Killer.RoleID))
                    ev.DamageAmount = Plugin.ConfigUTR.damage;
            }
            else if (ev.Killer == Player && ev.Victim?.RoleID == (int)RoleType.Scp096)
                _protected096 = false;
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
            foreach(var player in Server.Get.Players)
            {
                if (player.Scp173Controller.IgnoredPlayers.Contains(Player))
                    player.Scp173Controller.IgnoredPlayers.Remove(player);
            }
            Player.StaminaUsage = StaminaUse;
            base.DeSpawn();
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseIteam;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent -= OnAddTarget;
            Server.Get.Events.Player.PlayerEnterFemurEvent -= OnFemur;
        }
    }
}
