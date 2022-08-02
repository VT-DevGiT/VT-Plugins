using Achievements;
using CustomPlayerEffects;
using InventorySystem.Items.Usables;
using Mirror;
using PlayerStatsSystem;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
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
    public class NTFInfirmierScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfInfirmier;

        protected override string RoleName => Plugin.Instance.Config.NtfInfirmierName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.NtfInfirmierConfig;

        private DateTime lastPower = DateTime.Now.AddSeconds(-Plugin.Instance.Config.NtfInfirmierCooldown);
        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseItem;
        }

        static int[] AllowedItems = { (int)ItemID.Medkit, (int)ItemID.XlMedkit, (int)ItemID.Adrenaline, (int)ItemID.Painkillers, (int)ItemID.SCP500 };

        private static void OnUseItem(PlayerItemInteractEventArgs ev)
        {
            if (!ev.Allow || ev.CurrentItem == null || ev.State != ItemInteractState.Finalizing)
                return;

            var cible = ev.Player.LookingAt?.GetPlayer();
            if (cible == null)
                return;

            switch (ev.CurrentItem.ID)
            {
                case (int)ItemID.Medkit:
                    {
                        cible.PlayerStats.GetModule<HealthStat>().ServerHeal(Medkit.HpToHeal);
                        cible.PlayerEffectsController.UseMedicalItem(ev.CurrentItem.ItemType);
                        ev.CurrentItem.Destroy();
                    }
                    break;
                case (int)ItemID.Adrenaline:
                    {
                        cible.Hub.fpc.ModifyStamina(100f);
                        cible.PlayerStats.GetModule<AhpStat>().ServerAddProcess(Adrenaline.AhpAddition);
                        cible.PlayerEffectsController.EnableEffect<Invigorated>(Adrenaline.InvigoratedTargetDuration, true);
                        cible.PlayerEffectsController.UseMedicalItem(ev.CurrentItem.ItemType);
                        ev.CurrentItem.Destroy();
                    }
                    break;
                case (int)ItemID.Painkillers:
                    {
                        AnimationCurve regenCurve = ((Painkillers)ev.CurrentItem.ItemBase)._healProgress;
                        UsableItemsController.GetHandler(cible.Hub).ActiveRegenerations.Add(new RegenerationProcess(regenCurve, 0.06666667f, Painkillers.TotalHpToRegenerate));
                        cible.PlayerEffectsController.UseMedicalItem(ev.CurrentItem.ItemType);
                        ev.CurrentItem.Destroy();
                    }
                    break;
                case (int)ItemID.SCP500:
                    {
                        HealthStat module = cible.PlayerStats.GetModule<HealthStat>();
                        if (module.CurValue < Scp500.AchievementMaxHp)
                            AchievementHandlerBase.ServerAchieve((NetworkConnection)cible.NetworkIdentity.connectionToClient, AchievementName.CrisisAverted);
                        module.ServerHeal(Scp500.TotalHpToRegenerate);
                        AnimationCurve regenCurve = ((Scp500)ev.CurrentItem.ItemBase)._healProgress;
                        UsableItemsController.GetHandler(cible.Hub).ActiveRegenerations.Add(new RegenerationProcess(regenCurve, Scp500.TotalRegenerationTime / 100, Scp500.TotalRegenerationTime));
                        cible.PlayerEffectsController.UseMedicalItem(ev.CurrentItem.ItemType);
                        ev.CurrentItem.Destroy();
                    }
                    break;
                case (int)ItemID.XlMedkit:
                    {
                        cible.PlayerStats.GetModule<HealthStat>().ServerHeal(Medkit.HpToHeal);
                        cible.PlayerEffectsController.UseMedicalItem(ev.CurrentItem.ItemType); 
                        ev.Player.Inventory.AddItem(ItemType.Medkit);
                        ev.CurrentItem.Destroy();
                    }
                    break;
            }

        }

        private static void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.CustomRole is NTFInfirmierScript)
                ev.HollowBullet();
        }

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.Defibrillation)
            {
                if ((DateTime.Now - lastPower).TotalSeconds > Plugin.Instance.Config.NtfInfirmierCooldown)
                {
                    Player owner = Player.GetDeadPlayerInRangeOfPlayer(2.5f);
                    if (owner == null)
                    {
                        message = "You cant..";
                        return false;
                    }

                    if (VtController.Get.Role.OldTeam(owner) != (int)TeamID.SCP)
                    {
                        owner.RoleID = VtController.Get.Role.OldPlayerRole[owner];
                        owner.Position = owner.DeathPosition;
                        owner.Inventory.Clear();
                        lastPower = DateTime.Now;
                        message = "You successfully revive (s)he";
                    }
                    else
                        message = "You try to revive a scp";
                }
                else
                    message = Cooldown.Send(Player, lastPower, Plugin.Instance.Config.NtfInfirmierCooldown);
            }
            else message = "You ave only one power";
            return false;
        }
    }
}
