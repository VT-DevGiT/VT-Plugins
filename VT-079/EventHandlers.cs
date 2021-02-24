using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;

namespace VT079
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Scp.Scp079.Scp079RecontainEvent += On079Recontain;
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSet;
            Server.Get.Events.Map.WarheadDetonationEvent+= OnWarhead;
        }

        private void OnWarhead()
        {
            var listJoueur = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Scp079);
            foreach (var joueur079 in listJoueur)
            {
                joueur079.GodMode = false;
            }
        }

        private void OnPlayerSet(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp079)
            {
                ev.Player.gameObject.AddComponent<Scp079Behaviour>();
                Synapse.Api.Door porte = Server.Get.Map.Doors.FirstOrDefault(p => p.Name == "079_SECOND");
                if (porte != null)
                {
                    porte.Locked = true;
                }
            }
            if (Plugin.SCPRoleDeconf.Contains((int)ev.Role))
            {
                Plugin.SCPRoleDeconf.Remove((int)ev.Role);
            }
        }
        private void On079Recontain(Scp079RecontainEventArgs ev)
        {
            Synapse.Api.Door porte = Server.Get.Map.Doors.FirstOrDefault(p => p.Name == "079_SECOND");
            if (porte != null)
            {
                porte.Locked = false;
            }
            if (Plugin.Config.Scp079AdvenceRecontain)
            { 
                ev.Allow = false;
                var listJoueur = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Scp079);
                foreach (var joueur079 in listJoueur)
                {
                    joueur079.GodMode = true;
                }
            }
        }
    }
}