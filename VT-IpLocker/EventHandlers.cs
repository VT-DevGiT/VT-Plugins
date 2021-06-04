using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using System.Net;

namespace VT_Item
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
            string IpStr = Ip.ToString();   
            string contry = Methode.GetUserCountryByIp(IpStr);
            Server.Get.Logger.Info(IpStr);
            Server.Get.Logger.Info(contry);
            if (!Plugin.Config.WhitListCountry.Contains(contry))
                if (!Plugin.Config.WhitListIP.Contains(IpStr))
                    ev.Allow = false;
        }
    }
}