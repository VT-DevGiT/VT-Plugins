using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VTData
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerJoinEvent += OnJoin;
            Server.Get.Events.Player.PlayerLeaveEvent += OnLeave;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }
        private void OnDeath(PlayerDeathEventArgs ev)
        {
            
        }

        private void OnJoin(PlayerJoinEventArgs ev)
        {

            Plugin.PlayerJoinDate.Add(ev.Player.UserId, DateTime.Now);
            if (true)
            {
            
            }
            
        }

        private void OnLeave(PlayerLeaveEventArgs ev)
        {
            DateTime JoinDateTime;
            if (Plugin.PlayerJoinDate.TryGetValue(ev.Player.UserId, out JoinDateTime))
            { 
                var TimInGame = DateTime.Now - JoinDateTime;
                
            
            }
        }
    }
}