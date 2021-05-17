using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass.Pouvoir
{
    public static class Reponse
    {
        public static void Cooldown(Player player, DateTime lastPower, int Time)
        {
            int cooldown = Time - (int)(DateTime.Now - lastPower).TotalSeconds;
            player.SendBroadcast(1, PluginClass.PluginTranslation.ActiveTranslation.
                           PowerCooldown.Replace("%Time%", cooldown.ToString()));
            player.SendConsoleMessage(PluginClass.PluginTranslation.ActiveTranslation.
                PowerCooldown.Replace("%Time%", cooldown.ToString()));
        }
    }
}
