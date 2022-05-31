using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Armor;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.CustomObjects;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public abstract class BaseUTRScript : AbstractRole, IUtrRole
    {
        #region Properties & Variable
        const int BodyID = 115;
        const int HeadID = 116;
        const int BotID = 117;
        const int AllID = 118;

        protected virtual bool HeavyUTR => true;
        protected virtual Color Color => Color.white;
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
        protected bool Protected096 { get; set; } = true;

        private float _oldStaminaUse;
        private float _oldGroundMaxDistance;
        protected UtrCorp Corp { get; set; }

        #endregion

        #region Methods
        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            _oldStaminaUse = Player.StaminaUsage;
            _oldGroundMaxDistance = Player.FallDamage.groundMaxDistance;
            
            Player.StaminaUsage = 0;
            Player.FallDamage.groundMaxDistance = 1000;

            MEC.Timing.CallDelayed(1.75f, () => Player.GiveEffect(Effect.Visuals939));
            if (HeavyUTR) MEC.Timing.CallDelayed(1.75f, () =>
            {
                Player.GiveEffect(Effect.Disabled);
            });

            Corp = new UtrCorp(115, 116, 117, ev.Player, ev.Position, Color);

            ev.Position += Vector3.forward * 2;

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
            Player.FallDamage.groundMaxDistance = _oldGroundMaxDistance;

            Server.Get.Events.Player.PlayerSetClassEvent -= OnScp173Spawn;

            foreach (var player in Server.Get.Players)
            {
                if (player.Scp173Controller?.IgnoredPlayers?.Contains(Player) ?? false)
                    player.Scp173Controller.IgnoredPlayers.Remove(player);
            }

            Corp.Destroy();

            Player.StaminaUsage = _oldStaminaUse;

            base.DeSpawn();
        }

        public void UpdateBody()
        {
            //Corp.SetPose(Player.Position);
            Corp.SetRotation(Player.Rotation);
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
            VtController.Get.Events.Item.RemoveLimitAmmoEvent += OnAmmoLimit;
        }

        private static void OnAmmoLimit(RemoveAmmoEventArgs ev)
        {
            if (ev.Player.CustomRole is BaseUTRScript)
                ev.RemovAmmo.Clear();
            Synapse.Api.Logger.Get.Info("RTR");
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

        public override bool CallPower(byte power, out string message)
        {
            message = "ajaja";
            switch (power)
            {
                case 1:
                    BodyArmorUtils.RemoveEverythingExceedingLimits(Player.Hub.inventory, Player.Hub.inventory.TryGetBodyArmor(out var bodyArmor) ? bodyArmor : (BodyArmor)null, false);
                    break;
            }

            return true;
        }

        #endregion

        #region structure
        protected struct UtrCorp
        {
            public SynapseObject mainObject;

            public SynapseObject body;

            public SynapseObject head;

            public SynapseObject bot;

            public UtrCorp(int BodyID, int HeadID, int BotID, Player player, Vector3 position, Color color)
            {
                mainObject = new SynapseObject(new SynapseSchematic() { ID = 118, Name = "UTR" });
                body = SchematicHandler.Get.SpawnSchematic(BodyID, position);
                head = SchematicHandler.Get.SpawnSchematic(HeadID, position);
                bot = SchematicHandler.Get.SpawnSchematic(BotID, position);

                if (body == null) throw new Exception("Faild to spawn a body check if you ave a Schematic for the body (ID = " + BodyID + ")");
                if (head == null) throw new Exception("Faild to spawn a head check if you ave a Schematic for the body (ID = " + BodyID + ")");
                if (bot  == null) throw new Exception("Faild to spawn a bot check if you ave a Schematic for the body (ID = " + BodyID + ")");

                SetHeight(body);
                SetHeight(head);
                SetHeight(bot);

                SetChildrens(mainObject, body);
                SetChildrens(mainObject, head);
                SetChildrens(mainObject, bot);

                var teamColor = color;

                

                foreach (var child in mainObject.Childrens)
                {
                    if (child.CustomAttributes.FirstOrDefault(s => s.ToLower().Contains("teamcolor")) != null)
                    {
                        if (child is Synapse.Api.CustomObjects.SynapsePrimitiveObject primitve)
                            primitve.ToyBase.NetworkMaterialColor = teamColor;
                        else if (child is Synapse.Api.CustomObjects.SynapseLightObject light)
                            light.LightColor = teamColor;
                    }
                }

                //mainObject.DespawnForOnePlayer(player);


                void SetHeight(SynapseObject synapseObject)
                {
                    var pos = synapseObject.CustomAttributes?.FirstOrDefault(s => s.ToLower(). Contains("pos"));
                    var data = pos?.Split(':');
                    var height = 0f;

                    if (data?.Length == 2)
                        float.TryParse(data[1], out height);
                    
                    var newPos = synapseObject.Position;
                    newPos.y += height + 1;
                    synapseObject.Position = newPos;
                    synapseObject.Rotation = Quaternion.Euler(180, 0, 0);
                }

                void SetChildrens(SynapseObject parent, SynapseObject children)
                {
                    children.GameObject.transform.parent = parent.GameObject.transform;
                    parent.Childrens.Add(children);

                    foreach (var primitiveObject in children.PrimitivesChildrens)
                        parent.PrimitivesChildrens.Add(primitiveObject);

                    foreach (var lightObject in children.LightChildrens)
                        parent.LightChildrens.Add(lightObject);

                    foreach (var targetObject in children.TargetChildrens)
                        parent.TargetChildrens.Add(targetObject);

                    foreach (var itemObject in children.ItemChildrens)
                        parent.ItemChildrens.Add(itemObject);

                    foreach (var workStationObject in children.WorkStationChildrens)
                        parent.WorkStationChildrens.Add(workStationObject);

                    foreach (var doorObject in children.DoorChildrens)
                        parent.DoorChildrens.Add(doorObject);

                    foreach (var children2 in children.Childrens)
                        parent.Childrens.Add(children2);
                }
            }

            public void SetPose(Vector3 postion)
                => mainObject.Position = postion + Vector3.forward * 2;

            public void SetRotation(Vector3 rotation)
            {
                var headx = rotation.x;
                if (338.5f > headx && headx > 24.3f)
                {
                    if (headx > 157.1f)
                        headx = 338.5f;
                    else
                        headx = 24.3f;
                }
             
                mainObject.Rotation = Quaternion.Euler(rotation.y, 0, 0);
                head.Rotation = Quaternion.Euler(headx, 0, 0);

            }

            public void Destroy()
                => mainObject.Destroy();
        }
        #endregion
    }
}
