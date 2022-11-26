 using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;

namespace VT079
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSet;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;
        }

        private void OnRestart()
        {
            Plugin.Instance.SCPRoleDeconf = Plugin.Instance.Config.Scp079ScpDeconf.ToList();
        }

        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            //Pour que 079 bloque les SCPs... 
            if (ev.Door.Locked && !ev.Player.Bypass && ev.Player.RealTeam == Team.SCP)
                ev.Allow = false;
        }

        private void OnPlayerSet(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp079)
                ev.Player.gameObject.AddComponent<Scp079Behaviour>();

            if (Plugin.Instance.SCPRoleDeconf.Contains((int)ev.Role))
                Plugin.Instance.SCPRoleDeconf.Remove((int)ev.Role);
        }
    }
}