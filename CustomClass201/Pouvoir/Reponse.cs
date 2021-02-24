using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClass.Pouvoir
{
    public static class Reponse
    {
        public static void Cooldown(Player player, DateTime lastPower, int TempsRequi)
        {
            int cooldown = TempsRequi - (int)(DateTime.Now - lastPower).TotalSeconds;
            player.SendBroadcast(2, PluginClass.PluginTranslation.ActiveTranslation.
                           PowerCooldown.Replace("%Time%", cooldown.ToString()));
            player.SendConsoleMessage(PluginClass.PluginTranslation.ActiveTranslation.
                PowerCooldown.Replace("%Time%", cooldown.ToString()));
        }
    }
}
