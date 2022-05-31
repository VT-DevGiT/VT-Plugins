using Interactables.Interobjects.DoorUtils;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.CustomObjects;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public abstract class BaseUTRScript : AbstractRole, IUtrRole
    {
        #region Properties & Variable
        protected virtual bool HeavyUTR => true;
        protected virtual Color UtrColor => Color.white;
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
        protected bool Protected096 { get; set; } = true;

        private float oldStaminaUse;
    
        protected UtrCorp Corp { get; set; }

        #endregion

        #region Methods
        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            oldStaminaUse = Player.StaminaUsage; 
            Player.StaminaUsage = 0;
            Player.GiveEffect(Effect.Visuals939);
            Player.FallDamage.groundMaxDistance = 1000; 
            if (HeavyUTR) Player.GiveEffect(Effect.Disabled);

            Corp = new UtrCorp(115, 116, 117, ev.Player);

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

            Corp.Destroy();

            Player.StaminaUsage = oldStaminaUse;

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

        #region structure
        protected struct UtrCorp
        {
            public SynapseObject mainObject;

            public SynapseObject body;

            public SynapseObject head;

            public SynapseObject bot;

            public UtrCorp(int BodyID, int HeadID, int BotID, Player player, Color color = default)
            {
                if (!(player.CustomRole is BaseUTRScript))
                    throw new ArgumentException("The player is not a UTR, you cant create a UtrCorp !", "player");

                mainObject = new SynapseObject(new SynapseSchematic() { ID = 118, Name = "UTR" });
                body = SchematicHandler.Get.SpawnSchematic(BodyID, player.Position);
                head = SchematicHandler.Get.SpawnSchematic(HeadID, player.Position);
                bot = SchematicHandler.Get.SpawnSchematic(BotID, player.Position);

                SetHeight(body);
                SetHeight(head);
                SetHeight(bot);

                SetChildrens(mainObject, body);
                SetChildrens(mainObject, head);
                SetChildrens(mainObject, bot);

                var teamColor = (player.CustomRole as BaseUTRScript).UtrColor;

                foreach (var child in mainObject.Childrens)
                {
                    if (child.CustomAttributes.FirstOrDefault(s => s.ToLower().Contains("teamcolor")) == null) ;
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
                    var pos = synapseObject.CustomAttributes.FirstOrDefault(s => s.Contains("pos"));
                    var data = pos.Split(':');

                    if (data.Length == 2 && float.TryParse(data[1], out var height))
                    {
                        var newPos = synapseObject.Position;
                        newPos.y += height;
                        synapseObject.Position = newPos;
                    }
                }

                void SetChildrens(SynapseObject parent, SynapseObject children)
                {
                    parent.GameObject.transform.parent = children.GameObject.transform;
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
                => mainObject.Position = postion;

            public void SetRotation(Vector3 rotation)
            {
                var bodyRotation = new Vector3(rotation.x, 0);
                var headRotation = new Vector3(0, rotation.y);
                mainObject.Rotation = Quaternion.Euler(bodyRotation);
                head.Rotation = Quaternion.Euler(headRotation);
            }

            public void Destroy()
                => mainObject.Destroy();
        }
        #endregion
    }
}
