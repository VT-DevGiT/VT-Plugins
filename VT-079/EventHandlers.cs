using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;

namespace VT079
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Scp.Scp079.Scp079RecontainEvent += On079Recontain;
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSet;
            //Server.Get.Events.Round.SpawnPlayersEvent += SpawnPlayersEvent;
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
                ev.Allow = false;
        }
    }
}