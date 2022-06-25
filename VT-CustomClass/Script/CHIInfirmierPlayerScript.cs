using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class CHIInfirmierScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosRepressor;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosInfirmier;

        protected override string RoleName => Plugin.Instance.Config.ChiInfirmierName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.ChiInfirmierConfig;

        private DateTime lastPower = DateTime.Now.AddSeconds(-Plugin.Instance.Config.ChiInfirmierCooldown);

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
        }
        

        private static void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.CustomRole is CHIInfirmierScript)
                ev.HollowBullet();
        }

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.Defibrillation)
            {
                if ((DateTime.Now - lastPower).TotalSeconds > Plugin.Instance.Config.ChiInfirmierCooldown)
                {
                    Player owner = Player.GetDeadPlayerInRangeOfPlayer(2.5f);
                    if (owner == null)
                    {
                        message = "You cant..";
                        return false;
                    }

                    var oldteam = VtController.Get.Role.OldTeam(owner);
                    var oldrole = VtController.Get.Role.OldRoleID(owner);
                    if (oldteam != (int)TeamID.SCP && oldteam != (int)TeamID.BerserkSCP && (!Synapse.Api.Roles.RoleManager.Get.IsIDRegistered(oldrole) || !(Synapse.Api.Roles.RoleManager.Get.GetCustomRole(oldrole) is IUtrRole)))
                    {
                        owner.RoleID = VtController.Get.Role.OldPlayerRole[owner];
                        owner.Position = owner.DeathPosition;
                        owner.Inventory.Clear();
                        lastPower = DateTime.Now;
                        message = "You successfully revive (s)he";
                    }
                    else 
                        message = "You try to revive a scp or a UTR";
                }
                else
                    message = Cooldown.Send(lastPower, Plugin.Instance.Config.ChiInfirmierCooldown);
            }
            else message = "You ave only one power";
            return false;
        }
    }
}
