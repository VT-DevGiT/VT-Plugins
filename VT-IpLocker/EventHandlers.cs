using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VT_IpLocker
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Server.PreAuthenticationEvent += OnAuthentication;
        }

        private void OnAuthentication(PreAuthenticationEventArgs ev)
        {
            
            Server.Get.Logger.Info(ev.Request.RemoteEndPoint.Address.ToString());
            Server.Get.Logger.Info(Methode.GetUserCountryByIp(ev.Request.RemoteEndPoint.Address.ToString()));
            
        }
    }
}