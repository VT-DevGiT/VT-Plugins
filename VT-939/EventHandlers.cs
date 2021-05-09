using Dissonance;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;

namespace VT939
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Round.SpawnPlayersEvent += OnSpawn;
        }

        private void OnSpawn(SpawnPlayersEventArgs ev)
        {
            
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
            {
                if (ev.Player.gameObject.TryGetComponent(out Scp939Controller customScp939))
                    customScp939.Kill();
                ev.Player.gameObject.AddComponent<Scp939Controller>();
            }
        }


    }
}