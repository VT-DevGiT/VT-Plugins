using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api;
using Synapse.Api.CustomObjects;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Enum;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Plugin;
using VT_Api.Core.Roles;
using VT_Api.Extension;
using VT_Api.Reflexion;

namespace VTCustomClass.PlayerScript
{
    [AutoRegisterManager.Ignore]
    public abstract class BaseUTRScript : AbstractRole, IUtrRole
    {
        #region Properties & Variable
        const int BodyID = 115;
        const int HeadID = 116;
        const int BotID = 117;
        const int CorpID = 118;

        protected virtual bool HeavyUTR => true;
        protected virtual Color Color => Color.white;
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
        protected bool Protected096 { get; set; } = true;
        protected UtrCorp Corp { get; set; }
        //public Player Target { get; set; }
        
        private float _oldStaminaUse;
        private float _oldGroundMaxDistance;

        #endregion

        #region Methods
        public override void Spawning()
        {
            base.Spawning();
            Player.GiveEffect(Effect.Visuals939, 1);
            Player.Hub.playerEffectsController.ChangeByString("Scp1853".ToLower(), 1, -1);//whait next update
            
            Player.Position += Vector3.up * 1.5f;
            

            if (HeavyUTR)
            {
                Player.GiveEffect(Effect.Disabled);
                Player.Scale *= 1.1f;
            }
            else
            {
                Corp.body.Scale *= 0.75f;
            }
        }

        protected override void AditionalInit(PlayerSetClassEventArgs ev)
        {
            _oldStaminaUse = Player.StaminaUsage;
            _oldGroundMaxDistance = Player.FallDamage.groundMaxDistance;
            
            Player.StaminaUsage = 0;
            Player.FallDamage.groundMaxDistance = 1000;
            Corp = new UtrCorp(BodyID, HeadID, BotID, ev.Player, ev.Position, Color);

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

            if (Player != null)
            {
                Player.Inventory.Clear();
                Player.StaminaUsage = _oldStaminaUse;
                Player.Scale = Vector3.one;
            }
            base.DeSpawn();
        }

        public bool CanBySee(Player player)
            => Plugin.Instance.Config.UTRListScpDamge.ContainsKey(player.RoleID);
        

        /*
        public void RotateToTarget()
        {
            Player.Rotation = new Vector2(12, 3);
            
            Quaternion quaternion = Quaternion.LookRotation((Target.Position - Player.Position).normalized);
            Player.Rotation = new Vector2(quaternion.eulerAngles.x, quaternion.eulerAngles.y);
        }*/


        public void UpdateBody()
        {
            /*if (Target != null)
                RotateToTarget();*/

            Corp.SetPose(Player.Position);
            Corp.SetRotation(Player.Rotation);
        }

        public override bool CallPower(byte power, out string message) // debug
        {

            switch (power)
            {
                case 1:
                    Player.Position = Corp.body.Position;
                    message = "DEBUG !!!";
                    return true;
                    /*                    if (Target == null)
                                        {
                                            Target = Player.LookingAt.GetPlayer();
                                            if (Target == null || !SynapseExtensions.GetHarmPermission(Player, Target))
                                            {
                                                message = Plugin.Instance.Translation.ActiveTranslation.NeedToLookAPlayer;
                                                return false;
                                            }
                                            else
                                            {
                                                message = Plugin.Instance.Translation.ActiveTranslation.TargetLock;
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            if (Target == null || !SynapseExtensions.GetHarmPermission(Player, Target))
                                            {
                                                message = Plugin.Instance.Translation.ActiveTranslation.UnlockTarget;
                                                return true;
                                            }
                                            else
                                            {
                                                message = Plugin.Instance.Translation.ActiveTranslation.NewTargetLock;
                                                return true;
                                            }
                                        }*/
            }

            message = Plugin.Instance.Translation.ActiveTranslation.OnlyOnePower;
            return false;
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
            //Server.Get.Events.Player.PlayerChangeItemEvent += OnChangeItem;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
            VtController.Get.Events.Item.RemoveLimitAmmoEvent += OnAmmoLimit;
        }

        private static void OnSpeak(PlayerSpeakEventArgs ev)
        {
            if (ev.Player.CustomRole is BaseUTRScript)
            {
                ev.DissonanceUserSetup.RadioAsHuman = true;
            }
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Allow && ev.Victim.CustomRole is BaseUTRScript utr)
            {
                ev.Allow = false;
                ev.Victim.RoleID = (int)RoleID.Spectator;
                ev.Victim.Inventory.Clear();
                if (ev.DamageType != DamageType.Disruptor)
                    SchematicHandler.Get.SpawnSchematic(CorpID, ev.Victim.Position);
            }
        }

        private static void OnChangeItem(PlayerChangeItemEventArgs ev)
        {
            if (ev.Allow && ev.Player.CustomRole is BaseUTRScript utr)
                utr.Corp.ChangeItem(ev.NewItem);
        }   

        private static void OnAmmoLimit(RemoveAmmoEventArgs ev)
        {
            if (ev.Player.CustomRole is BaseUTRScript)
                ev.RemovAmmo.Clear();
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
                    if (Plugin.Instance.Config.UTRListScpDamge != null && Plugin.Instance.Config.UTRListScpDamge.ContainsKey(ev.Killer.RoleID))
                    {
                        int damage = Plugin.Instance.Config.UTRListScpDamge[ev.Killer.RoleID];
                        if (damage != -1)
                            ev.Damage = damage;
                    }
                    else
                        ev.Allow = false;
                }
            }
            else if (ev.Killer?.CustomRole is BaseUTRScript utr && ev.Victim?.RoleID == (int)RoleType.Scp096)
                utr.Protected096 = false;
        }

        private static void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player?.CustomRole is BaseUTRScript utr)
            {
                if (ev.CurrentItem?.ItemCategory == ItemCategory.Medical || ev.CurrentItem?.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
                else if (ev.CurrentItem?.ItemCategory == ItemCategory.Firearm && ev.CurrentItem.ID == (int)ItemID.MiniGun)
                    VT_Api.Core.MapAndRoundManger.Get.PlayShoot((ShootSound)ev.CurrentItem.ItemType, utr.Player.Position, 25);
            }
        }

        private void OnScp173Spawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp173)
                ev.Player.Scp173Controller.IgnoredPlayers.Add(Player);
        }

        #endregion

        #region Class
        protected class UtrCorp
        {

            #region Properties & Variable

            public SynapseObject body { get; private set; }
            public SynapseObject head { get; private set; }
            public SynapseObject bot { get; private set; }
            public SynapseItemObject item { get; private set; }

            private float rotationHeadMax;
            private float rotationHeadMin;
            private float rotationHeadMidle;
            #endregion

            #region Constructor & Destructor
            public UtrCorp(int BodyID, int HeadID, int BotID, Player player, Vector3 position, Color color)
            {
                body = SchematicHandler.Get.SpawnSchematic(BodyID, position);
                head = SchematicHandler.Get.SpawnSchematic(HeadID, position);
                bot  = SchematicHandler.Get.SpawnSchematic(BotID , position);
                item = body.ItemChildrens.FirstOrDefault(i => i.CustomAttributes.Any(a => a.ToLower() == "item"));

                rotationHeadMax = 380;
                rotationHeadMin = 000;
                rotationHeadMidle = 0;
                
                var limit = head.CustomAttributes?.FirstOrDefault(s => s.ToLower().Contains("lim"))?.Split(':');
                if (limit?.Length == 3)
                {
                    float.TryParse(limit[1], out rotationHeadMax);
                    float.TryParse(limit[2], out rotationHeadMin);
                    rotationHeadMidle = (rotationHeadMax + rotationHeadMin) / 2;
                }

                if (body == null) throw new Exception("Faild to spawn a body check if you ave a Schematic for the body (ID = " + BodyID + ")");
                if (head == null) throw new Exception("Faild to spawn a head check if you ave a Schematic for the body (ID = " + HeadID + ")");
                if (bot  == null) throw new Exception("Faild to spawn a bot check if you ave a Schematic for the body (ID = " +  BotID  + ")");

                SetHeight(body);
                SetHeight(head);
                SetHeight(bot);

                SetChildrens(body, head);
                SetChildrens(body, bot);

                var teamColor = color;

                foreach (var child in body.Childrens)
                {
                    //Maby a good idea but somany bug ;-; SAD
                    /*                  var hub = child.GameObject.AddComponent<ReferenceHub>();
                                        var hitbox = child.GameObject.AddComponent<HitboxIdentity>();
                                        hitbox.TargetHub = player.Hub;
                                        hub.CopyPropertyAndFeild<ReferenceHub>(player.Hub);*/

                    child.GameObject.layer = (int)LayerID.PlayerModel;
                    
                    if (child.CustomAttributes.FirstOrDefault(s => s.ToLower().Contains("teamcolor")) != null)
                    {
                        if (child is Synapse.Api.CustomObjects.SynapsePrimitiveObject primitve)
                            primitve.ToyBase.NetworkMaterialColor = teamColor;
                        else if (child is Synapse.Api.CustomObjects.SynapseLightObject light)
                            light.LightColor = teamColor;
                    }
                }

                body.DespawnForOnePlayer(player);

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
                }

                void SetChildrens(SynapseObject parent, SynapseObject children)
                {
                    children.GameObject.transform.parent = parent.GameObject.transform;
                    children.SetField<SynapseObject>("Parent", parent);
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
            #endregion

            #region Methods
            public void SetPose(Vector3 postion)
            {
                body.Position = postion;
                Synapse.Api.Logger.Get.Info("update Pose"); // Debug
            }

            public void ChangeItem(SynapseItem newItem)
            {
                if (!newItem.IsDefined())
                {
                    this.item.Scale = Vector3.zero;
                    return;
                }

                var synapseItemObject = new SynapseItemObject(newItem.ItemType, this.item.Position, new Quaternion(0, 0, 0, 0), newItem.Scale);
                synapseItemObject.CustomAttributes = this.item.CustomAttributes;
                
                this.item.Destroy();
                item = synapseItemObject;

                body.ItemChildrens.Add(synapseItemObject);
                body.Childrens.Add(synapseItemObject);
                
                synapseItemObject.GameObject.transform.parent = body.GameObject.transform;
                synapseItemObject.SetField<SynapseObject>("Parent", body);
            }

            public void SetRotation(Vector3 rotation)
            {
                var headx = rotation.x;
                if (rotationHeadMax > headx && headx > rotationHeadMin)
                {
                    if (headx > rotationHeadMidle)
                        headx = rotationHeadMax;
                    else
                        headx = rotationHeadMin;
                }

                body.Rotation = Quaternion.Euler(0, rotation.y, 0);
                head.Rotation = Quaternion.Euler(headx, rotation.y, 0);
            }

            public void Destroy()
            {
                body.Destroy();
            }
            #endregion
        }
        #endregion
    }
}
