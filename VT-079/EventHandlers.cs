using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Method;

namespace VT079
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSet;
            Server.Get.Events.Map.WarheadDetonationEvent+= OnWarhead;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            //Pour que 079 puisse blockée les SCP
            if (ev.Door.Locked && !ev.Player.Bypass && ev.Player.RealTeam == Team.SCP)
                ev.Allow = false;
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
                
                if (porte != null && Methods.GetVoltage() != 5000)
                    porte.Locked = true;
            }

            if (Plugin.SCPRoleDeconf.Contains((int)ev.Role))
            {
                Plugin.SCPRoleDeconf.Remove((int)ev.Role);
            }
        }
    }
}