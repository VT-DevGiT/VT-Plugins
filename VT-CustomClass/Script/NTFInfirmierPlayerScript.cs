using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
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
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

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
                    Player owner = Map.Get.GetPlayercoprs(Player, 2.5f);
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
                    message = Reponse.Cooldown(lastPower, Plugin.Instance.Config.NtfInfirmierCooldown);
            }
            else message = "You ave only one power";
            return false;
        }
    }
}
