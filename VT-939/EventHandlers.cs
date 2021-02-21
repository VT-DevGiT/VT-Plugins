using Synapse;
using Synapse.Api.Events.SynapseEventArguments;

namespace VT939
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            //Server.Get.Events.Player.PlayerSpeakEvent += OnSpeck;
        }

        private void OnSpeck(PlayerSpeakEventArgs ev)
        {
            if (ev.Player.TeamID != (int)Team.RIP)
                ev.Player.Hub.footstepSync.CmdScp939Noise(25f);
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
            {
                if (ev.Player.gameObject.TryGetComponent(out Scp939Controller customScp939))
                    customScp939.Destroy();

                ev.Player.gameObject.AddComponent<Scp939Controller>();
            }
        }


    }
}