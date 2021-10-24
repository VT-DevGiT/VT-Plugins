using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTDevHelp
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Server.UpdateEvent += OnUpdate;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            Logger.Get.Info($"Player Set class : {ev.Player} {ev.Player.RoleID} -> {(int)ev.Role}");
        }

        private void OnUpdate()
        {
            foreach (var player in Server.Get.Players)
            {

            }
        }
    }
}