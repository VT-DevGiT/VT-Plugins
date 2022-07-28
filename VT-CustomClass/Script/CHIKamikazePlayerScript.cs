using PlayerStatsSystem;
using Synapse;
using Synapse.Api.Enum;
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
    public class CHIKamikazeScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.CHIenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.CHIally.ToList();

        protected override RoleType RoleType => RoleType.ChaosRepressor;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosKamikaze;

        protected override string RoleName => Plugin.Instance.Config.KamikazeName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.KamikazeConfig;

        private DateTime coolDown = DateTime.Now;

        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.kamikaze)
            {
                if ((DateTime.Now - coolDown).TotalSeconds < 30)
                {
                    message = "you just spawned!";
                    return false;
                }
                
                Server.Get.Map.Explode(Player.Position, GrenadeType.Grenade, Player);
                Player.Hurt(new ExplosionDamageHandler(new Footprinting.Footprint(Player.Hub), Vector3.zero, -1, 100));
                message = "BOOM";
                return true;
            }
            message = "You ave only one power";
            return false;
        }

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        private static void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim?.CustomRole is CHIKamikazeScript)
            {
                Server.Get.Map.SpawnGrenade(ev.Victim.DeathPosition, Vector3.zero, Plugin.Instance.Config.KamikazeGrenadeTimeDeath, GrenadeType.Grenade, ev.Victim);
            }
        }
    }
}
