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
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            //Pour que 079 puisse blockée les SCP
            if (ev.Door.Locked && !ev.Player.Bypass && ev.Player.RealTeam == Team.SCP)
                ev.Allow = false;
        }

        private void OnPlayerSet(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp079)
                ev.Player.gameObject.AddComponent<Scp079Behaviour>();

            if (Plugin.SCPRoleDeconf.Contains((int)ev.Role))
                Plugin.SCPRoleDeconf.Remove((int)ev.Role);
        }
    }
}