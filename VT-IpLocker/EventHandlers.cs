using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using System.Net;

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
            IPAddress Ip = ev.Request.RemoteEndPoint.Address;
            string contry = Methode.GetUserCountryByIp(Ip.ToString());
            Server.Get.Logger.Info(Ip.ToString());
            Server.Get.Logger.Info(contry);
            if (!Plugin.Config.WhitListCountry.Contains(contry))
                if (!Plugin.Config.WhitListIP.Contains(Ip))
                    ev.Allow = false;
        }
    }
}