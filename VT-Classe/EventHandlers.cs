using CustomClass.PlayerScript;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;


namespace CustomClass
{
    public class EventHandlers
    {
        public bool IsValideRole(int role)
        {
            return role >= (int)MoreClasseID.Concierge && role <= (int)MoreClasseID.TestClass;
        }
        public EventHandlers()
        {
            //Server.Get.Events.Round.TeamRespawnEvent += OnTeamrespawn;
            //Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            Server.Get.Events.Player.PlayerShootEvent += OnShoot;
            Server.Get.Events.Player.PlayerItemUseEvent += OnItemUse;
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
        }

        private void OnItemUse(PlayerItemInteractEventArgs ev)
        {
            if (IsValideRole(ev.Player.RoleID)
                           && (ev.Player.CustomRole is BasePlayerScript script))
            {
                script.OnItemUse(ev);
            }
        }

        private void OnShoot(PlayerShootEventArgs ev)
        {
            if (IsValideRole(ev.Target.RoleID)
               && (ev.Target.CustomRole is BasePlayerScript script))
            {
                script.OnShootEvent(ev);
            }
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (IsValideRole(ev.Killer.RoleID)
               && (ev.Killer.CustomRole is BasePlayerScript script))
            {
                script.OnDamageEvent(ev);
            }
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {

            if (IsValideRole(ev.Player.RoleID)
                && (ev.Player.CustomRole is BasePlayerScript script) && !script.Spawned)
            {
                script.Spawned = true;
                ev.Position = script.Config.ConfigSpawnPoint.Parse().Position;
            }
        }

        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (IsValideRole(ev.Target.RoleID)
               && (ev.Target.CustomRole is BasePlayerScript script))
            {
                script.OnCuffEvent(ev);
            }
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
        {
            var rnd = new System.Random();
            // trouver ce que l on va donne comme scripte
            foreach (KeyValuePair<Player, int> entry in ev.SpawnPlayers)
            {
                var listRolePossible = MoreClasseClass.GetRolePossible(entry.Value);
                var proba = rnd.Next(0, 100);
                if (listRolePossible != null && proba < listRolePossible.Count && IsValideRole(listRolePossible[proba].Role))
                {
                    if (Server.Get.GetPlayers(x => !x.OverWatch).Count >= listRolePossible[proba].MinPlayer)
                    {
                        ev.SpawnPlayers[entry.Key] = listRolePossible[proba].Role;
                    }
                }
            }
        }

        private void OnTeamrespawn(TeamRespawnEventArgs ev)
        {
            throw new NotImplementedException();
        }


        private bool IsScpID(int id) => id == (int)RoleType.Scp173 || id == (int)RoleType.Scp049 || id == (int)RoleType.Scp0492 || id == (int)RoleType.Scp079 || id == (int)RoleType.Scp096 || id == (int)RoleType.Scp106 || id == (int)RoleType.Scp93953 || id == (int)RoleType.Scp93989;

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (IsValideRole(ev.Victim.RoleID)
                && (ev.Victim.CustomRole is BasePlayerScript scriptVictim))
            {
                scriptVictim.OnDeathEvent(ev);
            }

            if (IsValideRole(ev.Killer.RoleID)
                && (ev.Killer.CustomRole is BasePlayerScript scriptKille))
            {
                scriptKille.OnKillEvent(ev);
            }
        }
        /*
                private void OnKeyPress(Synapse.Api.Events.SynapseEventArguments.PlayerKeyPressEventArgs ev)
                {
        #if DEBUG
                    if (ev.KeyCode == KeyCode.Alpha7) ev.Player.CustomRole = new Scp056PlayerScript();
        #endif
                    if (ev.Player.RoleID != 56) return;

                    RoleType role;

                    switch (ev.KeyCode)
                    {
                        case KeyCode.Alpha1: role = RoleType.ClassD; break;

                        case KeyCode.Alpha2: role = RoleType.Scientist; break;

                        case KeyCode.Alpha3: role = RoleType.FacilityGuard; break;

                        case KeyCode.Alpha4: role = RoleType.NtfLieutenant; break;

                        case KeyCode.Alpha5: role = RoleType.ChaosInsurgency; break;

                        case KeyCode.Alpha6:
                            var targets = Server.Get.GetPlayers(x => x.RealTeam == Team.MTF || x.RealTeam == Team.CDP || x.RealTeam == Team.RSC).Count;
                            ev.Player.SendBroadcast(7, PluginClass.GetTranslation("targets").Replace("%targets%", targets.ToString()));
                            return;

                        default: return;
                    }

                    ev.Player.ChangeRoleAtPosition(role);
                    ev.Player.MaxHealth = PluginClass.Config.Scp056Health;
                    ev.Player.Ammo5 = 999;
                    ev.Player.Ammo7 = 999;
                    ev.Player.Ammo9 = 999;
                }
        */
    }
}
